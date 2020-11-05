using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Newbe.ObjectVisitor
{
    public static class DictionaryExtensions
    {
        public static Dictionary<string,object> CollectAsDictionary<T>(this T obj)
        {
            var dic = new Dictionary<string,object>();
            DictionaryVisitor<T>.Instance.Run(obj, dic);
            return dic;
        }

        private static class DictionaryVisitor<T>
        {
            internal static readonly ICachedObjectVisitor<T, Dictionary<string,object>> Instance = CreateDictionaryVisitor();
            private static ICachedObjectVisitor<T, Dictionary<string,object>> CreateDictionaryVisitor()
            {
                var re = default(T)!
                    .V()
                    .WithExtendObject<T, Dictionary<string,object>>()
                    .ForEach((name, value, s) => s.Add(name,value))
                    .Cache();
                return re;
            }
        }
    }
}