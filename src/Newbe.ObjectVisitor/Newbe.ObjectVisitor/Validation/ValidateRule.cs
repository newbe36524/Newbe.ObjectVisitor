using System.Collections.Generic;

namespace Newbe.ObjectVisitor.Validation
{
    public static class ValidateRule<T>
    {
        public static ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S GetBuilder()
        {
            return new ValidationRuleGroupBuilder<T>(new List<ValidationRuleGroup<T>>());
        }
    }

    public static class ValidateRule
    {
        public static ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S GetBuilder<T>(T obj)
        {
            return new ValidationRuleGroupBuilder<T>(new List<ValidationRuleGroup<T>>());
        }
    }
}