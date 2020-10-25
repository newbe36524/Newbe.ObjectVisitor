using System;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor
{
    public class FailedToConvertExpressionException : ObjectVisitorException
    {
        public Expression SourceExpression { get; }
        public Type Type { get; }

        public FailedToConvertExpressionException(Expression sourceExpression, Type type)
            : this($"Failed to convert expression from {sourceExpression} to {type}. That should be a bug", sourceExpression, type)
        {
            SourceExpression = sourceExpression;
            Type = type;
        }

        public FailedToConvertExpressionException(string message, Expression sourceExpression, Type type) :
            base(message)
        {
            SourceExpression = sourceExpression;
            Type = type;
        }

        public FailedToConvertExpressionException(string message, Exception innerException, Expression sourceExpression,
            Type type) : base(message, innerException)
        {
            SourceExpression = sourceExpression;
            Type = type;
        }
    }
}