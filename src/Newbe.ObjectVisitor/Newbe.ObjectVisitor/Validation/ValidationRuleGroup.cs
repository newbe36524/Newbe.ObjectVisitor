using System.Collections.Generic;

namespace Newbe.ObjectVisitor.Validation
{
    /// <summary>
    /// A group of <see cref="ValidationRuleBuilder{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidationRuleGroup<T> : List<ValidationRule<T>>
    {
        /// <summary>
        /// Relation of rules in the group
        /// </summary>
        public ValidationRuleRelation RuleRelation { get; set; } = ValidationRuleRelation.And;
    }
}