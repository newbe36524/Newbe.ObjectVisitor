using System.Collections.Generic;

namespace Newbe.ObjectVisitor.Validation
{
    /// <summary>
    /// Helper class to create <see cref="ValidationRule{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ValidationRuleBuilder<T>
    {
        /// <summary>
        /// Create a builder
        /// </summary>
        /// <returns></returns>
        public static ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S GetBuilder()
        {
            return new ValidationRuleGroupBuilder<T>(new List<ValidationRuleGroup<T>>());
        }
    }

    /// <summary>
    /// Helper class to create <see cref="ValidationRule{T}"/>
    /// </summary>
    public static class ValidationRuleBuilder
    {
        /// <summary>
        /// Create a builder
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S GetBuilder<T>(T obj)
        {
            return new ValidationRuleGroupBuilder<T>(new List<ValidationRuleGroup<T>>());
        }
    }
}