using System;
using System.Linq;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    public class ObjectVisitorContext<TObj, TValue> : IObjectVisitorContext<TObj, TValue>
    {
        public ObjectVisitorContext(string name, TValue value, TObj sourceObject, PropertyInfo propertyInfo)
        {
            Name = name;
            Value = value;
            SourceObject = sourceObject;
            PropertyInfo = propertyInfo;
        }

        public static ObjectVisitorContext<TObj, TValue> Create(string name,
            TValue value,
            TObj sourceObject,
            PropertyInfo propertyInfo)
        {
            return new ObjectVisitorContext<TObj, TValue>(name, value, sourceObject, propertyInfo);
        }

        public string Name { get; }
        public TValue Value { get; }
        public TObj SourceObject { get; }
        public PropertyInfo PropertyInfo { get; }
    }

    public class ObjectVisitorContext<TObj, TExtend, TValue> : IObjectVisitorContext<TObj, TExtend, TValue>
    {
        public ObjectVisitorContext(string name,
            TValue value,
            TObj sourceObject,
            TExtend extendObject,
            PropertyInfo propertyInfo)
        {
            Name = name;
            Value = value;
            ExtendObject = extendObject;
            SourceObject = sourceObject;
            PropertyInfo = propertyInfo;
        }

        public static ObjectVisitorContext<TObj, TExtend, TValue> Create(string name,
            TValue value,
            TObj sourceObject,
            TExtend extendObject,
            PropertyInfo propertyInfo)
        {
            return new ObjectVisitorContext<TObj, TExtend, TValue>(name,
                value,
                sourceObject,
                extendObject,
                propertyInfo);
        }

        public string Name { get; }
        public TValue Value { get; }
        public TExtend ExtendObject { get; }
        public TObj SourceObject { get; }
        public PropertyInfo PropertyInfo { get; }
    }


    public static class ObjectVisitorContext
    {
        public static MethodInfo GetCreateMethodInfo(Type inputType, Type valueType)
        {
            var type = typeof(ObjectVisitorContext<,>).MakeGenericType(inputType, valueType);
            var methodInfo = type.GetRuntimeMethods()
                .First(x => x.Name == nameof(ObjectVisitorContext<object, object>.Create));
            return methodInfo;
        }

        public static MethodInfo GetCreateMethodInfo(Type inputType, Type extendObjectType, Type valueType)
        {
            var type = typeof(ObjectVisitorContext<,,>).MakeGenericType(inputType, extendObjectType, valueType);
            var methodInfo = type.GetRuntimeMethods()
                .First(x => x.Name == nameof(ObjectVisitorContext<object, object, object>.Create));
            return methodInfo;
        }
    }
}