using System;

using R5T.T0142;


namespace R5T.S0046
{
    [DraftUtilityTypeMarker]
    public class ServiceImplementationInformation
    {
        public string ImplementationNamespacedTypeName { get; set; }
        public string DefinitionNamespacedTypeName { get; set; }
        public string[] DependencyDefinitionNamespacedTypeNames { get; set; }
    }
}
