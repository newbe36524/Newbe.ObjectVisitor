using System.Linq.Expressions;

namespace Newbe.ObjectVisitor
{
    public interface IObjectVisitor
    {
        Expression CreateExpression();
    }

    public interface IObjectVisitor<T> : IObjectVisitor
    {
    }

    public interface IObjectVisitor<T, TExtend> : IObjectVisitor
    {
    }
}