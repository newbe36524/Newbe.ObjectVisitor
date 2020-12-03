using System;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Missing extend data object
    /// </summary>
    public class MissingExtendObjectException : ObjectVisitorException
    {
        public MissingExtendObjectException()
            : this("missing extend object when try to visit object")
        {
        }

        public MissingExtendObjectException(string message) : base(message)
        {
        }

        public MissingExtendObjectException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}