using System;
using System.Collections.Generic;
using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class NotNullTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(null, false)]
        [TestCase(1, true)]
        [TestCase(0, true)]
        public void NotNull(int? value, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.NullInt).NotNull()
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                NullInt = value
            });
            ResultShouldBe(result, success);
        }

        [Test]
        [TestCase(null, false)]
        [TestCase("", true)]
        [TestCase("0", true)]
        public void NotNull(string value, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.String).NotNull()
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