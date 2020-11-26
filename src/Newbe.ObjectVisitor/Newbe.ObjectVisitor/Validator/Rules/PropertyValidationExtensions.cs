using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newbe.ObjectVisitor.Validator.Rules;

// ReSharper disable once CheckNamespace
namespace Newbe.ObjectVisitor.Validator
{
    public static class PropertyValidationExtensions
    {
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            Equal<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                TValue expected)
            where TValue : IEquatable<TValue>
        {
            var rule = EqualRuleFactory.Create<T, TValue>(expected);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            Equal<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                TValue expected,
                IEqualityComparer<TValue> comparer)
        {
            var rule = EqualRuleFactory.Create<T, TValue>(expected, comparer);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            NotEqual<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                TValue expected)
            where TValue : IEquatable<TValue>
        {
            var rule = NotEqualRuleFactory.Create<T, TValue>(expected);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            NotEqual<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                TValue expected,
                IEqualityComparer<TValue> comparer)
        {
            var rule = NotEqualRuleFactory.Create<T, TValue>(expected, comparer);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            LessThan<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                TValue max)
            where TValue : IComparable<TValue>
        {
            var rule = LessThanRuleFactory.Create<T, TValue>(max);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            LessThan<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                TValue max,
                IComparer<TValue> comparer)
        {
            var rule = LessThanRuleFactory.Create<T, TValue>(max, comparer);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            LessThanOrEqual<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                TValue max)
            where TValue : IComparable<TValue>
        {
            var rule = LessThanOrEqualRuleFactory.Create<T, TValue>(max);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            LessThanOrEqual<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                TValue max,
                IComparer<TValue> comparer)
        {
            var rule = LessThanOrEqualRuleFactory.Create<T, TValue>(max, comparer);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            GreaterThan<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                TValue min)
            where TValue : IComparable<TValue>
        {
            var rule = GreaterThanRuleFactory.Create<T, TValue>(min);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            GreaterThan<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                TValue min,
                IComparer<TValue> comparer)
        {
            var rule = GreaterThanRuleFactory.Create<T, TValue>(min, comparer);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            GreaterThanOrEqual<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                TValue min)
            where TValue : IComparable<TValue>
        {
            var rule = GreaterThanOrEqualRuleFactory.Create<T, TValue>(min);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            GreaterThanOrEqual<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                TValue min,
                IComparer<TValue> comparer)
        {
            var rule = GreaterThanOrEqualRuleFactory.Create<T, TValue>(min, comparer);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            IsInRange<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                TValue min,
                TValue max,
                bool excludeMin = false,
                bool excludeMax = true)
            where TValue : IComparable<TValue>
        {
            var rule = IsInRangeRuleFactory.Create<T, TValue>(min, max, excludeMin, excludeMax);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            IsInRange<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                TValue min,
                TValue max,
                IComparer<TValue> comparer,
                bool excludeMin = false,
                bool excludeMax = true)
        {
            var rule = IsInRangeRuleFactory.Create<T, TValue>(min, max, excludeMin, excludeMax, comparer);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            IsInSet<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                IEnumerable<TValue> expectedSet)
            where TValue : IEquatable<TValue>
        {
            var rule = IsInSetRuleFactory.Create<T, TValue>(expectedSet);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            IsInSet<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                IEnumerable<TValue> expectedSet,
                IEqualityComparer<TValue> comparer)
        {
            var rule = IsInSetRuleFactory.Create<T, TValue>(expectedSet, comparer);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            IsNotInSet<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                IEnumerable<TValue> expectedSet)
            where TValue : IEquatable<TValue>
        {
            var rule = IsNotInSetRuleFactory.Create<T, TValue>(expectedSet);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            IsNotInSet<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                IEnumerable<TValue> expectedSet,
                IEqualityComparer<TValue> comparer)
        {
            var rule = IsNotInSetRuleFactory.Create<T, TValue>(expectedSet, comparer);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            Null<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step)
            where TValue : class
        {
            var rule = new ClassNullRule<T, TValue>();
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            NotNull<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step)
            where TValue : class
        {
            var rule = new ClassNotNullRule<T, TValue>();
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_V
            NotEmpty<T>(
                this IPropertyValidationRuleBuilderValidateStep<T, string> step)
        {
            var rule = new StringNotEmptyRule<T>();
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_V
            Empty<T>(
                this IPropertyValidationRuleBuilderValidateStep<T, string> step)
        {
            var rule = new StringEmptyRule<T>();
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            NotEmpty<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step)
            where TValue : IEnumerable
        {
            var rule = new EnumerableNotEmptyRule<T, TValue>();
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            Empty<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step)
            where TValue : IEnumerable
        {
            var rule = new EnumerableEmptyRule<T, TValue>();
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_V
            MatchRegex<T>(
                this IPropertyValidationRuleBuilderValidateStep<T, string> step,
                string pattern)
        {
            var rule = MatchRegexRuleFactory.Create<T>(pattern);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_V
            MatchRegex<T>(
                this IPropertyValidationRuleBuilderValidateStep<T, string> step,
                Regex regex)
        {
            var rule = MatchRegexRuleFactory.Create<T>(regex);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_V
            NotMatchRegex<T>(
                this IPropertyValidationRuleBuilderValidateStep<T, string> step,
                string pattern)
        {
            var rule = NotMatchRegexRuleFactory.Create<T>(pattern);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_V
            NotMatchRegex<T>(
                this IPropertyValidationRuleBuilderValidateStep<T, string> step,
                Regex regex)
        {
            var rule = NotMatchRegexRuleFactory.Create<T>(regex);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            Length<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                int min,
                int max)
            where TValue : IEnumerable
        {
            var rule = new LengthRule<T, TValue>(min, max);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            MinLength<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                int min)
            where TValue : IEnumerable
        {
            var rule = new MinLengthRule<T, TValue>(min);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            MaxLength<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                int max)
            where TValue : IEnumerable
        {
            var rule = new MaxLengthRule<T, TValue>(max);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            IsInEnum<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                Type enumType,
                bool? flagged = null)
        {
            var rule = IsInEnumRuleFactory.Create<T, TValue>(enumType,
                flagged);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            IsInEnum<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                bool? flagged = null)
        {
            var rule = IsInEnumRuleFactory.Create<T, TValue>(typeof(TValue),
                flagged);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_V
            IsInEnumName<T>(
                this IPropertyValidationRuleBuilderValidateStep<T, string> step,
                Type enumType)
        {
            var rule = IsInEnumRuleFactory.Create<T, string>(enumType, false);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            ScalePrecision<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                int scale,
                int precision)
            where T : struct
        {
            var rule = ScalePrecisionRuleFactory.Create<T, TValue>(scale, precision);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            Or<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                params Func<IPropertyValidationRuleBuilderValidateStep<T, TValue>, IPropertyValidationRule<T, TValue>>[]
                    ruleFactories)
        {
            var rules = ruleFactories.Select(x => x.Invoke(step)).ToArray();
            var rule = new OrRule<T, TValue>(rules);
            return step.Validate(rule);
        }
    }
}