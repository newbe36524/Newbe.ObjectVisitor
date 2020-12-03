using System.Reflection;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Context of visiting a property of target object
    /// </summary>
    /// <typeparam name="TSourceObject">Type of source object while visiting</typeparam>
    /// <typeparam name="TValue">Type of value while visiting</typeparam>
    public interface IObjectVisitorContext<out TSourceObject, TValue>
    {
        /// <summary>
        /// Name of visiting property
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Value of visiting property
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// Source object
        /// </summary>
        public TSourceObject SourceObject { get; }

        /// <summary>
        /// Property info of visiting property
        /// </summary>
        public PropertyInfo PropertyInfo { get; }
    }

    /// <summary>
    /// Context of visiting a object
    /// </summary>
    /// <typeparam name="TSourceObject">Type of source object while visiting</typeparam>
    /// <typeparam name="TExtend">Type of extend data while visiting</typeparam>
    /// <typeparam name="TValue">Type of value while visiting</typeparam>
    public interface IObjectVisitorContext<out TSourceObject, out TExtend, TValue>
    {
        /// <summary>
        /// Name of visiting property
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Value of visiting property
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// Extend data
        /// </summary>
        public TExtend ExtendObject { get; }

        /// <summary>
        /// Source object
        /// </summary>
        public TSourceObject SourceObject { get; }

        /// <summary>
        /// Property info of visiting property
        /// </summary>
        public PropertyInfo PropertyInfo { get; }
    }
}