using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor.Validation
{
    public interface IPropertyValidationRule<T, TValue>
    {
        Expression<Func<TValue, bool>> MustExpression { get; }

        Expression<Func<T, TValue, PropertyInfo, string>> ErrorMessageExpression { get; }
    }
}