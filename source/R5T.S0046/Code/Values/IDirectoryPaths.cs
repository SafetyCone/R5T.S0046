using System;

using R5T.T0131;


namespace R5T.S0046
{
	[DraftValuesMarker]
	public partial interface IDirectoryPaths : IValuesMarker
	{
		/// Also see: R5T.S0041.IDirectoryPaths.NuGetAssemblies.
		public string NuGetAssemblies => @"C:\Users\David\Dropbox\Organizations\Rivet\Shared\Binaries\Nuget Assemblies\";
	}
}