using System;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Cached Object Visitor. It means the delegate of this visitor has been created and cached. It will be more faster than using a <see cref="IObjectVisitor"/>.
    /// </summary>
    public interface ICachedObjectVisitor : IObjectVisitor
    {
    }

    /// <summary>
    /// Cached Object Visitor. It means the delegate of this visitor has been created and cached. It will be more faster than using a <see cref="IObjectVisitor"/>.
    /// </summary>
    /// <typeparam name="T">Type of Object Visitor target</typeparam>
    public interface ICachedObjectVisitor<T> : ICachedObjectVisitor, IObjectVisitor<T>
    {
        /// <summary>
        /// Delegate of visitor
        /// </summary>
        Action<T> Action { get; }
    }

    /// <summary>
    /// Cached Object Visitor. It means the delegate of this visitor has been created and cached. It will be more faster than using a <see cref="IObjectVisitor"/>.
    /// </summary>
    /// <typeparam name="T">Type of object visitor target</typeparam>
    /// <typeparam name="TExtend">Type of extend data</typeparam>
    public interface ICachedObjectVisitor<T, TExtend> : ICachedObjectVisitor, IObjectVisitor<T, TExtend>
    {
        /// <summary>
        /// Delegate of visitor
        /// </summary>
        Action<T, TExtend> Action { get; }
    }
}