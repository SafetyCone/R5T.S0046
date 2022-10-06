# R5T.S0046
Script to add service AddX() methods to a project.


## Implementation Plan

* Survey of services (in project file path):
	* For a provided project file path.
	* Determine the built-assembly file path.
	* Reflect on the built-assembly file path. (See R5T.S0041 for an example implementation of this.)
	* Identify classes (only classes) with the R5T.T0064.ServiceImplementationMarkerAttribute attribute.
	* For these classes, identify (private, instance-level) properties of the class whose type has the R5T.T0064.IServiceDefinitionMarkerAttribute attribute.
	* For each class, get the namespaced type names of the class, and all it's properties of interest. (Dictionary<string, List<string>>)
	* Intermediate: serialize this information to a JSON file.

* Generation of AddX() methods.
	* Using the services survey result (Dictionary<string, List<string>>), either from the survey, or from the survey result file.
	* Add Usings
		* Microsoft.Extensions.Dependency injection.
		* R5T.T0147
		* All namespaces of services.
	* Create namespace.
	* Create static class.
	* Create AddX() method.
		* Create documentation.
		* Create method signature.
			* Create method name.
			* IServiceAction<T> input parameters for all dependencies.
		* Create body.
			* Create services.Run() chain.
				* Create dependencies call.
				* Create .AddSingleton() call.
			* Create return services call.

* Generation of AddXAction() methods.
	* Same as for AddX()
		* Usings, namespace
	* IServiceActionOperator instances.
		* Class
		* Interface
			* Inherit from R5T.T0147.IServiceActionOperator.
	* Create AddXAction() method.
		* Same documentation as for AddX() method.
		* Create method signature.
			* Create method name.
			* IServiceAction<T> input parameters for all dependencies.
		* Create body.
			* serviceAction instance creation from R5T.T0147.IServiceActionOperator
			* services anonymous function, calling AddX() method, provided with IServiceAction<T> input parameters for all dependencies.
