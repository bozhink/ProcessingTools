// <copyright file="XQueryTransformCache.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Xml.XQuery
{
    using System.Collections.Concurrent;
    using System.IO;

    /// <summary>
    /// XQuery transform cache.
    /// </summary>
    public class XQueryTransformCache : AbstractGenericTransformCache<IXQueryTransform>, IXQueryTransformCache
    {
        private static readonly ConcurrentDictionary<string, IXQueryTransform> XQueryTransformObjects = new ConcurrentDictionary<string, IXQueryTransform>();

        /// <inheritdoc/>
        protected override ConcurrentDictionary<string, IXQueryTransform> TransformObjects => XQueryTransformObjects;

        /// <inheritdoc/>
        protected override IXQueryTransform GetTransformObject(string fileName)
        {
            var query = File.ReadAllText(fileName);

            var transform = new XQueryTransform();
            transform.Load(query: query);

            return transform;
        }
    }
}
