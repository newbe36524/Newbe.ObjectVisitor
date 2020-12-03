namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Cache object visitor extensions
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// Create cached object visitor
        /// </summary>
        /// <param name="builderContext">builder context of object visitor</param>
        /// <typeparam name="T">Type of target type</typeparam>
        /// <returns>Cached object visitor</returns>
        public static ICachedObjectVisitor<T> Cache<T>(this OVBuilder<T>.IOVBuilder_V builderContext)
        {
            var visitor = builderContext.CreateVisitor();
            return new CachedObjectVisitor<T>(visitor);
        }

        /// <summary>
        /// Create cached object visitor
        /// </summary>
        /// <param name="builderContext">builder context of object visitor</param>
        /// <typeparam name="T">Type of target type</typeparam>
        /// <typeparam name="TExtend">Type of extend data.</typeparam>
        /// <returns>Cached object visitor</returns>
        public static ICachedObjectVisitor<T, TExtend> Cache<T, TExtend>(
            this OVBuilderExt<T, TExtend>.IOVBuilderExt_V builderContext)
        {
            var visitor = builderContext.CreateVisitor();
            return new CachedObjectVisitor<T, TExtend>(visitor);
        }

        /// <summary>
        /// Run a cache object visitor
        /// </summary>
        /// <param name="visitor">visitor</param>
        /// <param name="obj">Target object</param>
        /// <typeparam name="T">Type of target type</typeparam>
        public static void Run<T>(this ICachedObjectVisitor<T> visitor, T obj)
        {
            visitor.Action.Invoke(obj);
        }

        /// <summary>
        /// Run a cache object visitor
        /// </summary>
        /// <param name="visitor">visitor</param>
        /// <param name="obj">Target object</param>
        /// <param name="extendObj">Extend data to run visitor</param>
        /// <typeparam name="T">Type of target type</typeparam>
        /// <typeparam name="TExtend">Type of extend data.</typeparam>
        public static void Run<T, TExtend>(this ICachedObjectVisitor<T, TExtend> visitor, T obj, TExtend extendObj)
        {
            visitor.Action.Invoke(obj, extendObj);
        }
    }
}