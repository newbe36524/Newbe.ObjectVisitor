using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor.Validation
{
    /// <summary>
    /// Extension of property validation fluent API
    /// </summary>
    public static class PropertyValidationRuleBuilderExtensions
    {
        /// <summary>
        /// Add a new validation rule
        /// </summary>
        /// <param name="step"></param>
        /// <param name="func">New validation func</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S Validate<T, TValue>(
            this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
            Expression<Func<TValue, bool>> func)
        {
            var finalExp = CreateFullExp<T, TValue, bool>((inputExp, valueExp, pExp) => func.Unwrap(valueExp));
            return step.Validate(finalExp);
        }

        /// <summary>
        /// Switch to a new property to validate it
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyExpression"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S Property<T, TValue>(
            this ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S source,
            Expression<Func<T, TValue>> propertyExpression)
        {
            var builder = new ValidationRuleGroupBuilder<T>(source.Build());
            return new PropertyValidationRuleBuilder<T, TValue>(builder)
                .GetBuilder(propertyExpression);
        }

        /// <summary>
        /// Add a new validation rule
        /// </summary>
        /// <param name="step"></param>
        /// <param name="rule">New validation rule</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
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