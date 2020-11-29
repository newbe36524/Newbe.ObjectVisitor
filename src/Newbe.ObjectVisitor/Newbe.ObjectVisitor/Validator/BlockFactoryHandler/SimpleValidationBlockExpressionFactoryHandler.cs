using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator
{
    public class SimpleValidationBlockExpressionFactoryHandler<T> : IValidationBlockExpressionFactoryHandler
    {
        private readonly ValidationRule<T> _rule;

        public SimpleValidationBlockExpressionFactoryHandler(
            ValidationRule<T> rule)
        {
            _rule = rule;
        }

        public Expression Create(ICreateValidationBlockInput input)
        {
            var mustValueExp = _rule.MustExpression.Unwrap(input.InputExpression);
            var errorExp = _rule.ErrorMessageExpression.Unwrap(input.InputExpression);
            // TODO support IF 
            return Expression.IfThen(Expression.Not(mustValueExp),
                input.ErrorAdd(errorExp));
        }
    }
}