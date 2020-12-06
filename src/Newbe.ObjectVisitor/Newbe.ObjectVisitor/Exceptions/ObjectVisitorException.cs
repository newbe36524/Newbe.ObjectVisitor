using System;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Base class of expression about object visitor
    /// </summary>
    public abstract class ObjectVisitorException : Exception
    {
        /// <inheritdoc />
        protected ObjectVisitorException()
        {
        }
        /// <inheritdoc />

        protected ObjectVisitorException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        protected ObjectVisitorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}