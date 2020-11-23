using System;
using System.Collections;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public class MaxLengthRule<T, TValue> : LengthRuleBase<T, TValue>
        where TValue : IEnumerable
    {
        public MaxLengthRule(
            int max)
        {
            ErrorMessageExp = (name, length) =>
                $"Length of value named {name} must <= {max}, but found {length}";
            LengthCompareExp = length => length <= max;
            Init();
        }

        protected override Expression<Func<string, int, string>> ErrorMessageExp { get; }

        protected override Expression<Func<int, bool>> LengthCompareExp { get; }
    }
}