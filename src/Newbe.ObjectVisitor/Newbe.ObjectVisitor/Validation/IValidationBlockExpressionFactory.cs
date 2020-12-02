namespace Newbe.ObjectVisitor.Validation
{
    public interface IValidationBlockExpressionFactory
    {
        IValidationBlockExpressionFactoryHandler Create<T>(ValidationRuleGroup<T> ruleGroup);
    }
}