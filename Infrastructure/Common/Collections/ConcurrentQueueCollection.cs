namespace ProcessingTools.Common.Collections
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class ConcurrentQueueCollection<T> : ConcurrentQueue<T>, ICollection<T>
    {
        public ConcurrentQueueCollection()
            : base()
        {
        }

        public ConcurrentQueueCollection(IEnumerable<T> collection)
            : base(collection: collection)
        {
        }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            this.Enqueue(item);
        }

        public void Clear()
        {
            while (!this.IsEmpty)
            {
                T item;
                this.TryDequeue(out item);
            }
        }

        public bool Contains(T item)
        {
            foreach (var currentItem in this)
            {
                if (currentItem.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Remove(T item)
        {
            return false;
        }
    }
}
