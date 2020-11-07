using System;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    public abstract class ObjectVisitorContextBase<TObj, TValue>
    {
        private readonly Func<TObj, TValue> _getter;
        private readonly Action<TObj, TValue> _setter;

        protected ObjectVisitorContextBase(string name,
            TObj sourceObject,
            PropertyInfo propertyInfo,
            Func<TObj, TValue> getter,
            Action<TObj, TValue> setter)
        {
            _getter = getter;
            _setter = setter;
            Name = name;
            SourceObject = sourceObject;
            PropertyInfo = propertyInfo;
        }

        public string Name { get; }

        public TValue Value
        {
            get => _getter(SourceObject);
            set => _setter(SourceObject, value);
        }

        public TObj SourceObject { get; }
        public PropertyInfo PropertyInfo { get; }
    }
}