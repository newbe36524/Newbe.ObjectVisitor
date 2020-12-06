using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class IsNotInSetTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(new[] {1, 2, 3}, false)]
        [TestCase(new[] {1, 3}, true)]
        [TestCase(new int[0], true)]
        public void IsNotInSet(int[] dataRange, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.Int).IsNotInSet(dataRange)
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                Int = 2
            });
            ResultShouldBe(result, success);
        }

        [Test]
        [TestCase(2, new[] {1, 2, 3}, false)]
        [TestCase(2, new[] {1, 3}, true)]
        [TestCase(2, new int[0], true)]
        [TestCase(null, new[] {1, 3}, true)]
        [TestCase(null, null, false)]
        [TestCase(0, null, true)]
        public void IsNotInSetNullable(int? value, int[] dataRange, bool success)
        {
            var set = dataRange?.Cast<int?>().ToArray() ?? new int?[] {null};
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.NullInt).IsNotInSet(set)
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
        [TestCase(2, new[] {1, 2, 3}, false)]
        [TestCase(2, new[] {1, 3}, true)]
        [TestCase(2, new int[0], true)]
        [TestCase(null, new[] {1, 3}, true)]
        public void IsNotInSetWithComparer(int? value, int[] dataRange, bool success)
        {
            var set = dataRange.Cast<int?>().ToArray();
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.NullInt).IsNotInSet(set, new IntNullableEqualityComparer())
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