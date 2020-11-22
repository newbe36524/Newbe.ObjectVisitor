using System;
using System.Collections.Generic;
using Newbe.ObjectVisitor.Validator.Rules;

// ReSharper disable once CheckNamespace
namespace Newbe.ObjectVisitor.Validator
{
    public static class RangeExtensions
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
                ICollection<TValue> expectedSet)
            where TValue : IEquatable<TValue>
        {
            var rule = new IsInSetRule<T, TValue>(expectedSet);
            return step.Validate(rule);
        }
    }
}