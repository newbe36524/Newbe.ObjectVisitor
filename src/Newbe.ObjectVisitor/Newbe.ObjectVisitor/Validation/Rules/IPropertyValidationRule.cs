using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor.Validation
{
    /// <summary>
    /// Rule for a specified property
    /// </summary>
    /// <typeparam name="T">Type of validation target</typeparam>
    /// <typeparam name="TValue">Type of validation property</typeparam>
    public interface IPropertyValidationRule<T, TValue>
    {
        /// <summary>
        /// Validation expression, true for success, false for validation failed
        /// </summary>
        Expression<Func<TValue, bool>> MustExpression { get; }

        /// <summary>
        /// Error message factory expression, it will be invoke if validation failed
        /// </summary>
        Expression<Func<T, TValue, PropertyInfo, string>> ErrorMessageExpression { get; }
    }
}