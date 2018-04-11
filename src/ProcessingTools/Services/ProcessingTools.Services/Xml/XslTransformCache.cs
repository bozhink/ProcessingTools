// <copyright file="XslTransformCache.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Xml
{
    using System.Collections.Concurrent;
    using System.Xml.Xsl;
    using ProcessingTools.Contracts.Xml;

    /// <summary>
    /// XSL transform cache.
    /// </summary>
    public class XslTransformCache : TransformCache<XslCompiledTransform>, IXslTransformCache
    {
        private static readonly ConcurrentDictionary<string, XslCompiledTransform> XslCompiledTransformObjects = new ConcurrentDictionary<string, XslCompiledTransform>();

        /// <inheritdoc/>
        protected override ConcurrentDictionary<string, XslCompiledTransform> TransformObjects => XslCompiledTransformObjects;

        /// <inheritdoc/>
        protected override XslCompiledTransform GetTransformObject(string fileName)
        {
            var transform = new XslCompiledTransform();
            transform.Load(fileName);
            return transform;
        }
    }
}
