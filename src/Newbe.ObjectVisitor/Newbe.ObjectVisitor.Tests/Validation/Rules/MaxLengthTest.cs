using System;
using System.Collections.Generic;
using FluentAssertions;
using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class MaxLengthTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(0, false)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        public void MaxLength(int min, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.String).MaxLength(min)
                .Property(x => x.Ints).MaxLength(min)
                .Property(x => x.EnumerableItem).MaxLength(min)
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                Ints = new[] {1, 2},
                String = "12",
                Items = new List<int> {1, 2},
                EnumerableItem = new EnumerableItem(2)
            });
            ResultShouldBe(result, success);
        }
    }
}