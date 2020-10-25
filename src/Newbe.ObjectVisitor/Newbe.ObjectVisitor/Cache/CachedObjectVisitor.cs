using System;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor
{
    public class CachedObjectVisitor<T> : ICachedObjectVisitor<T>
    {
        private readonly IObjectVisitor _objectVisitor;

        private readonly Lazy<Action<T>> _lazy;

        public CachedObjectVisitor(
            IObjectVisitor objectVisitor)
        {
            _objectVisitor = objectVisitor;
            _lazy = new Lazy<Action<T>>(() =>
            {
                if (objectVisitor.TryCreateActionExpression<T>(out var exp))
                {
                    var action = exp.Compile();
                    return action;
                }

                throw new FailedToConvertExpressionException(objectVisitor.CreateExpression(),
                    typeof(Expression<Action<T>>));
            });
        }

        public Expression CreateExpression()
        {
            return _objectVisitor.CreateExpression();
        }

        public Action<T> Action => _lazy.Value;
    }

    public class CachedObjectVisitor<T, TExtend> : ICachedObjectVisitor<T, TExtend>
    {
        private readonly IObjectVisitor _objectVisitor;

        private readonly Lazy<Action<T, TExtend>> _lazy;

        public CachedObjectVisitor(
            IObjectVisitor objectVisitor)
        {
            _objectVisitor = objectVisitor;
            _lazy = new Lazy<Action<T, TExtend>>(() =>
            {
                if (objectVisitor.TryCreateActionExpression<T, TExtend>(out var exp))
                {
                    var action = exp.Compile();
                    return action;
                }

                throw new FailedToConvertExpressionException(objectVisitor.CreateExpression(),
                    typeof(Expression<Action<T, TExtend>>));
            });
        }

        public Expression CreateExpression()
        {
            return _objectVisitor.CreateExpression();
        }

        public Action<T, TExtend> Action => _lazy.Value;
    }
}