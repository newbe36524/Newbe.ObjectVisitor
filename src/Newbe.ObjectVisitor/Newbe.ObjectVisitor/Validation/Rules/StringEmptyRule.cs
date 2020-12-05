﻿namespace Newbe.ObjectVisitor.Validation
{
    internal class StringEmptyRule<T> : PropertyValidationRuleBase<T, string>
    {
        public StringEmptyRule()
        {
            MustExpression = value => string.IsNullOrWhiteSpace(value);
            ErrorMessageExpression = (input, value, p) => $"Value of {p.Name} must not be empty, but found {value}";
        }
    }
}