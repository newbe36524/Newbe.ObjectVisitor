using System;
using FluentAssertions;
using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class EnumTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase((NoFlagEnum) 5, 5, "???", false)]
        [TestCase(NoFlagEnum.Autumn, 3, nameof(NoFlagEnum.Autumn), true)]
        public void NoFlagEnumTest(NoFlagEnum enumValue, byte byteValue, string stringValue, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>.GetBuilder()
                .Property(x => x.NoFlagEnum).IsInEnum()
                .Property(x => x.NoFlagEnumByte).IsInEnum(typeof(NoFlagEnum))
                .Property(x => x.NoFlagEnumString).IsInEnum(typeof(NoFlagEnum))
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                NoFlagEnum = enumValue,
                NoFlagEnumByte = byteValue,
                NoFlagEnumString = stringValue
            });
            ResultShouldBe(result, success);
        }

        [Test]
        [TestCase((FlagsEnum) 0, 0, 0, "0", false)]
        [TestCase(FlagsEnum.Afternoon | FlagsEnum.Evening, 3, 3, nameof(FlagsEnum.Evening), true)]
        public void FlagsEnumTest(FlagsEnum enumValue, int intValue, int? intNullableValue, string stringValue,
            bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>.GetBuilder()
                .Property(x => x.FlagsEnum).IsInEnum()
                .Property(x => x.FlagsEnumInt).IsInEnum(typeof(FlagsEnum))
                .Property(x => x.FlagsEnumNullableInt).IsInEnum(typeof(FlagsEnum))
                .Property(x => x.FlagsEnumString).IsInEnum(typeof(FlagsEnum))
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                FlagsEnum = enumValue,
                FlagsEnumInt = intValue,
                FlagsEnumString = stringValue,
                FlagsEnumNullableInt = intNullableValue
            });
            ResultShouldBe(result, success);
        }


        [Test]
        [TestCase("0", false)]
        [TestCase(nameof(FlagsEnum.Evening), true)]
        public void IsInEnumName(string stringValue, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>.GetBuilder()
                .Property(x => x.FlagsEnumString).IsInEnumName(typeof(FlagsEnum))
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                FlagsEnumString = stringValue,
            });
            ResultShouldBe(result, success);
        }
    }
}