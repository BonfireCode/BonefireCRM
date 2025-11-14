using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BonefireCRM.SourceGenerator.Tests.Common
{
    public static class ModuleInitializer
    {
        [ModuleInitializer]
        public static void Init()
        {
            VerifySourceGenerators.Initialize(); 
#if !DEBUG
            DerivePathInfo(
                (sourceFile, projectDirectory, type, method) => new(
                    directory: Path.Combine(Path.GetDirectoryName(type.Assembly.Location)!, "Snapshots"),
                    typeName: type.Name,
                    methodName: method.Name));
#endif
        }
    }
}
