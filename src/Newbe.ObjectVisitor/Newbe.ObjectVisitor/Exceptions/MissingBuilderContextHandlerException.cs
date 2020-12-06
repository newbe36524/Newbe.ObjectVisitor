using System;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Missing builder context handler
    /// </summary>
    public class MissingBuilderContextHandlerException : ObjectVisitorException
    {
        /// <inheritdoc />
        public MissingBuilderContextHandlerException()
            : this("Missing builder context handler")
        {
        }

        /// <inheritdoc />
        public MissingBuilderContextHandlerException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public MissingBuilderContextHandlerException(string message, Exception innerException) : base(message,
            innerException)
        {
        }
    }
}