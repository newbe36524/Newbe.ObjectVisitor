using System;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Missing source object
    /// </summary>
    public class MissingSourceObjectException : ObjectVisitorException
    {
        /// <inheritdoc />
        public MissingSourceObjectException()
            : this("missing source object when try to visit object")
        {
        }

        /// <inheritdoc />
        public MissingSourceObjectException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public MissingSourceObjectException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}