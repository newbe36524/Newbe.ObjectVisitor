using System.Collections.Generic;

namespace Newbe.ObjectVisitor
{
    public interface IOvBuilderContext : ICollection<IOvBuilderContextItem>
    {
    }

    public interface IOvBuilderContext<T> : IOvBuilderContext
    {
    }

    public interface IOvBuilderContext<TSourceObject, TExtendObject> : IOvBuilderContext
    {
    }
}