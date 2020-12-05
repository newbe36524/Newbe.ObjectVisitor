using System;
using System.Collections.Generic;

namespace Newbe.ObjectVisitor.Validation
{
    /// <summary>
    /// Extensions of validator
    /// </summary>
    public static class OvBuilderExtensions
    {
        /// <summary>
        /// Create a validator from object visitor builder
        /// </summary>
        /// <param name="ovBuilder">Object visitor builder</param>
        /// <param name="groups">Validation rule groups</param>
        /// <typeparam name="T">Type of value to be validated</typeparam>
        /// <returns></returns>
        public static IValidator<T> Validate<T>(this OVBuilder<T>.IOVBuilder_V ovBuilder,
            List<ValidationRuleGroup<T>> groups)
        {
            return new Validator<T>(groups);
        }

        /// <summary>
        /// Create a validator from object visitor builder
        /// </summary>
        /// <param name="ovBuilder">Object visitor builder</param>
        /// <param name="buildRuleAction">Action for building validation rule group created by fluent API</param>
        /// <typeparam name="T">Type of value to be validated</typeparam>
        /// <returns></returns>
        public static IValidator<T> Validate<T>(this OVBuilder<T>.IOVBuilder_V ovBuilder,
            Action<ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S> buildRuleAction)
        {
            var builder = ValidateRule<T>.GetBuilder();
            buildRuleAction(builder);
            var rules = builder.Build();
            return ovBuilder.Validate(rules);
        }

        /// <summary>
        /// Run a validator with specified value
        /// </summary>
        /// <param name="validator">Object validator</param>
        /// <param name="value">Value to be validated</param>
        /// <typeparam name="T">Type of <paramref name="value"/></typeparam>
        /// <returns></returns>
        public static IValidationResult<T> Run<T>(this IValidator<T> validator, T value)
        {
            return validator.Validate(value);
        }
    }
}