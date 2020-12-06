using System;
using System.Collections;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validation
{
    internal class MinLengthRule<T, TValue> : LengthRuleBase<T, TValue>
        where TValue : IEnumerable
    {
        public MinLengthRule(
            int min)
        {
            ErrorMessageExp = (name, length) =>
                $"Length of value named {name} must >= {min}, but found {length}";
            LengthCompareExp = length => length >= min;
            Init();
        }

        protected override Expression<Func<string, int, string>> ErrorMessageExp { get; }

        protected override Expression<Func<int, bool>> LengthCompareExp { get; }
    }
}