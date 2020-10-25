using System;

namespace Newbe.ObjectVisitor
{
    public class MissingBuilderContextHandlerException : ObjectVisitorException
    {
        public MissingBuilderContextHandlerException()
            : this("Missing builder context handler")
        {
        }

        public MissingBuilderContextHandlerException(string message) : base(message)
        {
        }

        public MissingBuilderContextHandlerException(string message, Exception innerException) : base(message,
            innerException)
        {
        }
    }
}