using Basic.Reference.Assemblies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace BonefireCRM.SourceGenerator.Tests.Common
{
    internal static class TestHelper
    {
        public static GeneratorDriver GenerateCode(string source, IIncrementalGenerator generator)
        {
            // Parse the provided string into a C# syntax tree
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);

            // Create references for assemblies we require
            // We could add multiple references if required
            IEnumerable<PortableExecutableReference> references = Net80.References.All;

            var compilation = CSharpCompilation.Create(
                "Tests",
                [syntaxTree],
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            // The GeneratorDriver is used to run our generator against a compilation
            GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

            // Run the source generator!
            driver = driver.RunGenerators(compilation);

            return driver;
        }
    }
}
