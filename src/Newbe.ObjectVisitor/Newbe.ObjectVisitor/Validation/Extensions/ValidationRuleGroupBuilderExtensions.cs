using System;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validation
{
    public static class ValidationRuleGroupBuilderExtensions
    {
        public static ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S Validate<T>(
            this ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S source,
            Expression<Func<T, bool>> func)
        {
            return source.Next()
                .Validate(func);
        }

        public static ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S Or<T>(
            this ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S source,
            params Func<ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S,
                ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S>[] orFunc)
        {
            var anyBuilder =
                new ValidationRuleGroupBuilder<T>(source.Build()).GetBuilder(ValidationRuleRelation.Any);
            var t = anyBuilder;
            foreach (var func in orFunc)
            {
                t = func.Invoke(t).Next();
            }

            t.Build();
            return source;
        }

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