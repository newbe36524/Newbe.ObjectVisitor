using System;
using System.Linq.Expressions;
using System.Reflection;

// ReSharper disable InconsistentNaming

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Object visitor builder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OVBuilder<T> : Newbe.ObjectVisitor.IFluentApi
        , OVBuilder<T>.IOVBuilder_V
        , OVBuilder<T>.IOVBuilder_VP
    {
        private Func<PropertyInfo, bool>? _propertyInfoFilter;
        private readonly IOvBuilderContext<T> _context;

        /// <summary>
        /// Create a object visitor builder
        /// </summary>
        /// <param name="context"></param>
        public OVBuilder(IOvBuilderContext<T> context)
        {
            _context = context;
        }

        #region UserImpl

        private void Shared_ForEachObject(Expression<Action<IObjectVisitorContext<T, object>>> foreachAction)
        {
            var filter = _propertyInfoFilter ?? PropertyInfoFilters.AllPropertyInfo;
            _context.Add(new ForEachActionContextItem
            {
                PropertyInfoFilter = filter,
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.ObjectVisitorContext
            });
            _propertyInfoFilter = null;
        }


        private void Shared_ForEachObject(Expression<Action<string, object>> foreachAction)
        {
            var filter = _propertyInfoFilter ?? PropertyInfoFilters.AllPropertyInfo;
            _context.Add(new ForEachActionContextItem
            {
                PropertyInfoFilter = filter,
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.NameAndValue,
            });
            _propertyInfoFilter = null;
        }


        private void Shared_ForEach<TValue>(Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction)
        {
            var filter = _propertyInfoFilter ?? (x => x.PropertyType == typeof(TValue));
            _context.Add(new ForEachActionContextItem
            {
                ValueExpectedType = typeof(TValue),
                PropertyInfoFilter = filter,
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.ObjectVisitorContext
            });
            _propertyInfoFilter = null;
        }


        private void Shared_ForEach<TValue>(Expression<Action<string, TValue>> foreachAction)
        {
            var filter = _propertyInfoFilter ?? (x => x.PropertyType == typeof(TValue));
            _context.Add(new ForEachActionContextItem
            {
                ValueExpectedType = typeof(TValue),
                PropertyInfoFilter = filter,
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.NameAndValue
            });
            _propertyInfoFilter = null;
        }

        private void Core_GetBuilder()
        {
            // nothing
        }


        private void Core_FilterProperty(Func<PropertyInfo, bool>? propertyInfoFilter)
        {
            _propertyInfoFilter = propertyInfoFilter;
        }


        private IObjectVisitor<T> Core_CreateVisitor()
        {
            return _context.CreateVisitor();
        }


        private IOvBuilderContext Core_GetContext()
        {
            return _context;
        }

        #endregion

        #region AutoGenerate

        /// <summary>
        /// Get object visitor builder
        /// </summary>
        /// <returns></returns>
        public IOVBuilder_V GetBuilder()
        {
            Core_GetBuilder();
            return this;
        }


        IOVBuilder_VP IOVBuilder_V.FilterProperty(Func<PropertyInfo, bool>? propertyInfoFilter)
        {
            Core_FilterProperty(propertyInfoFilter);
            return this;
        }


        IOVBuilder_V IOVBuilder_VP.ForEach(Expression<Action<IObjectVisitorContext<T, object>>> foreachAction)
        {
            Shared_ForEachObject(foreachAction);
            return this;
        }


        IOVBuilder_V IOVBuilder_VP.ForEach(Expression<Action<string, object>> foreachAction)
        {
            Shared_ForEachObject(foreachAction);
            return this;
        }


        IOVBuilder_V IOVBuilder_VP.ForEach<TValue>(Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction)
        {
            Shared_ForEach<TValue>(foreachAction);
            return this;
        }


        IOVBuilder_V IOVBuilder_VP.ForEach<TValue>(Expression<Action<string, TValue>> foreachAction)
        {
            Shared_ForEach<TValue>(foreachAction);
            return this;
        }


        IOVBuilder_V IOVBuilder_V.ForEach(Expression<Action<IObjectVisitorContext<T, object>>> foreachAction)
        {
            Shared_ForEachObject(foreachAction);
            return this;
        }


        IOVBuilder_V IOVBuilder_V.ForEach(Expression<Action<string, object>> foreachAction)
        {
            Shared_ForEachObject(foreachAction);
            return this;
        }


        IOVBuilder_V IOVBuilder_V.ForEach<TValue>(Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction)
        {
            Shared_ForEach<TValue>(foreachAction);
            return this;
        }


        IOVBuilder_V IOVBuilder_V.ForEach<TValue>(Expression<Action<string, TValue>> foreachAction)
        {
            Shared_ForEach<TValue>(foreachAction);
            return this;
        }


        IObjectVisitor<T> IOVBuilder_V.CreateVisitor()
        {
            return Core_CreateVisitor();
        }


        IOvBuilderContext IOVBuilder_V.GetContext()
        {
            return Core_GetContext();
        }


        /// <summary>
        /// Object visitor builder step
        /// </summary>
        public interface IOVBuilder_V
        {
            /// <summary>
            /// Add a action to visit properties
            /// </summary>
            /// <param name="foreachAction"></param>
            /// <returns></returns>
            IOVBuilder_V ForEach(Expression<Action<IObjectVisitorContext<T, object>>> foreachAction);


            /// <summary>
            /// Add a action to visit properties
            /// </summary>
            /// <param name="foreachAction"></param>
            /// <returns></returns>
            IOVBuilder_V ForEach(Expression<Action<string, object>> foreachAction);


            /// <summary>
            /// Add a action to visit properties
            /// </summary>
            /// <param name="foreachAction"></param>
            /// <typeparam name="TValue"></typeparam>
            /// <returns></returns>
            IOVBuilder_V ForEach<TValue>(Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction);


            /// <summary>
            /// Add a action to visit properties
            /// </summary>
            /// <param name="foreachAction"></param>
            /// <typeparam name="TValue"></typeparam>
            /// <returns></returns>
            IOVBuilder_V ForEach<TValue>(Expression<Action<string, TValue>> foreachAction);


            /// <summary>
            /// Filter properties to visit
            /// </summary>
            /// <param name="propertyInfoFilter"></param>
            /// <returns></returns>
            IOVBuilder_VP FilterProperty(Func<PropertyInfo, bool>? propertyInfoFilter);


            /// <summary>
            /// Create visitor
            /// </summary>
            /// <returns></returns>
            IObjectVisitor<T> CreateVisitor();


            /// <summary>
            /// Get context
            /// </summary>
            /// <returns></returns>
            IOvBuilderContext GetContext();
        }


        /// <summary>
        /// Object visitor builder step
        /// </summary>
        public interface IOVBuilder_VP
        {
            /// <summary>
            /// Add a action to visit properties
            /// </summary>
            /// <param name="foreachAction"></param>
            /// <returns></returns>
            IOVBuilder_V ForEach(Expression<Action<IObjectVisitorContext<T, object>>> foreachAction);


            /// <summary>
            /// Add a action to visit properties
            /// </summary>
            /// <param name="foreachAction"></param>
            /// <returns></returns>
            IOVBuilder_V ForEach(Expression<Action<string, object>> foreachAction);


            /// <summary>
            /// Add a action to visit properties
            /// </summary>
            /// <param name="foreachAction"></param>
            /// <typeparam name="TValue"></typeparam>
            /// <returns></returns>
            IOVBuilder_V ForEach<TValue>(Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction);


            /// <summary>
            /// Add a action to visit properties
            /// </summary>
            /// <param name="foreachAction"></param>
            /// <typeparam name="TValue"></typeparam>
            /// <returns></returns>
            IOVBuilder_V ForEach<TValue>(Expression<Action<string, TValue>> foreachAction);
        }

        #endregion
    }
}