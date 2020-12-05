using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor.Validation
{
    internal abstract class PropertyValidationRuleBase<T, TValue> : IPropertyValidationRule<T, TValue>
    {
        public Expression<Func<TValue, bool>> MustExpression { get; protected set; } = null!;
        public Expression<Func<T, TValue, PropertyInfo, string>> ErrorMessageExpression { get; protected set; } = null!;
    }
}