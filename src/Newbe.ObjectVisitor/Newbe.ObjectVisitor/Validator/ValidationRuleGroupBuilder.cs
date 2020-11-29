using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator
{
    public class ValidationRuleGroupBuilder<T> : Newbe.ObjectVisitor.IFluentApi
        , ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S
        , ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_V
    {
        private readonly List<ValidationRuleGroup<T>> _context;
        private readonly ValidationRuleGroup<T> _nowGroup;
        private ValidationRule<T> nowRule;

        public ValidationRuleGroupBuilder(List<ValidationRuleGroup<T>> context)
        {
            _context = context;
            _nowGroup = new ValidationRuleGroup<T>();
            nowRule = new ValidationRule<T>();
        }

        #region UserImpl

        private void Core_GetBuilder(ValidationRuleRelation relation = ValidationRuleRelation.All)
        {
            _nowGroup.RuleRelation = relation;
        }


        private void Core_Validate(Expression<Func<T, bool>> func)
        {
            nowRule.MustExpression = func;
            _nowGroup.Add(nowRule);
        }


        private void Core_ErrorMessage(Expression<Func<T, string>> func)
        {
            nowRule.ErrorMessageExpression = func;
        }


        private void Core_If(Expression<Func<T, bool>> func)
        {
            nowRule.IfExpression = func;
        }


        private void Core_Next()
        {
            nowRule = new ValidationRule<T>();
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

        public IValidationRuleGroupBuilder_S GetBuilder(ValidationRuleRelation relation = ValidationRuleRelation.All)
        {
            Core_GetBuilder(relation);
            return this;
        }


        IValidationRuleGroupBuilder_V IValidationRuleGroupBuilder_S.Validate(Expression<Func<T, bool>> func)
        {
            Core_Validate(func);
            return this;
        }


        IValidationRuleGroupBuilder_V IValidationRuleGroupBuilder_V.ErrorMessage(Expression<Func<T, string>> func)
        {
            Core_ErrorMessage(func);
            return this;
        }


        IValidationRuleGroupBuilder_S IValidationRuleGroupBuilder_S.If(Expression<Func<T, bool>> func)
        {
            Core_If(func);
            return this;
        }


        IValidationRuleGroupBuilder_S IValidationRuleGroupBuilder_V.Next()
        {
            Core_Next();
            return this;
        }


        List<ValidationRuleGroup<T>> IValidationRuleGroupBuilder_S.GetRuleSet()
        {
            return Core_GetRuleSet();
        }


        public interface IValidationRuleGroupBuilder_S
        {
            IValidationRuleGroupBuilder_S If(Expression<Func<T, bool>> func);


            IValidationRuleGroupBuilder_V Validate(Expression<Func<T, bool>> func);


            List<ValidationRuleGroup<T>> GetRuleSet();
        }


        public interface IValidationRuleGroupBuilder_V
        {
            IValidationRuleGroupBuilder_S Next();


            IValidationRuleGroupBuilder_V ErrorMessage(Expression<Func<T, string>> func);
        }

        #endregion
    }
}