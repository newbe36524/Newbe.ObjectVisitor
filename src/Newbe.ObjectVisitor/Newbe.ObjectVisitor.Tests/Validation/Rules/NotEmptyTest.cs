using System;
using System.Collections.Generic;
using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class NotEmptyTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("\t", false)]
        [TestCase("0", true)]
        public void NotEmpty(string value, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.String).NotEmpty()
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
        [TestCase(new int[0], false)]
        [TestCase(new[] {1}, true)]
        public void NotEmpty(int[] value, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.Ints).NotEmpty()
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                Ints = value
            });
            ResultShouldBe(result, success);
        }
    }
}