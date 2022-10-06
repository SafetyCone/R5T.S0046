using System;

using R5T.F0018;
using R5T.F0040;


namespace R5T.S0046
{
    public static class Instances
    {
        public static IDirectoryPaths DirectoryPaths { get; } = S0046.DirectoryPaths.Instance;
        public static IFilePaths FilePaths { get; } = S0046.FilePaths.Instance;
        public static IOperations Operations { get; } = S0046.Operations.Instance;
        public static IProjectPathsOperator ProjectPathsOperator { get; } = F0040.ProjectPathsOperator.Instance;
        public static IReflectionOperator ReflectionOperator { get; } = F0018.ReflectionOperator.Instance;
    }
}