using System;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Base class of expression about object visitor
    /// </summary>
    public abstract class ObjectVisitorException : Exception
    {
        protected ObjectVisitorException()
        {
        }

        protected ObjectVisitorException(string message) : base(message)
        {
        }

        protected ObjectVisitorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}