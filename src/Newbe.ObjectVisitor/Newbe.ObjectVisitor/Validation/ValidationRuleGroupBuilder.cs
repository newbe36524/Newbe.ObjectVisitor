using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validation
{
    /// <summary>
    /// Validation rule group builder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidationRuleGroupBuilder<T> : Newbe.ObjectVisitor.IFluentApi
        , ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S
    {
        private readonly List<ValidationRuleGroup<T>> _context;
        private readonly ValidationRuleGroup<T> _nowGroup;
        private ValidationRule<T> _nowRule;

        /// <summary>
        /// New ValidationRuleGroupBuilder
        /// </summary>
        /// <param name="context"></param>
        public ValidationRuleGroupBuilder(List<ValidationRuleGroup<T>> context)
        {
            _context = context;
            _nowGroup = new ValidationRuleGroup<T>();
            _nowRule = new ValidationRule<T>();
        }

        #region UserImpl

        private void Core_GetBuilder(ValidationRuleRelation relation = ValidationRuleRelation.And)
        {
            _nowGroup.RuleRelation = relation;
        }


        private void Core_Validate(Expression<Func<T, bool>> func)
        {
            _nowRule.MustExpression = func;
            _nowGroup.Add(_nowRule);
        }


        private void Core_ErrorMessage(Expression<Func<T, string>> func)
        {
            _nowRule.ErrorMessageExpression = func;
        }


        private void Core_If(Expression<Func<T, bool>> func)
        {
            _nowRule.IfExpression = func;
        }


        private void Core_Next()
        {
            _nowRule = new ValidationRule<T>();
        }


        private List<ValidationRuleGroup<T>> Core_GetRuleSet()
        {
            if (_nowGroup.Any())
            {
                _context.Add(_nowGroup);
            }

            return _context;
        }

        #endregion

        #region AutoGenerate

        /// <summary>
        /// Create builder
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        public IValidationRuleGroupBuilder_S GetBuilder(ValidationRuleRelation relation = ValidationRuleRelation.And)
        {
            Core_GetBuilder(relation);
            return this;
        }


        IValidationRuleGroupBuilder_S IValidationRuleGroupBuilder_S.Validate(Expression<Func<T, bool>> func)
        {
            Core_Validate(func);
            return this;
        }


        IValidationRuleGroupBuilder_S IValidationRuleGroupBuilder_S.ErrorMessage(Expression<Func<T, string>> func)
        {
            Core_ErrorMessage(func);
            return this;
        }


        IValidationRuleGroupBuilder_S IValidationRuleGroupBuilder_S.If(Expression<Func<T, bool>> func)
        {
            Core_If(func);
            return this;
        }


        IValidationRuleGroupBuilder_S IValidationRuleGroupBuilder_S.Next()
        {
            Core_Next();
            return this;
        }


        List<ValidationRuleGroup<T>> IValidationRuleGroupBuilder_S.Build()
        {
            return Core_GetRuleSet();
        }


        /// <summary>
        /// Validation rule group builder step
        /// </summary>
        public interface IValidationRuleGroupBuilder_S
        {
            /// <summary>
            /// Add a func as validation func
            /// </summary>
            /// <param name="func"></param>
            /// <returns></returns>
            IValidationRuleGroupBuilder_S Validate(Expression<Func<T, bool>> func);

            /// <summary>
            /// Specify error message func when validation failed
            /// </summary>
            /// <param name="func"></param>
            /// <returns></returns>
            IValidationRuleGroupBuilder_S ErrorMessage(Expression<Func<T, string>> func);

            /// <summary>
            /// Specify condition that trigger validation
            /// </summary>
            /// <param name="func"></param>
            /// <returns></returns>
            IValidationRuleGroupBuilder_S If(Expression<Func<T, bool>> func);

            /// <summary>
            /// End now and move to next building step
            /// </summary>
            /// <returns></returns>
            IValidationRuleGroupBuilder_S Next();

            /// <summary>
            /// Build validation rule group
            /// </summary>
            /// <returns></returns>
            List<ValidationRuleGroup<T>> Build();
        }

        #endregion
    }
}