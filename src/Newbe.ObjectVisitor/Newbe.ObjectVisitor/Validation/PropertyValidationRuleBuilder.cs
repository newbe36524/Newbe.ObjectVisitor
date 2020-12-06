using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
// ReSharper disable InconsistentNaming

namespace Newbe.ObjectVisitor.Validation
{
    /// <summary>
    /// Property validation rule builder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class PropertyValidationRuleBuilder<T, TValue> : Newbe.ObjectVisitor.IFluentApi
        , PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
    {
        private Expression<Func<T, TValue>> _propertyExp = null!;
        private PropertyInfo _propertyInfo = null!;
        private ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S _context;

        /// <summary>
        /// Create PropertyValidationRuleBuilder 
        /// </summary>
        /// <param name="context"></param>
        public PropertyValidationRuleBuilder(ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S context)
        {
            _context = context;
        }

        #region UserImpl

        private void Core_GetBuilder(Expression<Func<T, TValue>> propertyExpression)
        {
            _propertyExp = propertyExpression;
            _propertyInfo = propertyExpression.GetPropertyInfo();
        }


        private void Core_Validate(Expression<Func<T, TValue, PropertyInfo, bool>> func)
        {
            var pExp = Expression.Parameter(typeof(T), "input");
            var valueExp = Expression.Invoke(_propertyExp, pExp);
            var propertyInfoExp = Expression.Constant(_propertyInfo);
            var bodyExp = Expression.Invoke(func, pExp, valueExp, propertyInfoExp);
            var mustExp = Expression.Lambda<Func<T, bool>>(bodyExp, pExp);
            _context = _context.Validate(mustExp);
        }


        private void Core_ErrorMessage(Expression<Func<T, TValue, PropertyInfo, string>> func)
        {
            var pExp = Expression.Parameter(typeof(T), "input");
            var valueExp = Expression.Invoke(_propertyExp, pExp);
            var propertyInfoExp = Expression.Constant(_propertyInfo);
            var bodyExp = Expression.Invoke(func, pExp, valueExp, propertyInfoExp);
            var errorMessageExp = Expression.Lambda<Func<T, string>>(bodyExp, pExp);
            _context = _context.ErrorMessage(errorMessageExp);
        }


        private void Core_If(Expression<Func<T, TValue, PropertyInfo, bool>> func)
        {
            var pExp = Expression.Parameter(typeof(T), "input");
            var valueExp = Expression.Invoke(_propertyExp, pExp);
            var propertyInfoExp = Expression.Constant(_propertyInfo);
            var bodyExp = Expression.Invoke(func, pExp, valueExp, propertyInfoExp);
            var ifExp = Expression.Lambda<Func<T, bool>>(bodyExp, pExp);
            _context = _context.If(ifExp);
        }


        private void Core_Next()
        {
            _context = _context.Next();
        }


        private PropertyValidationRuleBuilder<T, TNewValue>.IPropertyValidationRuleBuilder_S Core_Property<TNewValue>(
            Expression<Func<T, TNewValue>> propertyExpression)
        {
            var ruleSet = Core_GetRuleSet();
            var builder = new ValidationRuleGroupBuilder<T>(ruleSet).GetBuilder();
            return new PropertyValidationRuleBuilder<T, TNewValue>(builder)
                .GetBuilder(propertyExpression);
        }


        private List<ValidationRuleGroup<T>> Core_GetRuleSet()
        {
            return _context.Build();
        }


        private Expression<Func<T, TValue>> Core_GetPropertyExpression()
        {
            return _propertyExp;
        }

        #endregion

        #region AutoGenerate

        /// <summary>
        /// Create builder
        /// </summary>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        public IPropertyValidationRuleBuilder_S GetBuilder(Expression<Func<T, TValue>> propertyExpression)
        {
            Core_GetBuilder(propertyExpression);
            return this;
        }


        IPropertyValidationRuleBuilder_S IPropertyValidationRuleBuilder_S.Validate(
            Expression<Func<T, TValue, PropertyInfo, bool>> func)
        {
            Core_Validate(func);
            return this;
        }


        IPropertyValidationRuleBuilder_S IPropertyValidationRuleBuilder_S.ErrorMessage(
            Expression<Func<T, TValue, PropertyInfo, string>> func)
        {
            Core_ErrorMessage(func);
            return this;
        }


        IPropertyValidationRuleBuilder_S IPropertyValidationRuleBuilder_S.If(
            Expression<Func<T, TValue, PropertyInfo, bool>> func)
        {
            Core_If(func);
            return this;
        }


        IPropertyValidationRuleBuilder_S IPropertyValidationRuleBuilder_S.Next()
        {
            Core_Next();
            return this;
        }


        PropertyValidationRuleBuilder<T, TNewValue>.IPropertyValidationRuleBuilder_S IPropertyValidationRuleBuilder_S.
            Property<TNewValue>(Expression<Func<T, TNewValue>> propertyExpression)
        {
            return Core_Property<TNewValue>(propertyExpression);
        }


        List<ValidationRuleGroup<T>> IPropertyValidationRuleBuilder_S.Build()
        {
            return Core_GetRuleSet();
        }


        Expression<Func<T, TValue>> IPropertyValidationRuleBuilder_S.GetPropertyExpression()
        {
            return Core_GetPropertyExpression();
        }


        /// <summary>
        /// Property validation builder step
        /// </summary>
        public interface IPropertyValidationRuleBuilder_S
        {
            /// <summary>
            /// Add validation func
            /// </summary>
            /// <param name="func"></param>
            /// <returns></returns>
            IPropertyValidationRuleBuilder_S Validate(Expression<Func<T, TValue, PropertyInfo, bool>> func);

            /// <summary>
            /// Specify error message if validation failed
            /// </summary>
            /// <param name="func"></param>
            /// <returns></returns>
            IPropertyValidationRuleBuilder_S ErrorMessage(Expression<Func<T, TValue, PropertyInfo, string>> func);


            /// <summary>
            /// Add condition to trigger validation
            /// </summary>
            /// <param name="func"></param>
            /// <returns></returns>
            IPropertyValidationRuleBuilder_S If(Expression<Func<T, TValue, PropertyInfo, bool>> func);


            /// <summary>
            /// Move to next validation rule creation step
            /// </summary>
            /// <returns></returns>
            IPropertyValidationRuleBuilder_S Next();


            /// <summary>
            /// Switch to new property to create validation rule
            /// </summary>
            /// <param name="propertyExpression"></param>
            /// <typeparam name="TNewValue"></typeparam>
            /// <returns></returns>
            PropertyValidationRuleBuilder<T, TNewValue>.IPropertyValidationRuleBuilder_S Property<TNewValue>(
                Expression<Func<T, TNewValue>> propertyExpression);


            /// <summary>
            /// Build validation rule group
            /// </summary>
            /// <returns></returns>
            List<ValidationRuleGroup<T>> Build();


            /// <summary>
            /// Get property expression
            /// </summary>
            /// <returns></returns>
            Expression<Func<T, TValue>> GetPropertyExpression();
        }

        #endregion
    }
}