using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AgileObjects.NetStandardPolyfills;

namespace Newbe.ObjectVisitor
{
    public static class PropertyInfoFilters
    {
        public static readonly Func<PropertyInfo, bool> AllPropertyInfo = x => true;

        public static bool IsOrImplOf<TInterface>(this PropertyInfo info)
        {
            return GetAll().Contains(typeof(TInterface));

            IEnumerable<Type> GetAll()
            {
                yield return info.PropertyType;
                foreach (var type in GetAllInterfaces(info.PropertyType))
                {
                    yield return type;
                }
            }
        }

        /// <summary>
        /// Gets all the interfaces implemented or inherited by the current <paramref name="type" />.
        /// </summary>
        /// <param name="type">The Type for which to retrieve the implemented interfaces.</param>
        /// <returns>
        /// An array of Types representing all the interfaces implemented or inherited by the current
        /// <paramref name="type" />, or an empty array if no interfaces are implemented or inherited.
        /// </returns>
        public static Type[] GetAllInterfaces(this Type type)
        {
            var source = new List<Type>();
            for (; (object) type != null; type = type.GetBaseType())
            {
                source.AddRange(type.GetTypeInfo().ImplementedInterfaces);
            }

            return source.Distinct().ToArray();
        }
    }
}