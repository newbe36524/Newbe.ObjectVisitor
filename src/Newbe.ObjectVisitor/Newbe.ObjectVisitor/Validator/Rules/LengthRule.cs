using System;
using System.Collections;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public class LengthRule<T, TValue> : LengthRuleBase<T, TValue>
        where TValue : IEnumerable
    {
        public LengthRule(
            int min,
            int max)
        {
            string range = $"[{min}, {max}]";
            ErrorMessageExp = (name, length) =>
                $"Length of value named {name} must be in range {range}, but found {length}";
            LengthCompareExp = length => length >= min && length <= max;
            Init();
        }

        protected override Expression<Func<string, int, string>> ErrorMessageExp { get; }

        protected override Expression<Func<int, bool>> LengthCompareExp { get; }
    }
}