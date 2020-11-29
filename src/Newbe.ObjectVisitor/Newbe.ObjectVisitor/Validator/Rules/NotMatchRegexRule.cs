using System;
using System.Text.RegularExpressions;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public class NotMatchRegexRule<T> : PropertyValidationRuleBase<T, string>
    {
        public NotMatchRegexRule(Regex regex)
        {
            MustExpression = value => !regex.Match(value).Success;
            ErrorMessageExpression = (input, value, p) =>
                $"Value of {p.Name} must not match to regex {regex}, but found {value}";
        }
    }
}