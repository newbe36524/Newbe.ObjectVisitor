using System.Linq.Expressions;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Object visitor
    /// </summary>
    public interface IObjectVisitor
    {
        /// <summary>
        /// Create expression of object visitor
        /// </summary>
        /// <returns></returns>
        Expression CreateExpression();
    }

    /// <summary>
    /// Object visitor
    /// </summary>
    /// <typeparam name="T">Type of target type</typeparam>
    public interface IObjectVisitor<T> : IObjectVisitor
    {
    }

    /// <summary>
    /// Object visitor
    /// </summary>
    /// <typeparam name="T">Type of target type</typeparam>
    /// <typeparam name="TExtend">Type of extend data when running this visitor</typeparam>
    public interface IObjectVisitor<T, TExtend> : IObjectVisitor
    {
    }
}