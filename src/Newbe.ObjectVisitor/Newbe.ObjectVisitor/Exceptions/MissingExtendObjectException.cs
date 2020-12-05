using System;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Missing extend data object
    /// </summary>
    public class MissingExtendObjectException : ObjectVisitorException
    {
        /// <inheritdoc />
        public MissingExtendObjectException()
            : this("missing extend object when try to visit object")
        {
        }

        /// <inheritdoc />
        public MissingExtendObjectException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public MissingExtendObjectException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}