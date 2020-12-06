using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Object visitor with extend data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TExtend"></typeparam>
    public class OVBuilderExt<T, TExtend> : Newbe.ObjectVisitor.IFluentApi
        , OVBuilderExt<T, TExtend>.IOVBuilderExt_V
        , OVBuilderExt<T, TExtend>.IOVBuilderExt_VP
    {
        private readonly IOvBuilderContext<T, TExtend> _context;
        private Func<PropertyInfo, bool>? _propertyInfoFilter;

        /// <summary>
        /// Create a object visitor
        /// </summary>
        /// <param name="context"></param>
        public OVBuilderExt(IOvBuilderContext<T, TExtend> context)
        {
            _context = context;
        }

        #region UserImpl

        private void Shared_ForEachObject(Expression<Action<IObjectVisitorContext<T, TExtend, object>>> foreachAction)
        {
            var filter = _propertyInfoFilter ?? PropertyInfoFilters.AllPropertyInfo;
            _context.Add(new ForEachActionContextItem
            {
                PropertyInfoFilter = filter,
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.ObjectVisitorContextWithExtend
            });
            _propertyInfoFilter = null;
        }


        private void Shared_ForEachObject(Expression<Action<string, object, TExtend>> foreachAction)
        {
            var filter = _propertyInfoFilter ?? PropertyInfoFilters.AllPropertyInfo;
            _context.Add(new ForEachActionContextItem
            {
                PropertyInfoFilter = filter,
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.NameAndValueWithExtend
            });
            _propertyInfoFilter = null;
        }


        private void Shared_ForEach<TValue>(Expression<Action<IObjectVisitorContext<T, TExtend, TValue>>> foreachAction)
        {
            var filter = _propertyInfoFilter ?? (x => x.PropertyType == typeof(TValue));
            _context.Add(new ForEachActionContextItem
            {
                ValueExpectedType = typeof(TValue),
                PropertyInfoFilter = filter,
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.ObjectVisitorContextWithExtend
            });
            _propertyInfoFilter = null;
        }


        private void Shared_ForEach<TValue>(Expression<Action<string, TValue, TExtend>> foreachAction)
        {
            var filter = _propertyInfoFilter ?? (x => x.PropertyType == typeof(TValue));
            _context.Add(new ForEachActionContextItem
            {
                ValueExpectedType = typeof(TValue),
                PropertyInfoFilter = filter,
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.NameAndValueWithExtend
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


        private IObjectVisitor<T, TExtend> Core_CreateVisitor()
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
        /// Create object visitor builder
        /// </summary>
        /// <returns></returns>
        public IOVBuilderExt_V GetBuilder()
        {
            Core_GetBuilder();
            return this;
        }


        IOVBuilderExt_VP IOVBuilderExt_V.FilterProperty(Func<PropertyInfo, bool>? propertyInfoFilter)
        {
            Core_FilterProperty(propertyInfoFilter);
            return this;
        }


        IOVBuilderExt_V IOVBuilderExt_VP.ForEach(
            Expression<Action<IObjectVisitorContext<T, TExtend, object>>> foreachAction)
        {
            Shared_ForEachObject(foreachAction);
            return this;
        }


        IOVBuilderExt_V IOVBuilderExt_VP.ForEach(Expression<Action<string, object, TExtend>> foreachAction)
        {
            Shared_ForEachObject(foreachAction);
            return this;
        }


        IOVBuilderExt_V IOVBuilderExt_VP.ForEach<TValue>(
            Expression<Action<IObjectVisitorContext<T, TExtend, TValue>>> foreachAction)
        {
            Shared_ForEach<TValue>(foreachAction);
            return this;
        }


        IOVBuilderExt_V IOVBuilderExt_VP.ForEach<TValue>(Expression<Action<string, TValue, TExtend>> foreachAction)
        {
            Shared_ForEach<TValue>(foreachAction);
            return this;
        }


        IOVBuilderExt_V IOVBuilderExt_V.ForEach(
            Expression<Action<IObjectVisitorContext<T, TExtend, object>>> foreachAction)
        {
            Shared_ForEachObject(foreachAction);
            return this;
        }


        IOVBuilderExt_V IOVBuilderExt_V.ForEach(Expression<Action<string, object, TExtend>> foreachAction)
        {
            Shared_ForEachObject(foreachAction);
            return this;
        }


        IOVBuilderExt_V IOVBuilderExt_V.ForEach<TValue>(
            Expression<Action<IObjectVisitorContext<T, TExtend, TValue>>> foreachAction)
        {
            Shared_ForEach<TValue>(foreachAction);
            return this;
        }


        IOVBuilderExt_V IOVBuilderExt_V.ForEach<TValue>(Expression<Action<string, TValue, TExtend>> foreachAction)
        {
            Shared_ForEach<TValue>(foreachAction);
            return this;
        }


        IObjectVisitor<T, TExtend> IOVBuilderExt_V.CreateVisitor()
        {
            return Core_CreateVisitor();
        }


        IOvBuilderContext IOVBuilderExt_V.GetContext()
        {
            return Core_GetContext();
        }


        /// <summary>
        /// Object visitor builder step
        /// </summary>
        public interface IOVBuilderExt_V
        {
            /// <summary>
            /// Add a action to visit properties
            /// </summary>
            /// <param name="foreachAction"></param>
            /// <returns></returns>
            IOVBuilderExt_V ForEach(Expression<Action<IObjectVisitorContext<T, TExtend, object>>> foreachAction);


            /// <summary>
            /// Add a action to visit properties
            /// </summary>
            /// <param name="foreachAction"></param>
            /// <returns></returns>
            IOVBuilderExt_V ForEach(Expression<Action<string, object, TExtend>> foreachAction);


            /// <summary>
            /// Add a action to visit properties
            /// </summary>
            /// <param name="foreachAction"></param>
            /// <typeparam name="TValue"></typeparam>
            /// <returns></returns>
            IOVBuilderExt_V
                ForEach<TValue>(Expression<Action<IObjectVisitorContext<T, TExtend, TValue>>> foreachAction);


            /// <summary>
            /// Add a action to visit properties
            /// </summary>
            /// <param name="foreachAction"></param>
            /// <typeparam name="TValue"></typeparam>
            /// <returns></returns>
            IOVBuilderExt_V ForEach<TValue>(Expression<Action<string, TValue, TExtend>> foreachAction);


            /// <summary>
            /// Filter properties to visit
            /// </summary>
            /// <param name="propertyInfoFilter"></param>
            /// <returns></returns>
            IOVBuilderExt_VP FilterProperty(Func<PropertyInfo, bool>? propertyInfoFilter);


            /// <summary>
            /// Create visitor
            /// </summary>
            /// <returns></returns>
            IObjectVisitor<T, TExtend> CreateVisitor();

            /// <summary>
            /// Get context 
            /// </summary>
            /// <returns></returns>
            IOvBuilderContext GetContext();
        }


        /// <summary>
        /// Object visitor builder step
        /// </summary>
        public interface IOVBuilderExt_VP
        {
            /// <summary>
            /// Add a action to visit properties
            /// </summary>
            /// <param name="foreachAction"></param>
            /// <returns></returns>
            IOVBuilderExt_V ForEach(Expression<Action<IObjectVisitorContext<T, TExtend, object>>> foreachAction);

            /// <summary>
            /// Add a action to visit properties
            /// </summary>
            /// <param name="foreachAction"></param>
            /// <returns></returns>
            IOVBuilderExt_V ForEach(Expression<Action<string, object, TExtend>> foreachAction);

            /// <summary>
            /// Add a action to visit properties
            /// </summary>
            /// <param name="foreachAction"></param>
            /// <typeparam name="TValue"></typeparam>
            /// <returns></returns>
            IOVBuilderExt_V
                ForEach<TValue>(Expression<Action<IObjectVisitorContext<T, TExtend, TValue>>> foreachAction);

            /// <summary>
            /// Add a action to visit properties
            /// </summary>
            /// <param name="foreachAction"></param>
            /// <typeparam name="TValue"></typeparam>
            /// <returns></returns>
            IOVBuilderExt_V ForEach<TValue>(Expression<Action<string, TValue, TExtend>> foreachAction);
        }

        #endregion
    }
}