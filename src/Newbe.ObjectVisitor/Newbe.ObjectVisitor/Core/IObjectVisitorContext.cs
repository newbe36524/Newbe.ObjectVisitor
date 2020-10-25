using System.Reflection;

namespace Newbe.ObjectVisitor
{
    public interface IObjectVisitorContext<out TSourceObject, out TValue>
    {
        public string Name { get; }
        public TValue Value { get; }
        public TSourceObject SourceObject { get; }
        public PropertyInfo PropertyInfo { get; }
    }

    public interface IObjectVisitorContext<out TSourceObject, out TExtend, out TValue>
    {
        public string Name { get; }
        public TValue Value { get; }
        public TExtend ExtendObject { get; }
        public TSourceObject SourceObject { get; }
        public PropertyInfo PropertyInfo { get; }
    }
}