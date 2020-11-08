using System;
using System.Linq;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    public class ObjectVisitorContext<TObj, TValue> : ObjectVisitorContextBase<TObj, TValue>,
        IObjectVisitorContext<TObj, TValue>
    {
        public ObjectVisitorContext(string name,
            TObj sourceObject,
            PropertyInfo propertyInfo,
            Func<TObj, TValue> getter,
            Action<TObj, TValue> setter)
            : base(name, sourceObject, propertyInfo, getter, setter)
        {
        }

        public static ObjectVisitorContext<TObj, TValue> Create(string name,
            TObj sourceObject,
            PropertyInfo propertyInfo,
            Func<TObj, TValue> getter,
            Action<TObj, TValue> setter)
        {
            return new ObjectVisitorContext<TObj, TValue>(name, sourceObject, propertyInfo, getter, setter);
        }
    }

    public class ObjectVisitorContext<TObj, TExtend, TValue> : ObjectVisitorContextBase<TObj, TValue>,
        IObjectVisitorContext<TObj, TExtend, TValue>
    {
        public ObjectVisitorContext(string name,
            TObj sourceObject,
            TExtend extendObject,
            PropertyInfo propertyInfo,
            Func<TObj, TValue> getter,
            Action<TObj, TValue> setter)
            : base(name, sourceObject, propertyInfo, getter, setter)
        {
            ExtendObject = extendObject;
        }

        public static ObjectVisitorContext<TObj, TExtend, TValue> Create(string name,
            TObj sourceObject,
            TExtend extendObject,
            PropertyInfo propertyInfo,
            Func<TObj, TValue> getter,
            Action<TObj, TValue> setter)
        {
            return new ObjectVisitorContext<TObj, TExtend, TValue>(name,
                sourceObject,
                extendObject,
                propertyInfo,
                getter,
                setter);
        }

        public TExtend ExtendObject { get; }
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