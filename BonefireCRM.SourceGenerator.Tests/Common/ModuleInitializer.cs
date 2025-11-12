using System.Runtime.CompilerServices;

namespace BonefireCRM.SourceGenerator.Tests.Common
{
    public static class ModuleInitializer
    {
        [ModuleInitializer]
        public static void Init()
        {
            VerifySourceGenerators.Initialize(); 
        }
    }
}
