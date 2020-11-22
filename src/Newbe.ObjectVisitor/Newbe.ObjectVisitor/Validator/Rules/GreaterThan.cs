﻿using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public class GreaterThan<T, TValue> : ValueRangeRuleBase<T, TValue>
        where TValue : IComparable<TValue>
    {
        public GreaterThan(TValue max)
        {
            var pExp = Expression.Parameter(typeof(TValue), "value");
            var ltExp = CreateGtExpression(pExp, max, true);
            var funcExp = Expression.Lambda<Func<TValue, bool>>(ltExp, pExp);
            MustExpression = funcExp;
            ErrorMessageExpression = (input, value, p) =>
                $"Value of {p.Name} must be > {max}, but found {value}";
        }

        public override Expression<Func<TValue, bool>> MustExpression { get; }
        public override Expression<Func<T, TValue, PropertyInfo, string>> ErrorMessageExpression { get; }
    }
}