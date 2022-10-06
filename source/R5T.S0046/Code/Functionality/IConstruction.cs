using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0132;

using R5T.F0000;


namespace R5T.S0046
{
	[FunctionalityMarker]
	public partial interface IConstruction : IFunctionalityMarker
	{
		public void Run()
        {
			/// Inputs
			var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.D0106\source\R5T.D0106.D002.I001\R5T.D0106.D002.I001.csproj";


			/// Run.
			var assemblyFilePath = F0040.Instances.ProjectPathsOperator.GetAssemblyFilePathForProjectFilePath(projectFilePath);

			var serviceImplementations = this.SurveyAssemblyFile(assemblyFilePath);

			var projectNamespaceName = F0020.ProjectFileOperator.Instance.GetDefaultNamespaceName(projectFilePath);

			var iServiceActionOperatorFilePath = Instances.ProjectPathsOperator.GetPath_ForProjectDirectoryRelativePath(
					projectFilePath,
					@"Code\Functionality\IServiceActionOperator-Generated.cs");

			this.CreateAddXActionMethods(
				serviceImplementations,
				projectNamespaceName,
				iServiceActionOperatorFilePath);

			var iServiceCollectionExtensionsFilePath = Instances.ProjectPathsOperator.GetPath_ForProjectDirectoryRelativePath(
					projectFilePath,
					@"Code\Extensions\IServiceCollectionExtensions-Generated.cs");

			this.CreateAddXMethods(
				serviceImplementations,
				projectNamespaceName,
				iServiceCollectionExtensionsFilePath);
		}

		public void CreateAddXActionMethods()
        {
			this.CreateAddXActionMethods(
				Instances.FilePaths.OutputIServiceActionOperatorFilePath);
        }

        public void CreateAddXActionMethods(string outputFilePath)
        {
			/// Inputs.
			var projectNamespaceName = "R5T.D0106.D002.I001";


			/// Run.
			var serviceImplementations = F0032.JsonOperator.Instance.Deserialize_Synchronous<ServiceImplementationInformation[]>(
				Instances.FilePaths.OutputJsonFilePath);

			this.CreateAddXActionMethods(
				serviceImplementations,
				projectNamespaceName,
				outputFilePath);
		}

		public void CreateAddXActionMethods(
			IEnumerable<ServiceImplementationInformation> serviceImplementations,
			string projectNamespaceName,
			string outputFilePath)
		{
			/// Run.
			var usingLines = Instances.Operations.GetUsingLines(
				projectNamespaceName,
				serviceImplementations,
				new[]
                {
					"R5T.T0132",
					"R5T.T0147"
                });

			var namespaceLines = new[]
			{
				$"namespace {projectNamespaceName}",
				"{",
			}
			.AppendRange(Instances.Operations.GetIServiceActionOperatorInterfaceLines(serviceImplementations))
			.AppendRange(new[]
			{
				"}"
			})
			.Now();

			var lines = usingLines
				.Append(Z0000.Instances.Strings.Empty)
				.Append(Z0000.Instances.Strings.Empty)
				.AppendRange(namespaceLines);

			FileSystemOperator.Instance.EnsureDirectoryForFilePathExists(
				outputFilePath);

			FileOperator.Instance.WriteLines(
				outputFilePath,
				lines);
		}

		public void CreateAddXMethods()
        {
			this.CreateAddXMethods(
				Instances.FilePaths.OutputIServiceCollectionExtensionsFilePath);
        }

		public void CreateAddXMethods(
			string outputFilePath)
        {
			var projectNamespaceName = "R5T.D0106.D002.I001";

			var serviceImplementations = F0032.JsonOperator.Instance.Deserialize_Synchronous<ServiceImplementationInformation[]>(
				Instances.FilePaths.OutputJsonFilePath);

			this.CreateAddXMethods(
				serviceImplementations,
				projectNamespaceName,
				outputFilePath);
		}

