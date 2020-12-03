using System.Collections.Generic;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Context of building a object visitor
    /// </summary>
    public interface IOvBuilderContext : ICollection<IOvBuilderContextItem>
    {
    }

    /// <summary>
    /// Context of building a object visitor
    /// </summary>
    /// <typeparam name="T">Type of target object</typeparam>
    public interface IOvBuilderContext<T> : IOvBuilderContext
    {
    }

    /// <summary>
    /// Context of building a object visitor
    /// </summary>
    /// <typeparam name="TSourceObject">Type of target object</typeparam>
    /// <typeparam name="TExtendObject">Type of extend data</typeparam>
    public interface IOvBuilderContext<TSourceObject, TExtendObject> : IOvBuilderContext
    {
    }
}