using System;

namespace Newbe.ObjectVisitor.Validation
{
    internal static class IsInEnumRuleFactory
    {
        public static IsInEnumRule<T, TValue> Create<T, TValue>(
            Type enumType,
            bool? flagged = null)
        {
            return new IsInEnumRule<T, TValue>(enumType,
                flagged);
        }
    }
}