using System;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Failed to convert expression type while creating visitor
    /// </summary>
    public class FailedToConvertExpressionException : ObjectVisitorException
    {
        /// <summary>
        /// Source Expression
        /// </summary>
        public Expression SourceExpression { get; }

        /// <summary>
        /// Type of target type
        /// </summary>
        public Type Type { get; }

        /// <inheritdoc />
        public FailedToConvertExpressionException(Expression sourceExpression, Type type)
            : this($"Failed to convert expression from {sourceExpression} to {type}. That should be a bug",
                sourceExpression, type)
        {
            SourceExpression = sourceExpression;
            Type = type;
        }

        /// <inheritdoc />
        public FailedToConvertExpressionException(string message, Expression sourceExpression, Type type) :
            base(message)
        {
            SourceExpression = sourceExpression;
            Type = type;
        }

        /// <inheritdoc />
        public FailedToConvertExpressionException(string message, Exception innerException, Expression sourceExpression,
            Type type) : base(message, innerException)
        {
            SourceExpression = sourceExpression;
            Type = type;
        }
    }
}