using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests
{
    public class FluentApiDesignParserTest
    {
        private static string GetTestFile() =>
            File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, "Content", "ObjectVisitorBuilder.fluent.md"));

        [Test]
        public void Parse()
        {
            var content = GetTestFile();
            var parser = new FluentApiDesignParser();
            var re = parser.Parse(content);
            re.ParametersBlock.Should().NotBeNullOrEmpty();
            re.StateDiagramBlock.Should().NotBeNullOrEmpty();
            re.SourceDesignContent.Should().NotBeNullOrEmpty();
            re.BuilderContextType.Should().NotBeNullOrEmpty();
            re.BuilderTypeName.Should().NotBeNullOrEmpty();
            re.ActionMapping.Should().NotBeEmpty();
            re.ApiSteps.Should().NotBeEmpty();
        }

        [Test]
        public void ParseBlock()
        {
            var content = GetTestFile();
            var parser = new FluentApiDesignParser();
            var fluentApiDesign = new FluentApiDesign();
            parser.ParseBlock(content, fluentApiDesign);
            Console.WriteLine(fluentApiDesign.ParametersBlock);
            Console.WriteLine(fluentApiDesign.StateDiagramBlock);
        }
    }
}