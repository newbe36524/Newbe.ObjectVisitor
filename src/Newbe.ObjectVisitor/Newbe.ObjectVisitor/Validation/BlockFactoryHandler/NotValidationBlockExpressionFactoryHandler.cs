using System;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validation
{
    public class NotValidationBlockExpressionFactoryHandler<T> : IValidationBlockExpressionFactoryHandler
    {
        private readonly ValidationRule<T> _rule;
        private readonly Expression<Func<T, string>> _errorMessageExpression;

        public NotValidationBlockExpressionFactoryHandler(
            ValidationRule<T> rule,
            Expression<Func<T, string>> errorMessageExpression)
        {
            _rule = rule;
            _errorMessageExpression = errorMessageExpression;
        }

        public Expression Create(ICreateValidationBlockInput input)
        {
            var mustValueExp = Expression.Not(_rule.MustExpression.Unwrap(input.InputExpression));
            var errorExp = _errorMessageExpression.Unwrap(input.InputExpression);
            return Expression.IfThen(Expression.Not(mustValueExp),
                input.ErrorAdd(errorExp));
        }
    }
}