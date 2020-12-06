using System;
using System.IO;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.SumBuilderFluentApi
{
    [Category("FluentAPI")]
    [Explicit]
    public class GenerateFluentApiTest
    {
        [Test]
        public void CreateBuilder()
        {
            var content = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, "Content",
                "SumBuilder.fluent.md"));
            var parser = new FluentApiDesignParser();
            var re = parser.Parse(content);
            var generator = new FluentApiFileGenerator();
            var output = generator.Generate(re);

            var nodesCs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../", "SumBuilderFluentApi",
                "SumBuilder.cs");
            File.WriteAllText(nodesCs, output.FluentApiFiles.Api);
        }

        [Test]
        public void CreateMultipleSumBuilder()
        {
            var content = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, "Content",
                "MultipleSumBuilder.fluent.md"));
            var parser = new FluentApiDesignParser();
            var re = parser.Parse(content);
            var generator = new FluentApiFileGenerator();
            var output = generator.Generate(re);

            var nodesCs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../", "SumBuilderFluentApi",
                "MultipleSumBuilder.cs");
            File.WriteAllText(nodesCs, output.FluentApiFiles.Api);
        }
    }
}