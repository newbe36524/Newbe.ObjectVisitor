using System;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator
{
    public static class ValidationRuleGroupBuilderExtensions
    {
        public static ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_V Validate<T>(
            this ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_V source,
            Expression<Func<T, bool>> func)
        {
            return source.Next()
                .Validate(func);
        }

        public static ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S Or<T>(
            this ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S source,
            params Func<ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S,
                ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_V>[] orFunc)
        {
            var anyBuilder =
                new ValidationRuleGroupBuilder<T>(source.GetRuleSet()).GetBuilder(ValidationRuleRelation.Any);
            var t = anyBuilder;
            foreach (var func in orFunc)
            {
                t = func.Invoke(t).Next();
            }

            return t;
        }

        public static ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S Or<T>(
            this ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_V source,
            params Func<ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S,
                ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_V>[] orFunc)
        {
            return source.Next().Or(orFunc);
        }
    }
}