using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// core
    /// </summary>
    public static class OvBuilderExtensions
    {
        public static IObjectVisitor CreateVisitor(this IOvBuilderContext builderContext)
        {
            var factory = OvFactory.Instance;
            var visitor = factory.Create(builderContext);
            return visitor;
        }

        public static IObjectVisitor<T> CreateVisitor<T>(this IOvBuilderContext<T> builderContext)
        {
            var factory = OvFactory.Instance;
            var visitor = factory.Create(builderContext);
            return new ObjectVisitor<T>(visitor);
        }

        public static IObjectVisitor<T, TExtend> CreateVisitor<T, TExtend>(
            this IOvBuilderContext<T, TExtend> builderContext)
        {
            var factory = OvFactory.Instance;
            var visitor = factory.Create(builderContext);
            return new ObjectVisitor<T, TExtend>(visitor);
        }

        public static Action<T> GetLambda<T>(this OVBuilder<T>.IOVBuilder_V builderContext)
        {
            builderContext.CreateVisitor().TryCreateActionExpression<T>(out var action);
            return action.Compile();
        }

        public static Action<T, TExtend> GetLambda<T, TExtend>(
            this OVBuilderExt<T, TExtend>.IOVBuilderExt_V builderContext)
        {
            builderContext.CreateVisitor().TryCreateActionExpression<T, TExtend>(out var action);
            return action.Compile();
        }

        public static string GetDebugInfo<T, TExtend>(this OVBuilderExt<T, TExtend>.IOVBuilderExt_V context)
        {
            var visitor = context.CreateVisitor();
            return GetDebugInfo(visitor);
        }
        
        public static string GetDebugInfo<T>(this OVBuilder<T>.IOVBuilder_V context)
        {
            var visitor = context.CreateVisitor();
            return GetDebugInfo(visitor);
        }

        public static string GetDebugInfo(this IObjectVisitor visitor)
        {
#if DEBUG
            return visitor.ToString();
#else
            var exp = visitor.CreateExpression();
            var propertyInfo =
                typeof(Expression).GetRuntimeProperties().First(x => x.Name == "DebugView");
            Debug.Assert(propertyInfo != null, nameof(propertyInfo) + " != null");
            return propertyInfo.GetValue(exp) as string;
#endif
        }

        internal static void Run<T>(this IObjectVisitor visitor, T obj)
        {
            if (visitor.TryCreateActionExpression<T>(out var exp))
            {
                var action = exp.Compile();
                action.Invoke(obj);
            }
        }

        public static void Run<T>(this IObjectVisitor<T> visitor, T obj)
        {
            Run((IObjectVisitor) visitor, obj);
        }

        internal static void Run<T, TExtend>(this IObjectVisitor visitor, T obj, TExtend extendObj)
        {
            if (visitor.TryCreateActionExpression<T, TExtend>(out var exp))
            {
                var action = exp.Compile();
                action.Invoke(obj, extendObj);
            }
        }

        public static void Run<T, TExtend>(this IObjectVisitor<T, TExtend> visitor, T obj, TExtend extendObj)
        {
            Run((IObjectVisitor) visitor, obj, extendObj);
        }

        public static void Run<T>(this OVBuilder<T>.IOVBuilder_V builderContext)
        {
            if (builderContext.GetContext().TryGetObject<T>(out var obj))
            {
                builderContext.Run(obj);
                return;
            }

            throw new MissingSourceObjectException();
        }

        public static void Run<T>(this OVBuilder<T>.IOVBuilder_V builderContext, T obj)
        {
            var visitor = builderContext.CreateVisitor();
            visitor.Run(obj);
        }

        public static void Run<T, TExtend>(this OVBuilderExt<T, TExtend>.IOVBuilderExt_V builderContext)
        {
            if (!builderContext.GetContext().TryGetObject<T>(out var obj))
            {
                throw new MissingSourceObjectException();
            }

            builderContext.Run(obj);
        }

        public static void Run<T, TExtend>(this OVBuilderExt<T, TExtend>.IOVBuilderExt_V builderContext, T obj)
        {
            if (builderContext.GetContext().TryGetExtObject<T, TExtend>(out var extend))
            {
                builderContext.Run(obj, extend);
                return;
            }

            throw new MissingExtendObjectException();
        }

        public static void Run<T, TExtend>(this OVBuilderExt<T, TExtend>.IOVBuilderExt_V builderContext, T obj, TExtend extendObj)
        {
            var visitor = builderContext.CreateVisitor();
            visitor.Run(obj, extendObj);
        }

        public static OVBuilder<T>.IOVBuilder_V V<T>(this T obj)
        {
            var context = new OvBuilderContext<T>(new OvBuilderContext())
            {
                new SourceObjectOvBuilderContextItem {InputType = typeof(T), SourceObject = obj}
            };
            return new OVBuilder<T>(context).GetBuilder();
        }

        public static IOvBuilderContext V(this Type obj)
        {
            var context = new OvBuilderContext
            {
                new SourceObjectOvBuilderContextItem {InputType = obj, SourceObject = default}
            };
            return context;
        }

        public static OVBuilderExt<T, TExtend>.IOVBuilderExt_V WithExtendObject<T,
            TExtend>(
            this OVBuilder<T>.IOVBuilder_V context)
        {
            return context.WithExtendObject<T, TExtend>(default!);
        }

        public static OVBuilderExt<T, TExtend>.IOVBuilderExt_V WithExtendObject<T,
            TExtend>(
            this OVBuilder<T>.IOVBuilder_V context,
            TExtend extendObj)
        {
            var sourceContext = context.GetContext()
                .ReplaceExtendObject(typeof(TExtend), extendObj);
            var objectVisitorBuilderExt =
                new OVBuilderExt<T, TExtend>(new OvBuilderContext<T, TExtend>(sourceContext));
            return objectVisitorBuilderExt.GetBuilder();
        }

        private static IOvBuilderContext ReplaceExtendObject(this IOvBuilderContext context,
            Type extendType,
            object? extendObj)
        {
            if (context.TryGetContextItem<ExtendObjectOvBuilderContextItem>(out var item))
            {
                context.Remove(item!);
            }

            item = new ExtendObjectOvBuilderContextItem
            {
                ExtendObjectType = extendType,
                ExtendObject = extendObj
            };
            context.Add(item);
            return context;
        }
    }
}