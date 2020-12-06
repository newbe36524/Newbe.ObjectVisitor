using System;
using System.Collections.Generic;
using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class EmptyTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(null, true)]
        [TestCase("", true)]
        [TestCase(" ", true)]
        [TestCase("\t", true)]
        [TestCase("0", false)]
        public void Empty(string value, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.String).Empty()
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
        [TestCase(new int[0], true)]
        [TestCase(new[] {1}, false)]
        public void Empty(int[] value, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.Ints).Empty()
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