using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class MatchRegexTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(null, false)]
        [TestCase("", true)]
        [TestCase(" ", true)]
        [TestCase("\t", true)]
        [TestCase("0", true)]
        public void MatchRegex(string value, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.String).MatchRegex(".*")
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                String = value
            });
            ResultShouldBe(result, success);
        }
        
        [Test]
        [TestCase(null, false)]
        [TestCase("", true)]
        [TestCase(" ", true)]
        [TestCase("\t", true)]
        [TestCase("0", true)]
        public void MatchRegex2(string value, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.String).MatchRegex(new Regex(".*"))
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                String = value
            });
            ResultShouldBe(result, success);
        }
    }
}