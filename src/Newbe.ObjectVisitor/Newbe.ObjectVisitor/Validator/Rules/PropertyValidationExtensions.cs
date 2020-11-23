using System;
using System.Collections;
using System.Collections.Generic;
using Newbe.ObjectVisitor.Validator.Rules;

// ReSharper disable once CheckNamespace
namespace Newbe.ObjectVisitor.Validator
{
    public static class PropertyValidationExtensions
    {
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            LessThan<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                TValue max)
            where TValue : IComparable<TValue>
        {
            var rule = new LessThanRule<T, TValue>(max);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            LessThanOrEqual<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                TValue max)
            where TValue : IComparable<TValue>
        {
            var rule = new LessThanOrEqual<T, TValue>(max);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            GreaterThan<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                TValue max)
            where TValue : IComparable<TValue>
        {
            var rule = new GreaterThan<T, TValue>(max);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            GreaterThanOrEqual<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                TValue max)
            where TValue : IComparable<TValue>
        {
            var rule = new GreaterThanOrEqual<T, TValue>(max);
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
            var rule = new IsInRangeRule<T, TValue>(min, max, excludeMin, excludeMax);
            return step.Validate(rule);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
            IsInSet<T, TValue>(
                this IPropertyValidationRuleBuilderValidateStep<T, TValue> step,
                IEnumerable<TValue> expectedSet)
            where TValue : IEquatable<TValue>
        {
            var rule = new IsInSetRule<T, TValue>(expectedSet);
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
    }
}