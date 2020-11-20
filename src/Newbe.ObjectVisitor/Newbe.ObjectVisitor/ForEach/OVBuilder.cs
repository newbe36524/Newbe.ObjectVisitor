using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    public class OVBuilder<T> : Newbe.ObjectVisitor.IFluentApi
        , OVBuilder<T>.IOVBuilder_V
        , OVBuilder<T>.IOVBuilder_VP
    {
        private Func<PropertyInfo, bool>? _propertyInfoFilter;
        private readonly IOvBuilderContext<T> _context;

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


        public interface IOVBuilder_V
        {
            IOVBuilder_V ForEach(Expression<Action<IObjectVisitorContext<T, object>>> foreachAction);


            IOVBuilder_V ForEach(Expression<Action<string, object>> foreachAction);


            IOVBuilder_V ForEach<TValue>(Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction);


            IOVBuilder_V ForEach<TValue>(Expression<Action<string, TValue>> foreachAction);


            IOVBuilder_VP FilterProperty(Func<PropertyInfo, bool>? propertyInfoFilter);


            IObjectVisitor<T> CreateVisitor();


            IOvBuilderContext GetContext();
        }


        public interface IOVBuilder_VP
        {
            IOVBuilder_V ForEach(Expression<Action<IObjectVisitorContext<T, object>>> foreachAction);


            IOVBuilder_V ForEach(Expression<Action<string, object>> foreachAction);


            IOVBuilder_V ForEach<TValue>(Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction);


            IOVBuilder_V ForEach<TValue>(Expression<Action<string, TValue>> foreachAction);
        }

        #endregion
    }
}