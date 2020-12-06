using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    internal class ForEachActionContextItem : IOvBuilderContextItem
    {
        public Func<PropertyInfo, bool> PropertyInfoFilter { get; set; } = PropertyInfoFilters.AllPropertyInfo;
        public Type? ValueExpectedType { get; set; }
        public ForEachActionContextExpressionType ExpressionType { get; set; }
        public Expression ForEachAction { get; set; } = null!;

    }
}