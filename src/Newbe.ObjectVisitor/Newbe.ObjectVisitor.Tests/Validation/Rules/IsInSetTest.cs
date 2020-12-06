using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class IsInSetTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(new[] {1, 2, 3}, true)]
        [TestCase(new[] {1, 3}, false)]
        [TestCase(new int[0], false)]
        public void IsInSet(int[] dataRange, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.Int).IsInSet(dataRange)
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
        [TestCase(2, new[] {1, 2, 3}, true)]
        [TestCase(2, new[] {1, 3}, false)]
        [TestCase(2, new int[0], false)]
        [TestCase(null, new[] {1, 3}, false)]
        [TestCase(null, null, true)]
        [TestCase(0, null, false)]
        public void IsInSetNullable(int? value, int[] dataRange, bool success)
        {
            var set = dataRange?.Cast<int?>().ToArray() ?? new int?[] {null};
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.NullInt).IsInSet(set)
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
        [TestCase(2, new[] {1, 2, 3}, true)]
        [TestCase(2, new[] {1, 3}, false)]
        [TestCase(2, new int[0], false)]
        [TestCase(null, new[] {1, 3}, false)]
        public void IsInSetWithComparer(int? value, int[] dataRange, bool success)
        {
            var set = dataRange.Cast<int?>().ToArray();
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.NullInt).IsInSet(set, new IntNullableEqualityComparer())
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