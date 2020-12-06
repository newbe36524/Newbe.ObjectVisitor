using System;
using System.Collections.Generic;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class IntNullableEqualityComparer : IEqualityComparer<int?>
    {
        public bool Equals(int? x, int? y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(int? obj)
        {
            return obj.GetHashCode();
        }
    }
}