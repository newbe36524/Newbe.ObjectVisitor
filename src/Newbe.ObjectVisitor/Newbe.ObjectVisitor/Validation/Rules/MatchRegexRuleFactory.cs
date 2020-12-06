using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Newbe.ObjectVisitor.Validation
{
    internal static class MatchRegexRuleFactory
    {
        private static readonly ConcurrentDictionary<string, Regex> Regexes = new ConcurrentDictionary<string, Regex>();

        public static MatchRegexRule<T> Create<T>(string pattern)
        {
            var regex = Regexes.GetOrAdd(pattern, GlobalFactories.Validation.RegexFactory);
            return new MatchRegexRule<T>(regex);
        }

        public static MatchRegexRule<T> Create<T>(Regex regex)
        {
            return new MatchRegexRule<T>(regex);
        }
    }
}