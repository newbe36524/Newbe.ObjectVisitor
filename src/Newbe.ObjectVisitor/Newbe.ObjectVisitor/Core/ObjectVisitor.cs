using System.Linq.Expressions;

namespace Newbe.ObjectVisitor
{
    public class ObjectVisitor : IObjectVisitor
    {
        private readonly Expression _expression;

        public ObjectVisitor(
            Expression expression)
        {
            _expression = expression;
        }

        public Expression CreateExpression()
        {
            return _expression;
        }
    }

    public class ObjectVisitor<T> : IObjectVisitor<T>
    {
        private readonly IObjectVisitor _objectVisitor;

        public ObjectVisitor(
            IObjectVisitor objectVisitor)
        {
            _objectVisitor = objectVisitor;
        }


        public Expression CreateExpression()
        {
            return _objectVisitor.CreateExpression();
        }
    }

    public class ObjectVisitor<T, TExtend> : IObjectVisitor<T, TExtend>
    {
        private readonly IObjectVisitor _objectVisitor;

        public ObjectVisitor(
            IObjectVisitor objectVisitor)
        {
            _objectVisitor = objectVisitor;
        }

        public Expression CreateExpression()
        {
            return _objectVisitor.CreateExpression();
        }
    }
}