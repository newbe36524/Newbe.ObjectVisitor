using System;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator
{
    public class ValidationRule<T>
    {
        /// <summary>
        /// Func{T,bool}
        /// </summary>
        public Expression<Func<T, bool>>? IfExpression { get; set; }

        /// <summary>
        /// Func{T,bool}
        /// </summary>
        public Expression<Func<T, bool>> MustExpression { get; set; } = null!;

        /// <summary>
        /// Func{T,string}
        /// </summary>
        public Expression<Func<T, string>> ErrorMessageExpression { get; set; } = null!;
    }
}