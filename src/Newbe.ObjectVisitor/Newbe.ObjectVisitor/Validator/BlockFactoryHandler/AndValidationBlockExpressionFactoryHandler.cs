using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator
{
    public class AndValidationBlockExpressionFactoryHandler<T> : IValidationBlockExpressionFactoryHandler
    {
        private readonly ValidationRule<T>[] _blocks;

        public AndValidationBlockExpressionFactoryHandler(
            IEnumerable<ValidationRule<T>> blocks)
        {
            _blocks = blocks.ToArray();
        }

        public Expression Create(ICreateValidationBlockInput input)
        {
            var mustResultsExp = Expression.Variable(typeof(bool[]), "mustResult");
            var blockExp = Expression.Block(new[] {mustResultsExp}, GetBlockItems());
            return blockExp;

            IEnumerable<Expression> GetBlockItems()
            {
                var mustValuesExpression =
                    _blocks.Select(CombineIfAndMust).ToArray();
                yield return Expression.Assign(mustResultsExp,
                    Expression.NewArrayInit(typeof(bool), mustValuesExpression));

                var errorMessage = _blocks.Select(x => x.ErrorMessageExpression.Unwrap(input.InputExpression));
                var errorArrayExp = Expression.NewArrayInit(typeof(string), errorMessage);
                yield return Expression.IfThen(Expression.Not(All.Unwrap(mustResultsExp)),
                    AddErrorExpression.Unwrap(mustResultsExp, errorArrayExp, input.ErrorExpression));

                Expression CombineIfAndMust(ValidationRule<T> rule)
                {
                    return rule.IfExpression == null
                        ? rule.MustExpression.Unwrap(input.InputExpression)
                        : Expression.OrElse(Expression.Not(rule.IfExpression.Unwrap(input.InputExpression)),
                            rule.MustExpression.Unwrap(input.InputExpression));
                }
            }
        }

        private static readonly Expression<Action<bool[], string[], HashSet<string>>> AddErrorExpression =
            (results, errors, errorSet) =>
                errorSet!.AddRange(results.Select((x, i) => !x ? errors[i] : null).Where(x => x != null));

        private static readonly Expression<Func<bool[], bool>> All = x => x.All(a => a);
    }
}