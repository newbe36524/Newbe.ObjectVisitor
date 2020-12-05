using System;
using System.IO;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.HttpClientFluentApi
{
    [Category("FluentAPI")]
    [Explicit]
    public class GenerateFluentApiTest
    {
        private static string GetTestFile() =>
            File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, "Content",
                "HttpRequestMessage.fluent.md"));

        [Test]
        public void CreateBuilder()
        {
            var content = GetTestFile();
            var parser = new FluentApiDesignParser();
            var re = parser.Parse(content);
            var generator = new FluentApiFileGenerator();
            var output = generator.Generate(re);

            var nodesCs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, "../../../", "HttpClientFluentApi",
                "HttpRequestMessageBuilder.cs");
            File.WriteAllText(nodesCs, output.FluentApiFiles.Api);
        }
    }
}