using System.Linq.Expressions;

namespace Newbe.ObjectVisitor
{
    public interface IBuildContextHandler
    {
        Expression? CreateExpression(IOvBuilderContext builderContext);
    }
}