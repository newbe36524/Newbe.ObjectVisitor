namespace Newbe.ObjectVisitor.Validation
{
    internal interface IValidationBlockExpressionFactory
    {
        IValidationBlockExpressionFactoryHandler Create<T>(ValidationRuleGroup<T> ruleGroup);
    }
}