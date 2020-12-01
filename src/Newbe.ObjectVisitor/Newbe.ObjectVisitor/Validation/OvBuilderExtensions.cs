using System;
using System.Collections.Generic;

namespace Newbe.ObjectVisitor.Validation
{
    public static class OvBuilderExtensions
    {
        public static IValidator<T> Validate<T>(this OVBuilder<T>.IOVBuilder_V ovBuilder,
            List<ValidationRuleGroup<T>> groups)
        {
            return new Validator<T>(groups);
        }

        public static IValidator<T> Validate<T>(this OVBuilder<T>.IOVBuilder_V ovBuilder,
            Action<ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S> buildRuleAction)
        {
            var builder = ValidateRule<T>.GetBuilder();
            buildRuleAction(builder);
            var rules = builder.Build();
            return ovBuilder.Validate(rules);
        }

        public static IValidationResult<T> Run<T>(this IValidator<T> validator, T value)
        {
            return validator.Validate(value);
        }
    }
}