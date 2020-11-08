using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor
{
    public abstract class BuildContextHandlerBase<T> : IBuildContextHandler
        where T : IOvBuilderContextItem
    {
        public Expression? CreateExpression(IOvBuilderContext builderContext)
        {
            var items = builderContext.OfType<T>().ToArray();
            if (items.Any())
            {
                var inputType = builderContext.OfType<SourceObjectOvBuilderContextItem>()
                    .First()
                    .InputType;
                var extendType = builderContext.OfType<ExtendObjectOvBuilderContextItem>()
                    .FirstOrDefault()
                    ?.ExtendObjectType;
                var exps = CreateExpressions();
                if (items.Length == 1)
                {
                    return exps.Single();
                }

                var pExp = Expression.Parameter(inputType, "s");
                if (extendType != null)
                {
                    var sourceExp = Expression.Parameter(extendType, "e");
                    var blockItems = exps.Select(x => Expression.Invoke(x, pExp, sourceExp));
                    var blockExpression = Expression.Block(blockItems);
                    var actionType = Expression.GetActionType(inputType, extendType);
                    var finalExp = Expression.Lambda(actionType, blockExpression, pExp, sourceExp);
                    return finalExp;
                }
                else
                {
                    var blockItems = exps.Select(x => Expression.Invoke(x, pExp));
                    var blockExpression = Expression.Block(blockItems);
                    var actionType = Expression.GetActionType(inputType);
                    var finalExp = Expression.Lambda(actionType, blockExpression, pExp);
                    return finalExp;
                }

                IEnumerable<Expression> CreateExpressions()
                {
                    foreach (var item in items)
                    {
                        yield return CreateCore(inputType, extendType, item);
                    }
                }
            }

            return null;
        }

        public abstract Expression CreateCore(Type inputType, Type? extendType, T context);
    }
}