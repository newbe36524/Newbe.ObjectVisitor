using System;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validation
{
    internal class IfValidationBlockExpressionFactoryHandler<T> : IValidationBlockExpressionFactoryHandler
    {
        private readonly IValidationBlockExpressionFactoryHandler _handler;
        private readonly Expression<Func<T, bool>> _ifExpression;

        public IfValidationBlockExpressionFactoryHandler(
            IValidationBlockExpressionFactoryHandler handler,
            Expression<Func<T, bool>> ifExpression)
        {
            _handler = handler;
            _ifExpression = ifExpression;
        }

        public Expression Create(ICreateValidationBlockInput input)
        {
            return Expression.IfThen(_ifExpression.Unwrap(input.InputExpression), _handler.Create(input));
        }
    }
}