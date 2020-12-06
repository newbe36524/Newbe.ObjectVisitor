using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class NotNotMatchRegexTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(null, true)]
        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("\t", false)]
        [TestCase("0", false)]
        public void NotMatchRegex(string value, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.String).NotMatchRegex(".*")
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
        [TestCase(null, true)]
        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("\t", false)]
        [TestCase("0", false)]
        public void NotMatchRegex2(string value, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.String).NotMatchRegex(new Regex(".*"))
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