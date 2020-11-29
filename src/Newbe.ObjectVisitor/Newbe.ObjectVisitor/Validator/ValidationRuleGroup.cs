using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator
{
    public class ValidationRuleGroup<T> : List<ValidationRule<T>>
    {
        public ValidationRuleRelation RuleRelation { get; set; } = ValidationRuleRelation.All;
    }
}