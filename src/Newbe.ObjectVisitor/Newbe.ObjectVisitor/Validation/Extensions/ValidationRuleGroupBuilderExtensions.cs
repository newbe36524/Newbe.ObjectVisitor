using System;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validation
{
    /// <summary>
    /// Extensions of validation rule group building
    /// </summary>
    public static class ValidationRuleGroupBuilderExtensions
    {
        /// <summary>
        /// Add a new validation rule
        /// </summary>
        /// <param name="source"></param>
        /// <param name="func">New validation func</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S Validate<T>(
            this ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S source,
            Expression<Func<T, bool>> func)
        {
            return source.Next()
                .Validate(func);
        }

        /// <summary>
        /// Create a new validation group with <see cref="ValidationRuleRelation.Or"/> relation
        /// </summary>
        /// <param name="source"></param>
        /// <param name="orFunc">Func to create inner rules in this group</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S Or<T>(
            this ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S source,
            params Func<ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S,
                ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S>[] orFunc)
        {
            var anyBuilder =
                new ValidationRuleGroupBuilder<T>(source.Build()).GetBuilder(ValidationRuleRelation.Or);
            var t = anyBuilder;
            foreach (var func in orFunc)
            {
                t = func.Invoke(t).Next();
            }

            t.Build();
            return source;
        }

        /// <summary>
        /// Create a new validation group with <see cref="ValidationRuleRelation.Not"/> relation
        /// </summary>
        /// <param name="source"></param>
        /// <param name="notFunc">Func to create inner rule in this group</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S
            Not<T>(
                this ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S source,
                Func<ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S,
                    ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S> notFunc)
        {
            var pBuilder =
                new ValidationRuleGroupBuilder<T>(source.Build()).GetBuilder(ValidationRuleRelation.Not);
            pBuilder = notFunc.Invoke(pBuilder);
            pBuilder.Build();
            return source;
        }
    }
}