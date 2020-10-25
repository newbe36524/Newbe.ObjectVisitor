using System;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor
{
    public interface ICachedObjectVisitor : IObjectVisitor
    {
    }

    public interface ICachedObjectVisitor<T> : ICachedObjectVisitor, IObjectVisitor<T>
    {
        Action<T> Action { get; }
    }

    public interface ICachedObjectVisitor<T, TExtend> : ICachedObjectVisitor, IObjectVisitor<T, TExtend>
    {
        Action<T, TExtend> Action { get; }
    }

}