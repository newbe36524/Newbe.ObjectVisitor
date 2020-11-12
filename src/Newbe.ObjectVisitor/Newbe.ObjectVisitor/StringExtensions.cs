// ReSharper disable once CheckNamespace

namespace System
{
#if NETSTANDARD1_4 || NET461
    public static class StringExtensions
    {
        public static string[] Split(this string source, string d)
        {
            return source.Split(new[] {d}, StringSplitOptions.None);
        }
    }
#endif
}