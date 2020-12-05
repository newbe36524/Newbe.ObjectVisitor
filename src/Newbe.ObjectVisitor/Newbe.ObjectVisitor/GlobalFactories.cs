using System;
using System.Text.RegularExpressions;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Global factories
    /// </summary>
    public static class GlobalFactories
    {
        /// <summary>
        /// Factories for Validation scope
        /// </summary>
        public static class Validation
        {
            static Validation()
            {
                RegexFactory = pattern => new Regex(pattern, RegexOptions.Compiled | RegexOptions.Singleline);
            }

            /// <summary>
            /// Regex factory
            /// </summary>
            public static Func<string, Regex> RegexFactory { get; set; }
        }
    }
}