using System.Collections.Concurrent;
using ProcessingTools.Services.Contracts.Xml;

namespace ProcessingTools.Xml
{
    public abstract class AbstractGenericTransformCache<T> : ITransformCache<T>
    {
        protected abstract ConcurrentDictionary<string, T> TransformObjects { get; }

        public virtual T this[string key]
        {
            get
            {
                var transform = this.TransformObjects.GetOrAdd(key, this.GetTransformObject);
                return transform;
            }
        }

        public virtual bool Remove(string key)
        {
            var result = this.TransformObjects.TryRemove(key, out T value);
            return result;
        }

        public virtual bool RemoveAll()
        {
            var result = true;
            foreach (var key in this.TransformObjects.Keys)
            {
                result &= this.TransformObjects.TryRemove(key, out T value);
            }

            return result;
        }

        protected abstract T GetTransformObject(string fileName);
    }
}
