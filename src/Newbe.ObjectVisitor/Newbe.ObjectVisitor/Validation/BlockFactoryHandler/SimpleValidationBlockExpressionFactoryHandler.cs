using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validation
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
            var checkExp = _rule.IfExpression == null
                ? mustValueExp
                : Expression.OrElse(Expression.Not(_rule.IfExpression.Unwrap(input.InputExpression)),
                    mustValueExp);
            return Expression.IfThen(Expression.Not(checkExp),
                input.ErrorAdd(errorExp));
        }
    }
}