		public void CreateAddXMethods(
			IEnumerable<ServiceImplementationInformation> serviceImplementations,
			string projectNamespaceName,
			string outputFilePath)
		{
			var usingLines = Instances.Operations.GetUsingLines(
				projectNamespaceName,
				serviceImplementations,
				EnumerableOperator.Instance.From("R5T.T0147"));            

			var namespaceLines = new[]
			{
				$"namespace {projectNamespaceName}",
				"{",
			}
			.AppendRange(Instances.Operations.GetIServiceCollectionExtensionsClassLines(serviceImplementations))
			.AppendRange(new[]
			{
				"}"
			})
			.Now();

			var lines = usingLines
				.Append(Z0000.Instances.Strings.Empty)
				.Append(Z0000.Instances.Strings.Empty)
				.AppendRange(namespaceLines);

			FileSystemOperator.Instance.EnsureDirectoryForFilePathExists(
				outputFilePath);

			FileOperator.Instance.WriteLines(
				outputFilePath,
				lines);
		}

		public void SurveyAssemblyFile()
        {
			/// Inputs.
			var assemblyFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.D0106\source\R5T.D0106.D002.I001\bin\Debug\net5.0\R5T.D0106.D002.I001.dll";

			var serviceImplementations = this.SurveyAssemblyFile(assemblyFilePath);

			F0032.JsonOperator.Instance.Serialize_Synchronous(
				Instances.FilePaths.OutputJsonFilePath,
				serviceImplementations);
		}

		public ServiceImplementationInformation[] SurveyAssemblyFile(string assemblyFilePath)
		{
			var serviceImplementations = new List<ServiceImplementationInformation>();

			var isServiceDefinitionType = F0018.TypeOperator.Instance.GetTypeByHasAttributeOfNamespacedTypeNamePredicate(
				Z0006.NamespacedTypeNames.Instance.ServiceDefinitionMarkerAttribute);
			var isServiceImplementationType = F0018.TypeOperator.Instance.GetTypeByHasAttributeOfNamespacedTypeNamePredicate(
				Z0006.NamespacedTypeNames.Instance.ServiceImplementationMarkerAttribute);

			Instances.ReflectionOperator.InAssemblyContext(
				assemblyFilePath,
				EnumerableOperator.Instance.From(Instances.DirectoryPaths.NuGetAssemblies),
				assembly =>
				{
					AssemblyOperator.Instance.ForTypes(
						assembly,
						isServiceImplementationType,
                        typeInfo =>
                        {
                            var serviceImplementationNamespacedTypeName = TypeOperator.Instance.GetNamespacedTypeName_ForTypeInfo(typeInfo);

							var directlyImplementedInterfaces = F0018.TypeOperator.Instance.GetOnlyDirectlyImplementedInterfaces(typeInfo);

							// Use the first.
							var serviceDefinitionNamespacedTypeName = directlyImplementedInterfaces
								.Where(@interface => isServiceDefinitionType(@interface))
								.Select(x => TypeOperator.Instance.GetNamespacedTypeName(x))
								.First();

                            var serviceDefinitionProperties = typeInfo.DeclaredProperties
                                .Where(property => isServiceDefinitionType(property.PropertyType))
                                .Now();

                            var serviceDependencyNamespacedTypeNames = serviceDefinitionProperties
                                .Select(property => TypeOperator.Instance.GetNamespacedTypeName_ForType(property.PropertyType))
                                .Now();

							var serviceImplementationInformation = new ServiceImplementationInformation
							{
								ImplementationNamespacedTypeName = serviceImplementationNamespacedTypeName,
								DefinitionNamespacedTypeName = serviceDefinitionNamespacedTypeName,
								DependencyDefinitionNamespacedTypeNames = serviceDependencyNamespacedTypeNames,
							};

							serviceImplementations.Add(serviceImplementationInformation);
                        });
				});

			return serviceImplementations.ToArray();
        }
	}
}