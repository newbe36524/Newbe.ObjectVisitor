using System.Linq.Expressions;

namespace Newbe.ObjectVisitor
{
    public class ForEachActionContextItem : IOvBuilderContextItem
    {
        public ForEachActionContextExpressionType ExpressionType { get; set; }
        public Expression ForEachAction { get; set; } = null!;
    }
}