using System.Collections.Generic;

namespace Newbe.ObjectVisitor.Validation
{
    public class ValidationRuleGroup<T> : List<ValidationRule<T>>
    {
        public ValidationRuleRelation RuleRelation { get; set; } = ValidationRuleRelation.All;
    }
}