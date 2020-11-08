using System.Reflection;

namespace Newbe.ObjectVisitor
{
    public interface IObjectVisitorContext<out TSourceObject, TValue>
    {
        public string Name { get; }
        public TValue Value { get; set; }
        public TSourceObject SourceObject { get; }
        public PropertyInfo PropertyInfo { get; }
    }

    public interface IObjectVisitorContext<out TSourceObject, out TExtend, TValue>
    {
        public string Name { get; }
        public TValue Value { get; set; }
        public TExtend ExtendObject { get; }
        public TSourceObject SourceObject { get; }
        public PropertyInfo PropertyInfo { get; }
    }
}