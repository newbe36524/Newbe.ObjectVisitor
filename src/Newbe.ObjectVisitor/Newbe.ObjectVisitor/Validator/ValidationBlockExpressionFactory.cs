using System;
using System.Linq;

namespace Newbe.ObjectVisitor.Validator
{
    public class ValidationBlockExpressionFactory : IValidationBlockExpressionFactory
    {
        public IValidationBlockExpressionFactoryHandler Create<T>(ValidationRuleGroup<T> ruleGroup)
        {
            return ruleGroup.RuleRelation switch
            {
                ValidationRuleRelation.All => new AndValidationBlockExpressionFactoryHandler<T>(ruleGroup),
                ValidationRuleRelation.Any => new OrValidationBlockExpressionFactoryHandler<T>(ruleGroup),
                ValidationRuleRelation.Not =>
                    // TODO message
                    new NotValidationBlockExpressionFactoryHandler<T>(ruleGroup.Single(), x => "Error"),
                _ => new SimpleValidationBlockExpressionFactoryHandler<T>(ruleGroup.Single())
            };
        }
    }
}