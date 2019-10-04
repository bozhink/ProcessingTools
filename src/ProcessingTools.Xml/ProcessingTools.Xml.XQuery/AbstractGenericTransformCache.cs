// <copyright file="AbstractGenericTransformCache.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Xml.XQuery
{
    using System.Collections.Concurrent;

    /// <summary>
    /// Transform cache.
    /// </summary>
    /// <typeparam name="T">Type of transform.</typeparam>
    public abstract class AbstractGenericTransformCache<T> : ITransformCache<T>
    {
        /// <summary>
        /// Gets the cache of transform objects.
        /// </summary>
        protected abstract ConcurrentDictionary<string, T> TransformObjects { get; }

        /// <inheritdoc/>
        public virtual T this[string key] => this.TransformObjects.GetOrAdd(key, this.GetTransformObject);

        /// <inheritdoc/>
        public virtual bool Remove(string key)
        {
            var result = this.TransformObjects.TryRemove(key, out _);
            return result;
        }

        /// <inheritdoc/>
        public virtual bool RemoveAll()
        {
            var result = true;
            foreach (var key in this.TransformObjects.Keys)
            {
                result &= this.TransformObjects.TryRemove(key, out _);
            }

            return result;
        }

        /// <summary>
        /// Gets the transform object.
        /// </summary>
        /// <param name="fileName">The name of the source file.</param>
        /// <returns>Transform object.</returns>
        protected abstract T GetTransformObject(string fileName);
    }
}
