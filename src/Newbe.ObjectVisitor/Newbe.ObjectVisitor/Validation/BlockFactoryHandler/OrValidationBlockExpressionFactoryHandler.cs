using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validation
{
    public class OrValidationBlockExpressionFactoryHandler<T> : IValidationBlockExpressionFactoryHandler
    {
        private readonly ValidationRule<T>[] _blocks;

        public OrValidationBlockExpressionFactoryHandler(
            IEnumerable<ValidationRule<T>> blocks)
        {
            _blocks = blocks.ToArray();
        }

        public Expression Create(ICreateValidationBlockInput input)
        {
            var mustValuesExpression =
                _blocks.Select(CombineIfAndMust).ToArray();
            var errorMessage = _blocks.Select(x => x.ErrorMessageExpression.Unwrap(input.InputExpression));
            var errorArrayExp = Expression.NewArrayInit(typeof(string), errorMessage);
            return Expression.IfThen(
                Expression.Not(Any.Unwrap(Expression.NewArrayInit(typeof(bool), mustValuesExpression))),
                AddErrorExpression.Unwrap(errorArrayExp, input.ErrorExpression));

            Expression CombineIfAndMust(ValidationRule<T> rule)
            {
                return rule.IfExpression == null
                    ? rule.MustExpression.Unwrap(input.InputExpression)
                    : Expression.OrElse(Expression.Not(rule.IfExpression.Unwrap(input.InputExpression)),
                        rule.MustExpression.Unwrap(input.InputExpression));
            }
        }

        private static readonly Expression<Action<string[], HashSet<string>>> AddErrorExpression =
            (errors, errorSet) =>
                errorSet!.AddRange(errors);

        private static readonly Expression<Func<bool[], bool>> Any = x => x.Any(a => a);
    }
}