using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validator
{
    public class ValidatorTest
    {
        [Test]
        public void EmptyRule()
        {
            var validator = new Validator<Yueluo>(new List<ValidationRuleGroup<Yueluo>>());
            var result = validator.Validate(Yueluo.Create());
            result.Errors.Should().BeEmpty();
            result.Success.Should().BeTrue();
        }

        [Test]
        public void OneRuleAlwaysSuccess()
        {
            var rules = new List<ValidationRuleGroup<Yueluo>>
            {
                new ValidationRuleGroup<Yueluo>
                {
                    new ValidationRule<Yueluo>
                    {
                        MustExpression = x => true,
                        ErrorMessageExpression = x => "Oh No!"
                    }
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
            var rules = new List<ValidationRuleGroup<Yueluo>>
            {
                new ValidationRuleGroup<Yueluo>
                {
                    new ValidationRule<Yueluo>
                    {
                        MustExpression = x => false,
                        ErrorMessageExpression = x => "Oh No!"
                    }
                }
            };
            var validator = new Validator<Yueluo>(rules);
            var result = validator.Validate(Yueluo.Create());
            result.Errors.Single().Should().Be("Oh No!");
            result.Success.Should().BeFalse();
        }


        [Test]
        [TestCase(true, true, true, new string[0])]
        [TestCase(false, true, false, new[] {"Oh No1!"})]
        [TestCase(true, false, false, new[] {"Oh No2!"})]
        [TestCase(false, false, false, new[] {"Oh No1!", "Oh No2!"})]
        public void AndAll(bool rule1, bool rule2, bool success, string[] errors)
        {
            var rules = new List<ValidationRuleGroup<Yueluo>>
            {
                new ValidationRuleGroup<Yueluo>
                {
                    new ValidationRule<Yueluo>
                    {
                        MustExpression = x => rule1,
                        ErrorMessageExpression = x => "Oh No1!"
                    },
                    new ValidationRule<Yueluo>
                    {
                        MustExpression = x => rule2,
                        ErrorMessageExpression = x => "Oh No2!"
                    }
                }
            };
            var validator = new Validator<Yueluo>(rules);
            var result = validator.Validate(Yueluo.Create());
            result.Errors.Should().BeEquivalentTo(errors);
            result.Success.Should().Be(success);
        }

        [Test]
        [TestCase(true, true, true, new string[0])]
        [TestCase(false, true, true, new string[0])]
        [TestCase(true, false, true, new string[0])]
        [TestCase(false, false, false, new[] {"Oh No1!", "Oh No2!"})]
        public void OrAny(bool rule1, bool rule2, bool success, string[] errors)
        {
            var validationRuleGroup = new ValidationRuleGroup<Yueluo>
            {
                new ValidationRule<Yueluo>
                {
                    MustExpression = x => rule1,
                    ErrorMessageExpression = x => "Oh No1!"
                },
                new ValidationRule<Yueluo>
                {
                    MustExpression = x => rule2,
                    ErrorMessageExpression = x => "Oh No2!"
                }
            };
            validationRuleGroup.RuleRelation = ValidationRuleRelation.Any;
            var rules = new List<ValidationRuleGroup<Yueluo>>
            {
                validationRuleGroup
            };
            var validator = new Validator<Yueluo>(rules);
            var result = validator.Validate(Yueluo.Create());
            result.Errors.Should().BeEquivalentTo(errors);
            result.Success.Should().Be(success);
        }

        [Test]
        [TestCase(true, false, new[]{"Error"})]
        [TestCase(false, true, new string[0])]
        public void Not(bool rule1, bool success, string[] errors)
        {
            var validationRuleGroup = new ValidationRuleGroup<Yueluo>
            {
                new ValidationRule<Yueluo>
                {
                    MustExpression = x => rule1,
                    ErrorMessageExpression = x => "Oh No1!"
                },
            };
            validationRuleGroup.RuleRelation = ValidationRuleRelation.Not;
            var rules = new List<ValidationRuleGroup<Yueluo>>
            {
                validationRuleGroup
            };
            var validator = new Validator<Yueluo>(rules);
            var result = validator.Validate(Yueluo.Create());
            result.Errors.Should().BeEquivalentTo(errors);
            result.Success.Should().Be(success);
        }
    }
}