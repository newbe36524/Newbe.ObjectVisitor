using System.Linq.Expressions;

namespace Newbe.ObjectVisitor
{
    internal interface IBuildContextHandler
    {
        Expression? CreateExpression(IOvBuilderContext builderContext);
    }
}