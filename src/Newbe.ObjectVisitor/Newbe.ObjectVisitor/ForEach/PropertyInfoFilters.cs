using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    public static class PropertyInfoFilters
    {
        public static readonly Func<PropertyInfo, bool> AllPropertyInfo = x => true;

        public static bool IsOrImplOf<TInterface>(this PropertyInfo info)
        {
            return info.PropertyType.IsOrImplOf<TInterface>();
        }

        public static bool IsOrImplOf<TInterface>(this Type type)
        {
            return type.IsOrImplOf(typeof(TInterface));
        }
        
        
        public static bool IsOrImplOf(this Type type,Type interfaceType)
        {
            return GetAll().Contains(interfaceType);

            IEnumerable<Type> GetAll()
            {
                yield return type;
                foreach (var t in GetAllInterfaces(type))
                {
                    yield return t;
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
            for (; (object) type != null; type = type.GetTypeInfo().BaseType)
            {
                source.AddRange(type.GetTypeInfo().ImplementedInterfaces);
            }

            return source.Distinct().ToArray();
        }
    }
}