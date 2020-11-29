namespace Newbe.ObjectVisitor.Validator
{
    public interface IValidationBlockExpressionFactory
    {
        IValidationBlockExpressionFactoryHandler Create<T>(ValidationRuleGroup<T> ruleGroup);
    }
}