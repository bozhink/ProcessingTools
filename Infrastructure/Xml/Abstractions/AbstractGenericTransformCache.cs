namespace ProcessingTools.Xml.Abstractions
{
    using System.Collections.Concurrent;
    using Contracts.Cache;

    public abstract class AbstractGenericTransformCache<T> : IGenericTransformCache<T>
    {
        protected abstract ConcurrentDictionary<string, T> TransformObjects { get; }

        public virtual T this[string fileName]
        {
            get
            {
                var transform = this.TransformObjects.GetOrAdd(fileName, this.GetTransformObject);
                return transform;
            }
        }

        public virtual bool Remove(string fileName)
        {
            T value;
            var result = this.TransformObjects.TryRemove(fileName, out value);
            return result;
        }

        public virtual bool RemoveAll()
        {
            var result = true;
            foreach (var key in this.TransformObjects.Keys)
            {
                T value;
                result &= this.TransformObjects.TryRemove(key, out value);
            }

            return result;
        }

        protected abstract T GetTransformObject(string fileName);
    }
}
