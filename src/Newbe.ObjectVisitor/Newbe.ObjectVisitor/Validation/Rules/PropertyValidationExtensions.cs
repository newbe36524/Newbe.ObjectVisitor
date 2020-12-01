using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newbe.ObjectVisitor.Validation;

// ReSharper disable once CheckNamespace
namespace Newbe.ObjectVisitor.Validator
{
    public static class PropertyValidationExtensions
    {
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            Equal<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue expected)
            where TValue : IEquatable<TValue>
        {
            var rule = EqualRuleFactory.Create<T, TValue>(expected);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            Equal<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue expected,
                IEqualityComparer<TValue> comparer)
        {
            var rule = EqualRuleFactory.Create<T, TValue>(expected, comparer);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            NotEqual<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue expected)
            where TValue : IEquatable<TValue>
        {
            var rule = NotEqualRuleFactory.Create<T, TValue>(expected);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            NotEqual<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue expected,
                IEqualityComparer<TValue> comparer)
        {
            var rule = NotEqualRuleFactory.Create<T, TValue>(expected, comparer);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            LessThan<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue max)
            where TValue : IComparable<TValue>
        {
            var rule = LessThanRuleFactory.Create<T, TValue>(max);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            LessThan<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue max,
                IComparer<TValue> comparer)
        {
            var rule = LessThanRuleFactory.Create<T, TValue>(max, comparer);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            LessThanOrEqual<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue max)
            where TValue : IComparable<TValue>
        {
            var rule = LessThanOrEqualRuleFactory.Create<T, TValue>(max);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            LessThanOrEqual<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue max,
                IComparer<TValue> comparer)
        {
            var rule = LessThanOrEqualRuleFactory.Create<T, TValue>(max, comparer);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            GreaterThan<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue min)
            where TValue : IComparable<TValue>
        {
            var rule = GreaterThanRuleFactory.Create<T, TValue>(min);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            GreaterThan<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue min,
                IComparer<TValue> comparer)
        {
            var rule = GreaterThanRuleFactory.Create<T, TValue>(min, comparer);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            GreaterThanOrEqual<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue min)
            where TValue : IComparable<TValue>
        {
            var rule = GreaterThanOrEqualRuleFactory.Create<T, TValue>(min);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            GreaterThanOrEqual<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue min,
                IComparer<TValue> comparer)
        {
            var rule = GreaterThanOrEqualRuleFactory.Create<T, TValue>(min, comparer);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            IsInRange<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue min,
                TValue max,
                bool excludeMin = false,
                bool excludeMax = true)
            where TValue : IComparable<TValue>
        {
            var rule = IsInRangeRuleFactory.Create<T, TValue>(min, max, excludeMin, excludeMax);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            IsInRange<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue min,
                TValue max,
                IComparer<TValue> comparer,
                bool excludeMin = false,
                bool excludeMax = true)
        {
            var rule = IsInRangeRuleFactory.Create<T, TValue>(min, max, excludeMin, excludeMax, comparer);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            IsInSet<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                IEnumerable<TValue> expectedSet)
            where TValue : IEquatable<TValue>
        {
            var rule = IsInSetRuleFactory.Create<T, TValue>(expectedSet);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            IsInSet<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                IEnumerable<TValue> expectedSet,
                IEqualityComparer<TValue> comparer)
        {
            var rule = IsInSetRuleFactory.Create<T, TValue>(expectedSet, comparer);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            IsNotInSet<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                IEnumerable<TValue> expectedSet)
            where TValue : IEquatable<TValue>
        {
            var rule = IsNotInSetRuleFactory.Create<T, TValue>(expectedSet);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            IsNotInSet<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                IEnumerable<TValue> expectedSet,
                IEqualityComparer<TValue> comparer)
        {
            var rule = IsNotInSetRuleFactory.Create<T, TValue>(expectedSet, comparer);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            Null<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step)
            where TValue : class
        {
            var rule = new ClassNullRule<T, TValue>();
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            NotNull<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step)
            where TValue : class
        {
            var rule = new ClassNotNullRule<T, TValue>();
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S
            NotEmpty<T>(
                this PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S step)
        {
            var rule = new StringNotEmptyRule<T>();
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S
            Empty<T>(
                this PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S step)
        {
            var rule = new StringEmptyRule<T>();
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            NotEmpty<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step)
            where TValue : IEnumerable
        {
            var rule = new EnumerableNotEmptyRule<T, TValue>();
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            Empty<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step)
            where TValue : IEnumerable
        {
            var rule = new EnumerableEmptyRule<T, TValue>();
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S
            MatchRegex<T>(
                this PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S step,
                string pattern)
        {
            var rule = MatchRegexRuleFactory.Create<T>(pattern);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S
            MatchRegex<T>(
                this PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S step,
                Regex regex)
        {
            var rule = MatchRegexRuleFactory.Create<T>(regex);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S
            NotMatchRegex<T>(
                this PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S step,
                string pattern)
        {
            var rule = NotMatchRegexRuleFactory.Create<T>(pattern);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S
            NotMatchRegex<T>(
                this PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S step,
                Regex regex)
        {
            var rule = NotMatchRegexRuleFactory.Create<T>(regex);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            Length<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                int min,
                int max)
            where TValue : IEnumerable
        {
            var rule = new LengthRule<T, TValue>(min, max);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            MinLength<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                int min)
            where TValue : IEnumerable
        {
            var rule = new MinLengthRule<T, TValue>(min);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            MaxLength<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                int max)
            where TValue : IEnumerable
        {
            var rule = new MaxLengthRule<T, TValue>(max);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            IsInEnum<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                Type enumType,
                bool? flagged = null)
        {
            var rule = IsInEnumRuleFactory.Create<T, TValue>(enumType,
                flagged);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            IsInEnum<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                bool? flagged = null)
        {
            var rule = IsInEnumRuleFactory.Create<T, TValue>(typeof(TValue),
                flagged);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S
            IsInEnumName<T>(
                this PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S step,
                Type enumType)
        {
            var rule = IsInEnumRuleFactory.Create<T, string>(enumType, false);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            ScalePrecision<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                int scale,
                int precision)
            where T : struct
        {
            var rule = ScalePrecisionRuleFactory.Create<T, TValue>(scale, precision);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            Or<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                params Func<PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S,
                        PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S>[]
                    ruleFactories)
        {
            var builder = new ValidationRuleGroupBuilder<T>(step.Build());
            var anyBuilder = builder.GetBuilder(ValidationRuleRelation.Any);
            var pBuilder =
                new PropertyValidationRuleBuilder<T, TValue>(anyBuilder).GetBuilder(step.GetPropertyExpression());
            foreach (var func in ruleFactories)
            {
                pBuilder = func.Invoke(pBuilder);
            }

            _ = pBuilder.Build();
            return step;
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            Not<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                Func<PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S,
                        PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S>
                    notFunc)
        {
            var builder = new ValidationRuleGroupBuilder<T>(step.Build());
            var anyBuilder = builder.GetBuilder(ValidationRuleRelation.Not);
            var pBuilder =
                new PropertyValidationRuleBuilder<T, TValue>(anyBuilder).GetBuilder(step.GetPropertyExpression());
            pBuilder = notFunc.Invoke(pBuilder).Next();
            _ = pBuilder.Build();
            return step;
        }
    }
}