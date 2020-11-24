using System;
using System.Collections.Concurrent;
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

    public static class NotMatchRegexRuleFactory
    {
        private static readonly ConcurrentDictionary<string, Regex> Regexes = new ConcurrentDictionary<string, Regex>();

        // TODO move to global factories
        public static Func<string, Regex> RegexFactory { get; set; }

        static NotMatchRegexRuleFactory()
        {
            RegexFactory = pattern => new Regex(pattern, RegexOptions.Compiled | RegexOptions.Singleline);
        }

        public static NotMatchRegexRule<T> Create<T>(string pattern)
        {
            var regex = Regexes.GetOrAdd(pattern, RegexFactory);
            return new NotMatchRegexRule<T>(regex);
        }

        public static NotMatchRegexRule<T> Create<T>(Regex regex)
        {
            return new NotMatchRegexRule<T>(regex);
        }
    }
}