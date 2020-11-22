using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validator
{
    public class ValidateRuleTest
    {
        public class TestModel
        {
            public int Int { get; set; }
        }

        [Test]
        [TestCase(1, 2, false, true, false)]
        [TestCase(1, 2, false, false, true)]
        [TestCase(1, 3, false, true, true)]
        [TestCase(2, 3, true, true, false)]
        [TestCase(2, 3, false, true, true)]
        public void IsInRange(int min, int max, bool excludeMin, bool excludeMax, bool success)
        {
            var builder = new ValidationRuleBuilder<TestModel>(new List<ValidationRule<TestModel>>());
            var validationRules = builder.GetBuilder()
                .Property(x => x.Int).IsInRange(min, max, excludeMin, excludeMax)
                .GetRuleSet();
            var validator = new Validator<TestModel>(validationRules);
            var result = validator.Validate(new TestModel
            {
                Int = 2
            });
            result.Success.Should().Be(success);
            if (!success)
            {
                Console.WriteLine(result.Errors.Single());
            }
        }

        [Test]
        [TestCase(new[] {1, 2, 3}, true)]
        [TestCase(new[] {1, 3}, false)]
        [TestCase(new int[0], false)]
        public void IsInSet(int[] dataRange, bool success)
        {
            var builder = new ValidationRuleBuilder<TestModel>(new List<ValidationRule<TestModel>>());
            var validationRules = builder.GetBuilder()
                .Property(x => x.Int).IsInSet(dataRange)
                .GetRuleSet();
            var validator = new Validator<TestModel>(validationRules);
            var result = validator.Validate(new TestModel
            {
                Int = 2
            });
            result.Success.Should().Be(success);
            if (!success)
            {
                Console.WriteLine(result.Errors.Single());
            }
        }
    }
}