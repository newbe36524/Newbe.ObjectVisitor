using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Newbe.ObjectVisitor.Validator.Rules;

namespace Newbe.ObjectVisitor.Validator
{
    public static class PropertyValidationRuleBuilderExtensions
    {
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V Validate<T, TValue>(
            this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
            Expression<Func<TValue, bool>> func)
        {
            var finalExp = CreateFullExp<T, TValue, bool>((inputExp, valueExp, pExp) => func.Unwrap(valueExp));
            return step.Validate(finalExp);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S Property<T, TValue>(
            this ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S source,
            Expression<Func<T, TValue>> propertyExpression)
        {
            var builder = new ValidationRuleGroupBuilder<T>(source.GetRuleSet());
            return new PropertyValidationRuleBuilder<T, TValue>(builder)
                .GetBuilder(propertyExpression);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S Property<T, TValue>(
            this ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_V source,
            Expression<Func<T, TValue>> propertyExpression)
        {
            var step = source.Next();
            return step.Property(propertyExpression);
        }

        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S Validate<T, TValue>(
            this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
            IPropertyValidationRule<T, TValue> rule)
        {
            var mustExp = CreateFullExp<T, TValue, bool>(
                (inputExp, valueExp, pExp) => rule.MustExpression.Unwrap(valueExp));

            var errorExp = CreateFullExp<T, TValue, string>(
                (inputExp, valueExp, pExp) => rule.ErrorMessageExpression.Unwrap(inputExp, valueExp, pExp));

            return step.Validate(mustExp)
                .ErrorMessage(errorExp)
                .Next();
        }

        public static PropertyValidationRuleBuilder<T, TNewValue>.IPropertyValidationRuleBuilder_S
            Property<T, TValue, TNewValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V step,
                Expression<Func<T, TNewValue>> func)
        {
            return step.Next().Property(func);
        }

        public static List<ValidationRuleGroup<T>> GetRuleSet<T, TValue>(
            this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V step)
        {
            return step.Next().GetRuleSet();
        }

        private delegate Expression BodyExpFactory(ParameterExpression inputExp, ParameterExpression valueExp,
            ParameterExpression propertyInfoExp);

        private static Expression<Func<T, TValue, PropertyInfo, TReturn>> CreateFullExp<T, TValue, TReturn>(
            BodyExpFactory bodyFunc)
        {
            var inputExp = Expression.Parameter(typeof(T), "input");
            var valueExp = Expression.Parameter(typeof(TValue), "value");
            var pExp = Expression.Parameter(typeof(PropertyInfo), "p");
            var bodyExp = bodyFunc(inputExp, valueExp, pExp);
            var finalExp = Expression.Lambda<Func<T, TValue, PropertyInfo, TReturn>>(bodyExp, inputExp, valueExp, pExp);
            return finalExp;
        }
    }
}