// ReSharper disable once CheckNamespace

namespace System
{
#if NETSTANDARD1_4 || NET461
    /// <summary>
    /// String extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Split string with separator
        /// </summary>
        /// <param name="source">Source string to be split</param>
        /// <param name="separator">Separator</param>
        /// <returns></returns>
        public static string[] Split(this string source, string separator)
        {
            return source.Split(new[] {separator}, StringSplitOptions.None);
        }
    }
#endif
}