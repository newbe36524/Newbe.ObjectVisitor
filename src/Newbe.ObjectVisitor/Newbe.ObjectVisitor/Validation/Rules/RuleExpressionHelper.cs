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
            Expression<Func<TValue, bool>> re = value => expected.Equals(value);
            return re;
        }

        public static Expression<Func<TValue?, bool>> Equal<TValue>(TValue? expected)
            where TValue : struct, IEquatable<TValue>
        {
            Expression<Func<TValue?, bool>> re = value => expected.Equals(value);
            return re;
        }

        public static Expression<Func<TValue, bool>> Equal<TValue>(TValue expected,
            IEqualityComparer<TValue> comparer)
        {
            Expression<Func<TValue, bool>> re = value => comparer.Equals(value, expected);
            return re;
        }

        public static Expression<Func<TValue?, bool>> Equal<TValue>(TValue? expected,
            IEqualityComparer<TValue?> comparer)
            where TValue : struct
        {
            Expression<Func<TValue?, bool>> re = value => comparer.Equals(value, expected);
            return re;
        }

        public static Expression<Func<TValue, bool>> NotEqual<TValue>(TValue expected)
            where TValue : IEquatable<TValue>
        {
            Expression<Func<TValue, bool>> re = value => !expected.Equals(value);
            return re;
        }

        public static Expression<Func<TValue?, bool>> NotEqual<TValue>(TValue? expected)
            where TValue : struct, IEquatable<TValue>
        {
            Expression<Func<TValue?, bool>> re = value => !expected.Equals(value);
            return re;
        }


        public static Expression<Func<TValue, bool>> NotEqual<TValue>(TValue expected,
            IEqualityComparer<TValue> comparer)
        {
            Expression<Func<TValue, bool>> re = value => !comparer.Equals(value, expected);
            return re;
        }

        public static Expression<Func<TValue?, bool>> NotEqual<TValue>(TValue? expected,
            IEqualityComparer<TValue?> comparer)
            where TValue : struct
        {
            Expression<Func<TValue?, bool>> re = value => !comparer.Equals(value, expected);
            return re;
        }

        public static Expression<Func<TValue, bool>> Greater<TValue>(TValue min, bool excludeMin)
            where TValue : IComparable<TValue>
        {
            Expression<Func<TValue, bool>> re;
            if (excludeMin)
            {
                re = x => min.CompareTo(x) < 0;
            }
            else
            {
                re = x => min.CompareTo(x) <= 0;
            }

            return re;
        }

        public static Expression<Func<TValue?, bool>> GreaterNullable<TValue>(TValue min, bool excludeMin)
            where TValue : struct, IComparable<TValue>
        {
            Expression<Func<TValue?, bool>> re;
            if (excludeMin)
            {
                re = x => x.HasValue && min.CompareTo(x.Value) < 0;
            }
            else
            {
                re = x => x.HasValue && min.CompareTo(x.Value) <= 0;
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
                re = x => max.CompareTo(x) > 0;
            }
            else
            {
                re = x => max.CompareTo(x) >= 0;
            }

            return re;
        }

        public static Expression<Func<TValue?, bool>> LessNullable<TValue>(TValue max, bool excludeMax)
            where TValue : struct, IComparable<TValue>
        {
            Expression<Func<TValue?, bool>> re;
            if (excludeMax)
            {
                re = x => x.HasValue && max.CompareTo(x.Value) > 0;
            }
            else
            {
                re = x => x.HasValue && max.CompareTo(x.Value) >= 0;
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