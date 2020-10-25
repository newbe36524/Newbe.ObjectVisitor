using System.Linq;

namespace Newbe.ObjectVisitor
{
    internal static class BuilderExtensions
    {
        public static bool TryGetObject<T>(this IOvBuilderContext<T> builderContext, out T obj)
        {
            var re = TryGetContextItem<SourceObjectOvBuilderContextItem>(builderContext, out var item);
            obj = (T) item?.SourceObject!;
            return re;
        }

        public static bool TryGetExtObject<T, TExtend>(this IOvBuilderContext<T, TExtend> builderContext,
            out TExtend obj)
        {
            var re = TryGetContextItem<ExtendObjectOvBuilderContextItem>(builderContext, out var item);
            obj = (TExtend) item?.ExtendObject!;
            return re;
        }

        internal static bool TryGetContextItem<TItem>(this IOvBuilderContext builderContext,
            out TItem? obj)
            where TItem : class
        {
            var item = builderContext.OfType<TItem>().FirstOrDefault();
            if (item != null)
            {
                obj = item;
                return true;
            }

            obj = default!;
            return false;
        }
    }
}