﻿using System.Text.RegularExpressions;

namespace Newbe.ObjectVisitor.Validation
{
    internal class NotMatchRegexRule<T> : PropertyValidationRuleBase<T, string>
    {
        public NotMatchRegexRule(Regex regex)
        {
            MustExpression = value => value == null || !regex.Match(value).Success;
            ErrorMessageExpression = (input, value, p) =>
                $"Value of {p.Name} must not match to regex {regex}, but found {value}";
        }
    }
}