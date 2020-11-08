using System;
using System.Text;

namespace Newbe.ObjectVisitor
{
    public static class FormatToStringExtensions
    {
        public static string FormatToString<T>(this T obj)
        {
            var sb = new StringBuilder();
            FormatStringVisitor<T>.Instance.Run(obj, sb);
            var re = sb.ToString();
            return re;
        }

        private static class FormatStringVisitor<T>
        {
            internal static readonly ICachedObjectVisitor<T, StringBuilder> Instance = CreateFormatToStringVisitor();

            private static ICachedObjectVisitor<T, StringBuilder> CreateFormatToStringVisitor()
            {
                var re = default(T)!
                    .V()
                    .WithExtendObject<T, StringBuilder>()
                    .ForEach((name, value, s) => s.AppendFormat("{0}:{1}{2}", name, value,
                        Environment.NewLine))
                    .Cache();
                return re;
            }
        }
    }
}