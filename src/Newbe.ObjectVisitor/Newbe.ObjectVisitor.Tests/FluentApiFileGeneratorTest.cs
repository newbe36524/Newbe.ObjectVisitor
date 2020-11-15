using System;
using System.IO;
using System.Text.RegularExpressions;
using FluentAssertions;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests
{
    [Explicit]
    public class FluentApiFileGeneratorTest
    {
        private static string GetTestFile() =>
            File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, "Content", "foreach.fluent.md"));

        [Test]
        public void Generate()
        {
            var content = GetTestFile();
            var parser = new FluentApiDesignParser();
            var re = parser.Parse(content);
            var generator = new FluentApiFileGenerator();
            var output = generator.Generate(re);
            var nodesCs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../",
                "ForeachBuilderNodes.cs");
            File.WriteAllText(nodesCs, output.FluentApiFiles.AutoGenerate);

            output.FluentApiFiles.AutoGenerate.Should().NotBeNullOrEmpty();
            Console.WriteLine(output.FluentApiFiles.AutoGenerate);
        }

        [Test]
        public void RegexReplace()
        {
            var source = "_T_";
            var re = Regex.Replace(source, "_(.+)_", "<$1>");
            re.Should().Be("<T>");
        }
    }
}