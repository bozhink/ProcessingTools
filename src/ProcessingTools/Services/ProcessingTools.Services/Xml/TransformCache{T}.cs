// <copyright file="TransformCache{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Xml
{
    using System.Collections.Concurrent;
    using ProcessingTools.Contracts.Xml;

    /// <summary>
    /// Abstract generic transform cache.
    /// </summary>
    /// <typeparam name="T">Type of the transform object.</typeparam>
    public abstract class TransformCache<T> : ITransformCache<T>
    {
        /// <summary>
        /// Gets transform objects.
        /// </summary>
        protected abstract ConcurrentDictionary<string, T> TransformObjects { get; }

        /// <inheritdoc/>
        public virtual T this[string key] => this.TransformObjects.GetOrAdd(key, this.GetTransformObject);

        /// <inheritdoc/>
        public virtual bool Remove(string key)
        {
            return this.TransformObjects.TryRemove(key, out T value);
        }

        /// <inheritdoc/>
        public virtual bool RemoveAll()
        {
            var result = true;
            foreach (var key in this.TransformObjects.Keys)
            {
                result &= this.TransformObjects.TryRemove(key, out T value);
            }

            return result;
        }

        /// <summary>
        /// Gets transform object by file name.
        /// </summary>
        /// <param name="key">Key of the source file.</param>
        /// <returns>Transform object.</returns>
        protected abstract T GetTransformObject(string key);
    }
}
