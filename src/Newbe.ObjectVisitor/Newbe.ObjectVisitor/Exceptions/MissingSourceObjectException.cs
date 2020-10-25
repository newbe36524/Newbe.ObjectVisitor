using System;

namespace Newbe.ObjectVisitor
{
    public class MissingSourceObjectException : ObjectVisitorException
    {
        public MissingSourceObjectException()
            : this("missing source object when try to visit object")
        {
        }

        public MissingSourceObjectException(string message) : base(message)
        {
        }

        public MissingSourceObjectException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}