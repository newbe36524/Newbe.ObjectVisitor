using System.Linq.Expressions;
#if DEBUG
using AgileObjects.ReadableExpressions;

#endif

namespace Newbe.ObjectVisitor
{
    internal class ObjectVisitor : IObjectVisitor
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

        public override string ToString()
        {
#if !DEBUG
            return base.ToString();
#else
            return _expression.ToReadableString();
#endif
        }
    }

    internal class ObjectVisitor<T> : IObjectVisitor<T>
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

        public override string ToString()
        {
#if !DEBUG
            return base.ToString();
#else
            return _objectVisitor!.ToString();
#endif
        }
    }

    internal class ObjectVisitor<T, TExtend> : IObjectVisitor<T, TExtend>
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

        public override string ToString()
        {
#if !DEBUG
            return base.ToString();
#else
            return _objectVisitor!.ToString();
#endif
        }
    }
}