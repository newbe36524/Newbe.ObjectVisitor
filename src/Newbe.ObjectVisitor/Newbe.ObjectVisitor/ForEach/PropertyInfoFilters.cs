using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Extend method for creating PropertyInfo filter
    /// </summary>
    public static class PropertyInfoFilters
    {
        /// <summary>
        /// All properties ok
        /// </summary>
        public static readonly Func<PropertyInfo, bool> AllPropertyInfo = x => true;

        /// <summary>
        /// Check the type of property 'is' or 'implement' the specified <typeparamref name="TInterface"/>
        /// </summary>
        /// <param name="info">PropertyInfo to be checked</param>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        public static bool IsOrImplOf<TInterface>(this PropertyInfo info)
        {
            return info.PropertyType.IsOrImplOf<TInterface>();
        }

        /// <summary>
        /// Check the type 'is' or 'implement' the specified <typeparamref name="TInterface"/>
        /// </summary>
        /// <param name="type">Type to be checked</param>
        /// <typeparam name="TInterface">Type of target interface</typeparam>
        /// <returns></returns>
        public static bool IsOrImplOf<TInterface>(this Type type)
        {
            return type.IsOrImplOf(typeof(TInterface));
        }

        /// <summary>
        /// Check the type 'is' or 'implement' the specified <paramref name="interfaceType"/>
        /// </summary>
        /// <param name="type">Type to be checked</param>
        /// <param name="interfaceType">Type of target interface</param>
        /// <returns></returns>
        public static bool IsOrImplOf(this Type type, Type interfaceType)
        {
            return GetAll().Contains(interfaceType);

            IEnumerable<Type> GetAll()
            {
                yield return type;
                foreach (var t in type.GetAllInterfaces())
                {
                    yield return t;
                }
            }
        }
    }
}