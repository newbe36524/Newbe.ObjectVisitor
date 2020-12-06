namespace Newbe.ObjectVisitor.Validation
{
    /// <summary>
    /// Relation of validation rules
    /// </summary>
    public enum ValidationRuleRelation
    {
        /// <summary>
        /// All success. It is as All() in linq.
        /// </summary>
        And = 0,

        /// <summary>
        /// Any success. It is as Any() in linq.
        /// </summary>
        Or = 1,

        /// <summary>
        /// Negation of rules
        /// </summary>
        Not = 2,
    }
}