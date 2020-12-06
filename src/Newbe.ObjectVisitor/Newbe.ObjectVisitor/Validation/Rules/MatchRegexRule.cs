using System.Text.RegularExpressions;

namespace Newbe.ObjectVisitor.Validation
{
    internal class MatchRegexRule<T> : PropertyValidationRuleBase<T, string>
    {
        public MatchRegexRule(Regex regex)
        {
            MustExpression = value => value != null && regex.Match(value).Success;
            ErrorMessageExpression = (input, value, p) =>
                $"Value of {p.Name} must match to regex {regex}, but found {value}";
        }
    }
}