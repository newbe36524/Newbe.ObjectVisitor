using System;
using System.IO;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.ConnectionStringBuilderFluentApi
{
    [Category("FluentAPI")]
    [Explicit]
    public class GenerateFluentApiTest
    {
        private static string GetTestFile() =>
            File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, "Content",
                "ConnectionStringBuilder.fluent.md"));

        [Test]
        public void CreateBuilder()
        {
            var content = GetTestFile();
            var parser = new FluentApiDesignParser();
            var re = parser.Parse(content);
            var generator = new FluentApiFileGenerator();
            var output = generator.Generate(re);

            var nodesCs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../", "ConnectionStringBuilderFluentApi",
                "ConnectionStringBuilder.cs");
            File.WriteAllText(nodesCs, output.FluentApiFiles.Api);
        }
    }
}