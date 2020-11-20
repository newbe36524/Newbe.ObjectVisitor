using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    public class OVBuilderExt<T, TExtend> : Newbe.ObjectVisitor.IFluentApi
        , OVBuilderExt<T, TExtend>.IOVBuilderExt_V
        , OVBuilderExt<T, TExtend>.IOVBuilderExt_VP
    {
        private readonly IOvBuilderContext<T, TExtend> _context;
        private Func<PropertyInfo, bool>? _propertyInfoFilter;

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


        public interface IOVBuilderExt_V
        {
            IOVBuilderExt_V ForEach(Expression<Action<IObjectVisitorContext<T, TExtend, object>>> foreachAction);


            IOVBuilderExt_V ForEach(Expression<Action<string, object, TExtend>> foreachAction);


            IOVBuilderExt_V
                ForEach<TValue>(Expression<Action<IObjectVisitorContext<T, TExtend, TValue>>> foreachAction);


            IOVBuilderExt_V ForEach<TValue>(Expression<Action<string, TValue, TExtend>> foreachAction);


            IOVBuilderExt_VP FilterProperty(Func<PropertyInfo, bool>? propertyInfoFilter);


            IObjectVisitor<T, TExtend> CreateVisitor();


            IOvBuilderContext GetContext();
        }


        public interface IOVBuilderExt_VP
        {
            IOVBuilderExt_V ForEach(Expression<Action<IObjectVisitorContext<T, TExtend, object>>> foreachAction);


            IOVBuilderExt_V ForEach(Expression<Action<string, object, TExtend>> foreachAction);


            IOVBuilderExt_V
                ForEach<TValue>(Expression<Action<IObjectVisitorContext<T, TExtend, TValue>>> foreachAction);


            IOVBuilderExt_V ForEach<TValue>(Expression<Action<string, TValue, TExtend>> foreachAction);
        }

        #endregion
    }
}