using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validation
{
    internal static class RuleExpressionHelper
    {
        public static Expression<Func<TValue, bool>> Equal<TValue>(TValue expected)
            where TValue : IEquatable<TValue>
        {
            Expression<Func<TValue, bool>> re = value => value.Equals(expected);
            return re;
        }

        public static Expression<Func<TValue, bool>> Equal<TValue>(TValue expected,
            IEqualityComparer<TValue> comparer)
        {
            Expression<Func<TValue, bool>> re = value => comparer.Equals(value, expected);
            return re;
        }

        public static Expression<Func<TValue, bool>> NotEqual<TValue>(TValue expected)
            where TValue : IEquatable<TValue>
        {
            Expression<Func<TValue, bool>> re = value => !value.Equals(expected);
            return re;
        }

        public static Expression<Func<TValue, bool>> NotEqual<TValue>(TValue expected,
            IEqualityComparer<TValue> comparer)
        {
            Expression<Func<TValue, bool>> re = value => !comparer.Equals(value, expected);
            return re;
        }

        public static Expression<Func<TValue, bool>> Greater<TValue>(TValue min, bool excludeMin)
            where TValue : IComparable<TValue>
        {
            Expression<Func<TValue, bool>> re;
            if (excludeMin)
            {
                re = x => x.CompareTo(min) > 0;
            }
            else
            {
                re = x => x.CompareTo(min) >= 0;
            }

            return re;
        }

        public static Expression<Func<TValue, bool>> Greater<TValue>(TValue min,
            bool excludeMin,
            IComparer<TValue> comparer)
        {
            Expression<Func<TValue, bool>> re;
            if (excludeMin)
            {
                re = x => comparer.Compare(x, min) > 0;
            }
            else
            {
                re = x => comparer.Compare(x, min) >= 0;
            }

            return re;
        }


        public static Expression<Func<TValue, bool>> Less<TValue>(TValue max, bool excludeMax)
            where TValue : IComparable<TValue>
        {
            Expression<Func<TValue, bool>> re;
            if (excludeMax)
            {
                re = x => x.CompareTo(max) < 0;
            }
            else
            {
                re = x => x.CompareTo(max) <= 0;
            }

            return re;
        }

        public static Expression<Func<TValue, bool>> Less<TValue>(TValue max,
            bool excludeMax,
            IComparer<TValue> comparer)
        {
            Expression<Func<TValue, bool>> re;
            if (excludeMax)
            {
                re = x => comparer.Compare(x, max) < 0;
            }
            else
            {
                re = x => comparer.Compare(x, max) <= 0;
            }

            return re;
        }
    }
}