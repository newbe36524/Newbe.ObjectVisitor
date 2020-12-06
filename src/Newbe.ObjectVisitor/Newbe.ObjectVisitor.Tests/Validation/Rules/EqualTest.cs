using System;
using System.Collections.Generic;
using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class EqualTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(1, 1, true)]
        [TestCase(1, 2, false)]
        public void Equal(int value, int expected, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.Int).Equal(expected)
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                Int = value
            });
            ResultShouldBe(result, success);
        }

        [Test]
        [TestCase(1, 1, true)]
        [TestCase(1, 2, false)]
        [TestCase(null, 1, false)]
        [TestCase(null, null, true)]
        public void EqualNullable(int? value, int? expected, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.NullInt).Equal(expected)
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
        [TestCase(1, 1, true)]
        [TestCase(1, 2, false)]
        [TestCase(null, 1, false)]
        [TestCase(null, null, true)]
        public void EqualComparer(int? value, int? expected, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.NullInt).Equal(expected, new IntNullableEqualityComparer())
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
    }
}