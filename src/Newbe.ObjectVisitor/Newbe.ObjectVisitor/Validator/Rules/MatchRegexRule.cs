using System;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public class MatchRegexRule<T> : PropertyValidationRuleBase<T, string>
    {
        public MatchRegexRule(Regex regex)
        {
            MustExpression = value => regex.Match(value).Success;
            ErrorMessageExpression = (input, value, p) =>
                $"Value of {p.Name} must match to regex {regex}, but found {value}";
        }
    }

    public static class MatchRegexRuleFactory
    {
        private static readonly ConcurrentDictionary<string, Regex> Regexes = new ConcurrentDictionary<string, Regex>();

        // TODO move to global factories
        public static Func<string, Regex> RegexFactory { get; set; }

        static MatchRegexRuleFactory()
        {
            RegexFactory = pattern => new Regex(pattern, RegexOptions.Compiled | RegexOptions.Singleline);
        }

        public static MatchRegexRule<T> Create<T>(string pattern)
        {
            var regex = Regexes.GetOrAdd(pattern, RegexFactory);
            return new MatchRegexRule<T>(regex);
        }

        public static MatchRegexRule<T> Create<T>(Regex regex)
        {
            return new MatchRegexRule<T>(regex);
        }
    }
}