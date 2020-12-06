using System;
using System.Collections.Generic;
using FluentAssertions;
using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class LengthTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(0, 1, false)]
        [TestCase(2, 4, true)]
        [TestCase(3, 4, false)]
        public void Length(int min, int max, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.String).Length(min, max)
                .Property(x => x.Ints).Length(min, max)
                .Property(x => x.EnumerableItem).Length(min, max)
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
            result.Success.Should().Be(success);
            if (!success)
            {
                result.Errors.Length.Should().Be(3);
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
            }
        }
    }
}