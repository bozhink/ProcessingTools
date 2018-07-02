// <copyright file="XslTransformCacheFromFile.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Xml
{
    using System.Collections.Concurrent;
    using System.Xml;
    using System.Xml.Xsl;
    using ProcessingTools.Contracts.Xml;

    /// <summary>
    /// XSL transform cache for XSL style-sheets specified by file name.
    /// </summary>
    public class XslTransformCacheFromFile : TransformCache<XslCompiledTransform>, IXslTransformCache
    {
        private readonly ConcurrentDictionary<string, XslCompiledTransform> transformObjects = new ConcurrentDictionary<string, XslCompiledTransform>();

        /// <inheritdoc/>
        protected override ConcurrentDictionary<string, XslCompiledTransform> TransformObjects => this.transformObjects;

        /// <inheritdoc/>
        protected override XslCompiledTransform GetTransformObject(string key)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            XsltSettings settings = new XsltSettings(true, true);
            XmlUrlResolver resolver = new XmlUrlResolver();
            transform.Load(key, settings, resolver);
            return transform;
        }
    }
}
