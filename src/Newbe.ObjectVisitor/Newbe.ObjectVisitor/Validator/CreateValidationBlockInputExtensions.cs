using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator
{
    public static class CreateValidationBlockInputExtensions
    {
        public static Expression ErrorAdd(this ICreateValidationBlockInput input, Expression valueExpression)
        {
            return Expression.Call(input.ErrorExpression, nameof(HashSet<string>.Add), Array.Empty<Type>(),
                valueExpression);
        }
    }
}