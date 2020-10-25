using System;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor
{
    public static class ObjectVisitorExtensions
    {
        private static bool TryCreateExpression<TExpression>(this IObjectVisitor objectVisitor,
            out TExpression ex)
            where TExpression : Expression
        {
            var exp = objectVisitor.CreateExpression();
            if (exp is TExpression e)
            {
                ex = e;
                return true;
            }

            ex = null!;
            return false;
        }

        public static bool TryCreateActionExpression<T>(this IObjectVisitor objectVisitor,
            out Expression<Action<T>> ex)
            => TryCreateExpression(objectVisitor, out ex);

        public static bool TryCreateActionExpression<T, TExtend>(this IObjectVisitor objectVisitor,
            out Expression<Action<T, TExtend>> ex)
            => TryCreateExpression(objectVisitor, out ex);
    }
}