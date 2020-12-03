using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Core extension methods of object visitor builder
    /// </summary>
    public static class OvBuilderExtensions
    {
        /// <summary>
        /// Create object visitor
        /// </summary>
        /// <param name="builderContext">Context of builder</param>
        /// <returns>Object visitor</returns>
        public static IObjectVisitor CreateVisitor(this IOvBuilderContext builderContext)
        {
            var factory = OvFactory.Instance;
            var visitor = factory.Create(builderContext);
            return visitor;
        }

        /// <summary>
        /// Create object visitor
        /// </summary>
        /// <param name="builderContext">Context of builder</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <returns>Object visitor</returns>
        public static IObjectVisitor<T> CreateVisitor<T>(this IOvBuilderContext<T> builderContext)
        {
            var factory = OvFactory.Instance;
            var visitor = factory.Create(builderContext);
            return new ObjectVisitor<T>(visitor);
        }

        /// <summary>
        /// Create object visitor
        /// </summary>
        /// <param name="builderContext">Context of builder</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <typeparam name="TExtend">Type of extend data</typeparam>
        /// <returns>Object visitor</returns>
        public static IObjectVisitor<T, TExtend> CreateVisitor<T, TExtend>(
            this IOvBuilderContext<T, TExtend> builderContext)
        {
            var factory = OvFactory.Instance;
            var visitor = factory.Create(builderContext);
            return new ObjectVisitor<T, TExtend>(visitor);
        }

        /// <summary>
        /// Build a lambda action from <paramref name="builderContext"/>
        /// </summary>
        /// <param name="builderContext">Context of builder</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <returns>Lambda action</returns>
        public static Action<T> GetLambda<T>(this OVBuilder<T>.IOVBuilder_V builderContext)
        {
            builderContext.CreateVisitor().TryCreateActionExpression<T>(out var action);
            return action.Compile();
        }

        /// <summary>
        /// Build a lambda action from <paramref name="builderContext"/>
        /// </summary>
        /// <param name="builderContext">Context of builder</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <typeparam name="TExtend">Type of extend data</typeparam>
        /// <returns>Lambda action</returns>
        public static Action<T, TExtend> GetLambda<T, TExtend>(
            this OVBuilderExt<T, TExtend>.IOVBuilderExt_V builderContext)
        {
            builderContext.CreateVisitor().TryCreateActionExpression<T, TExtend>(out var action);
            return action.Compile();
        }

        /// <summary>
        /// Get debug info from <paramref name="builderContext"/>. It is useful while debug expression.
        /// </summary>
        /// <param name="builderContext">Context of builder</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <typeparam name="TExtend">Type of extend data</typeparam>
        /// <returns>Debug info</returns>
        public static string GetDebugInfo<T, TExtend>(this OVBuilderExt<T, TExtend>.IOVBuilderExt_V builderContext)
        {
            var visitor = builderContext.CreateVisitor();
            return GetDebugInfo(visitor);
        }

        /// <summary>
        /// Get debug info from <paramref name="builderContext"/>. It is useful while debug expression.
        /// </summary>
        /// <param name="builderContext">Context of builder</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <returns>Debug info</returns>
        public static string GetDebugInfo<T>(this OVBuilder<T>.IOVBuilder_V builderContext)
        {
            var visitor = builderContext.CreateVisitor();
            return GetDebugInfo(visitor);
        }

        /// <summary>
        /// Get debug info from <paramref name="visitor"/>. It is useful while debug expression.
        /// </summary>
        /// <param name="visitor">Object visitor</param>
        /// <returns>Debug info</returns>
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

        private static void Run<T>(this IObjectVisitor visitor, T obj)
        {
            if (visitor.TryCreateActionExpression<T>(out var exp))
            {
                var action = exp.Compile();
                action.Invoke(obj);
            }
        }

        /// <summary>
        /// Run a object visitor with target object
        /// </summary>
        /// <param name="visitor">Object visitor</param>
        /// <param name="obj">Target object</param>
        /// <typeparam name="T">Type of target object</typeparam>
        public static void Run<T>(this IObjectVisitor<T> visitor, T obj)
        {
            Run((IObjectVisitor) visitor, obj);
        }

        private static void Run<T, TExtend>(this IObjectVisitor visitor, T obj, TExtend extendObj)
        {
            if (visitor.TryCreateActionExpression<T, TExtend>(out var exp))
            {
                var action = exp.Compile();
                action.Invoke(obj, extendObj);
            }
        }

        /// <summary>
        /// Run a object visitor with target object and extend data
        /// </summary>
        /// <param name="visitor">Object visitor</param>
        /// <param name="obj">Target object</param>
        /// <param name="extendObj">Extend data</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <typeparam name="TExtend">Type of extend data</typeparam>
        public static void Run<T, TExtend>(this IObjectVisitor<T, TExtend> visitor, T obj, TExtend extendObj)
        {
            Run((IObjectVisitor) visitor, obj, extendObj);
        }

        /// <summary>
        /// Run a object visitor with target object which has been specified at start
        /// </summary>
        /// <param name="builderContext">Context of builder</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <exception cref="MissingSourceObjectException">It throws if there is no source object specified at start. If throws, you should run this visitor with <see cref="Run{T}(Newbe.ObjectVisitor.OVBuilder{T}.IOVBuilder_V,T)"/></exception>
        public static void Run<T>(this OVBuilder<T>.IOVBuilder_V builderContext)
        {
            if (builderContext.GetContext().TryGetObject<T>(out var obj))
            {
                builderContext.Run(obj);
                return;
            }

            throw new MissingSourceObjectException();
        }

        /// <summary>
        /// Run a object visitor with target object
        /// </summary>
        /// <param name="builderContext">Context of builder</param>
        /// <param name="obj">Target object</param>
        /// <typeparam name="T">Type of target object</typeparam>
        public static void Run<T>(this OVBuilder<T>.IOVBuilder_V builderContext, T obj)
        {
            var visitor = builderContext.CreateVisitor();
            visitor.Run(obj);
        }

        /// <summary>
        /// Run a object visitor with target object and extend data those has been specified at start
        /// </summary>
        /// <param name="builderContext">Context of builder</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <typeparam name="TExtend">Type of extend data</typeparam>
        /// <exception cref="MissingSourceObjectException">It throws if there is no source object specified at start. If throws, you should run this visitor with <see cref="Run{T,TExtend}(Newbe.ObjectVisitor.OVBuilderExt{T,TExtend}.IOVBuilderExt_V,T)"/></exception>
        public static void Run<T, TExtend>(this OVBuilderExt<T, TExtend>.IOVBuilderExt_V builderContext)
        {
            if (!builderContext.GetContext().TryGetObject<T>(out var obj))
            {
                throw new MissingSourceObjectException();
            }

            builderContext.Run(obj);
        }

        /// <summary>
        /// Run a object visitor with target object and specified extend data when creating visitor
        /// </summary>
        /// <param name="builderContext">Context of builder</param>
        /// <param name="obj">Target object</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <typeparam name="TExtend">Type of extend data</typeparam>
        /// <exception cref="MissingSourceObjectException">It throws if there is no extend data specified at start. If throws, you should run this visitor with <see cref="Run{T,TExtend}(Newbe.ObjectVisitor.OVBuilderExt{T,TExtend}.IOVBuilderExt_V,T,TExtend)"/></exception>
        public static void Run<T, TExtend>(this OVBuilderExt<T, TExtend>.IOVBuilderExt_V builderContext, T obj)
        {
            if (builderContext.GetContext().TryGetExtObject<T, TExtend>(out var extend))
            {
                builderContext.Run(obj, extend);
                return;
            }

            throw new MissingExtendObjectException();
        }

        /// <summary>
        /// Run a object visitor with target object and extend data
        /// </summary>
        /// <param name="builderContext">Context of builder</param>
        /// <param name="obj">Target object</param>
        /// <param name="extendObj">Extend data</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <typeparam name="TExtend">Type of extend data</typeparam>
        public static void Run<T, TExtend>(this OVBuilderExt<T, TExtend>.IOVBuilderExt_V builderContext, T obj,
            TExtend extendObj)
        {
            var visitor = builderContext.CreateVisitor();
            visitor.Run(obj, extendObj);
        }

        /// <summary>
        /// Get a object visitor builder to create a object visitor
        /// </summary>
        /// <param name="obj">Target object</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <returns>Object visitor builder</returns>
        public static OVBuilder<T>.IOVBuilder_V V<T>(this T obj)
        {
            var context = new OvBuilderContext<T>(new OvBuilderContext())
            {
                new SourceObjectOvBuilderContextItem {InputType = typeof(T), SourceObject = obj}
            };
            return new OVBuilder<T>(context).GetBuilder();
        }

        /// <summary>
        /// Specify the object visitor should run with a extend data
        /// </summary>
        /// <param name="builderContext">Context of builder</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <typeparam name="TExtend">Type of extend data</typeparam>
        /// <returns>Object visitor builder</returns>
        public static OVBuilderExt<T, TExtend>.IOVBuilderExt_V WithExtendObject<T, TExtend>(
            this OVBuilder<T>.IOVBuilder_V builderContext)
        {
            return builderContext.WithExtendObject<T, TExtend>(default!);
        }

        /// <summary>
        /// Specify the object visitor should run with a extend data
        /// </summary>
        /// <param name="builderContext">Context of builder</param>
        /// <param name="extendObj">Extend data</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <typeparam name="TExtend">Type of extend data</typeparam>
        /// <returns>Object visitor builder</returns>
        public static OVBuilderExt<T, TExtend>.IOVBuilderExt_V WithExtendObject<T, TExtend>(
            this OVBuilder<T>.IOVBuilder_V builderContext,
            TExtend extendObj)
        {
            var sourceContext = builderContext.GetContext()
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