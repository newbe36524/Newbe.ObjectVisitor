using System.Linq;

namespace Newbe.ObjectVisitor.Validation
{
    internal class ValidationBlockExpressionFactory : IValidationBlockExpressionFactory
    {
        public IValidationBlockExpressionFactoryHandler Create<T>(ValidationRuleGroup<T> ruleGroup)
        {
            return ruleGroup.RuleRelation switch
            {
                ValidationRuleRelation.And => new AndValidationBlockExpressionFactoryHandler<T>(ruleGroup),
                ValidationRuleRelation.Or => new OrValidationBlockExpressionFactoryHandler<T>(ruleGroup),
                ValidationRuleRelation.Not =>
                    // TODO message
                    new NotValidationBlockExpressionFactoryHandler<T>(ruleGroup.Single(), x => "Error"),
                _ => new SimpleValidationBlockExpressionFactoryHandler<T>(ruleGroup.Single())
            };
        }
    }
}