using System;

using R5T.T0131;


namespace R5T.S0046
{
	[ValuesMarker]
	public partial interface IFilePaths : IValuesMarker
	{
		public string OutputJsonFilePath => @"C:\Temp\Output.json";
		public string OutputIServiceActionOperatorFilePath => @"C:\Temp\IServiceActionOperator.cs";
		public string OutputIServiceCollectionExtensionsFilePath => @"C:\Temp\IServiceCollectionExtensions.cs";
	}
}