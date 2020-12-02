using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor.Validation
{
    public abstract class LengthRuleBase<T, TValue> : PropertyValidationRuleBase<T, TValue>
        where TValue : IEnumerable
    {
        protected void Init()
        {
            CreateMustExpression();
            CreateErrorMessageExpression();
        }

        private void CreateErrorMessageExpression()
        {
            var inputExp = Expression.Parameter(typeof(T), "input");
            var valueExp = Expression.Parameter(typeof(TValue), "value");
            var pExp = Expression.Parameter(typeof(PropertyInfo), "p");
            var lengthExp = GetLengthExpression(valueExp);
            var bodyExp = ErrorMessageExp.Unwrap(Expression.Property(pExp, nameof(PropertyInfo.Name)),
                lengthExp);
            ErrorMessageExpression =
                Expression.Lambda<Func<T, TValue, PropertyInfo, string>>(bodyExp, inputExp, valueExp, pExp);
        }

        private void CreateMustExpression()
        {
            var pExp = Expression.Parameter(typeof(TValue), "value");
            var lengthExp = GetLengthExpression(pExp);
            var bodyExp = LengthCompareExp.Unwrap(lengthExp);
            MustExpression = Expression.Lambda<Func<TValue, bool>>(bodyExp, pExp);
        }

        private static Expression GetLengthExpression(Expression pExp)
        {
            if (typeof(TValue) == typeof(string))
            {
                return Expression.Property(pExp, nameof(string.Length));
            }

            var enumerableInterface = GetEnumerable();
            if (enumerableInterface != null)
            {
                return Expression.Call(typeof(Enumerable),
                    nameof(Enumerable.Count),
                    new[] {enumerableInterface.GenericTypeArguments.First()},
                    pExp);
            }

            Expression<Func<IEnumerable, int>> countExpression = x => x.Cast<object>().Count();
            return Expression.Invoke(countExpression, pExp);

            static Type? GetEnumerable()
            {
                var enumerableInterfaceName = typeof(IEnumerable<>).Name;
                if (typeof(TValue).Name == enumerableInterfaceName)
                {
                    return typeof(TValue);
                }

                var re = typeof(TValue).GetAllInterfaces()
                    .FirstOrDefault(x => x.Name == enumerableInterfaceName);
                return re;
            }
        }

        protected abstract Expression<Func<string, int, string>> ErrorMessageExp { get; }
        protected abstract Expression<Func<int, bool>> LengthCompareExp { get; }
    }
}