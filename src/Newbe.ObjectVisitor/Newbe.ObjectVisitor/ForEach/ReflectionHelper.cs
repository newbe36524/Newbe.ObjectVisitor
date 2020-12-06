using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    internal static class ReflectionHelper
    {
        internal static IEnumerable<Type> GetAllInterfaces(this Type type)
        {
            var source = new List<Type>();
            for (; (object) type != null; type = type.GetTypeInfo().BaseType)
            {
                source.AddRange(type.GetTypeInfo().ImplementedInterfaces);
            }

            return source.Distinct().ToArray();
        }
    }
}