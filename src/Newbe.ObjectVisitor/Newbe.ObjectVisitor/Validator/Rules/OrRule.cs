using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public class OrRule<T, TValue> : PropertyValidationRuleBase<T, TValue>
    {
        public OrRule(
            IEnumerable<IPropertyValidationRule<T, TValue>> rules)
        {
            var rulesArray = rules.ToArray();

            CreateMustExpression(rulesArray);

            CreateErrorMessageExpression(rulesArray);
        }

        private void CreateErrorMessageExpression(IPropertyValidationRule<T, TValue>[] rulesArray)
        {
            var inputExp = Expression.Parameter(typeof(T), "input");
            var valueExp = Expression.Parameter(typeof(TValue), "value");
            var pExp = Expression.Parameter(typeof(PropertyInfo), "p");

            var validateResults = rulesArray.Select(x => x.MustExpression.Unwrap(valueExp))
                .ToArray();

            var errorMessageResults = rulesArray.Select(x => x.ErrorMessageExpression.Unwrap(inputExp, valueExp, pExp))
                .ToArray();

            Expression<Func<bool[], string[], string>> messageJoiner = (results, messages) =>
                string.Join(",", messages.Select((s, i) => results[i] ? s : null).Where(x => x != null));

            var bodyExp = messageJoiner.Unwrap(Expression.NewArrayInit(typeof(bool), validateResults),
                Expression.NewArrayInit(typeof(string), errorMessageResults));
            ErrorMessageExpression =
                Expression.Lambda<Func<T, TValue, PropertyInfo, string>>(bodyExp, inputExp, valueExp, pExp);
        }

        private void CreateMustExpression(IEnumerable<IPropertyValidationRule<T, TValue>> rulesArray)
        {
            var pExp = Expression.Parameter(typeof(TValue), "value");
            var validateExps = rulesArray.Select(rule => rule.MustExpression.Unwrap(pExp))
                .ToArray();
            var seed = validateExps.First();
            var left = validateExps.Skip(1).ToArray();
            seed = left.Aggregate(seed, Expression.OrElse);
            MustExpression = Expression.Lambda<Func<TValue, bool>>(seed, pExp);
        }
    }
}