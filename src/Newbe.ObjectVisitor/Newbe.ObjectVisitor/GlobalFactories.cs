using System;
using System.Text.RegularExpressions;

namespace Newbe.ObjectVisitor
{
    public static class GlobalFactories
    {
        public static class Validator
        {
            static Validator()
            {
                RegexFactory = pattern => new Regex(pattern, RegexOptions.Compiled | RegexOptions.Singleline);
            }

            public static Func<string, Regex> RegexFactory { get; set; }
        }
    }
}