using System;
using System.Collections.Generic;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class IntNullableComparer : IComparer<int?>
    {
        public int Compare(int? x, int? y)
        {
            return Nullable.Compare(x, y);
        }
    }
}