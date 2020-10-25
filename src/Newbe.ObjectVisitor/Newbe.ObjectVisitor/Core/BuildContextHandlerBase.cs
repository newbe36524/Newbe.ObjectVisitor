using System;
using System.Linq;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor
{
    public abstract class BuildContextHandlerBase<T> : IBuildContextHandler
        where T : IOvBuilderContextItem
    {
        public Expression? CreateExpression(IOvBuilderContext builderContext)
        {
            var item = builderContext.OfType<T>().LastOrDefault();
            if (item != null)
            {
                var inputType = builderContext.OfType<SourceObjectOvBuilderContextItem>()
                    .First()
                    .InputType;
                var extendType = builderContext.OfType<ExtendObjectOvBuilderContextItem>()
                    .FirstOrDefault()
                    ?.ExtendObjectType;
                return CreateCore(inputType, extendType, item);
            }

            return null;
        }

        public abstract Expression CreateCore(Type inputType, Type? extendType, T context);
    }
}