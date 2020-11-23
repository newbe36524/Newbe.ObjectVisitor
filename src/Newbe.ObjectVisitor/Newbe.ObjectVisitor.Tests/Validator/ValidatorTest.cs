using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validator
{
    public class ValidatorTest
    {
        [Test]
        public void EmptyRule()
        {
            var validator = new Validator<Yueluo>(Enumerable.Empty<ValidationRule<Yueluo>>());
            var result = validator.Validate(Yueluo.Create());
            result.Errors.Should().BeEmpty();
            result.Success.Should().BeTrue();
        }

        [Test]
        public void OneRuleAlwaysSuccess()
        {
            var rules = new List<ValidationRule<Yueluo>>
            {
                new ValidationRule<Yueluo>
                {
                    MustExpression = x => true,
                    ErrorMessageExpression = x => "Oh No!"
                }
            };
            var validator = new Validator<Yueluo>(rules);
            var result = validator.Validate(Yueluo.Create());
            result.Errors.Should().BeEmpty();
            result.Success.Should().BeTrue();
        }

        [Test]
        public void OneRuleAlwaysFail()
        {
            var rules = new List<ValidationRule<Yueluo>>
            {
                new ValidationRule<Yueluo>
                {
                    MustExpression = x => false,
                    ErrorMessageExpression = x => "Oh No!"
                }
            };
            var validator = new Validator<Yueluo>(rules);
            var result = validator.Validate(Yueluo.Create());
            result.Errors.Single().Should().Be("Oh No!");
            result.Success.Should().BeFalse();
        }
    }
}