using System.Collections;
using System.Collections.Generic;

namespace Newbe.ObjectVisitor
{
    public class OvBuilderContext<T> : IOvBuilderContext<T>
    {
        private readonly IOvBuilderContext _context;

        public OvBuilderContext(
            IOvBuilderContext context)
        {
            _context = context;
        }

        public IEnumerator<IOvBuilderContextItem> GetEnumerator()
        {
            return _context.GetEnumerator();
        }

        public void Add(IOvBuilderContextItem item)
        {
            _context.Add(item);
        }

        public void Clear()
        {
            _context.Clear();
        }

        public bool Contains(IOvBuilderContextItem item)
        {
            return _context.Contains(item);
        }

        public void CopyTo(IOvBuilderContextItem[] array, int arrayIndex)
        {
            _context.CopyTo(array, arrayIndex);
        }

        public bool Remove(IOvBuilderContextItem item)
        {
            return _context.Remove(item);
        }

        public int Count => _context.Count;

        public bool IsReadOnly => _context.IsReadOnly;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class OvBuilderContext : IOvBuilderContext
    {
        private readonly List<IOvBuilderContextItem> _list;

        public OvBuilderContext()
        {
            _list = new List<IOvBuilderContextItem>();
        }

        public bool IsReadOnly => ((IList) _list).IsReadOnly;

        public void Add(IOvBuilderContextItem contextItem)
        {
            _list.Add(contextItem);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(IOvBuilderContextItem contextItem)
        {
            return _list.Contains(contextItem);
        }

        public void CopyTo(IOvBuilderContextItem[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(IOvBuilderContextItem contextItem)
        {
            return _list.Remove(contextItem);
        }

        public int Count => _list.Count;


        public IEnumerator<IOvBuilderContextItem> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class OvBuilderContext<TSourceObject, TExtendObject> : IOvBuilderContext<TSourceObject, TExtendObject>
    {
        private readonly IOvBuilderContext _context;

        public OvBuilderContext(
            IOvBuilderContext context)
        {
            _context = context;
        }

        public IEnumerator<IOvBuilderContextItem> GetEnumerator()
        {
            return _context.GetEnumerator();
        }

        public void Add(IOvBuilderContextItem item)
        {
            _context.Add(item);
        }

        public void Clear()
        {
            _context.Clear();
        }

        public bool Contains(IOvBuilderContextItem item)
        {
            return _context.Contains(item);
        }

        public void CopyTo(IOvBuilderContextItem[] array, int arrayIndex)
        {
            _context.CopyTo(array, arrayIndex);
        }

        public bool Remove(IOvBuilderContextItem item)
        {
            return _context.Remove(item);
        }

        public int Count => _context.Count;

        public bool IsReadOnly => _context.IsReadOnly;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}