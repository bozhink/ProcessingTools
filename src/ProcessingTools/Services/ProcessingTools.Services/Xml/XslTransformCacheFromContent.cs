// <copyright file="XslTransformCacheFromContent.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Xml
{
    using System.Collections.Concurrent;
    using System.Xml;
    using System.Xml.Xsl;
    using ProcessingTools.Contracts.Services.Xml;

    /// <summary>
    /// XSL transform cache for XSL style-sheets specified by content.
    /// </summary>
    public class XslTransformCacheFromContent : TransformCache<XslCompiledTransform>, IXslTransformCache
    {
        private readonly ConcurrentDictionary<string, XslCompiledTransform> transformObjects = new ConcurrentDictionary<string, XslCompiledTransform>();

        /// <inheritdoc/>
        protected override ConcurrentDictionary<string, XslCompiledTransform> TransformObjects => this.transformObjects;

        /// <inheritdoc/>
        protected override XslCompiledTransform GetTransformObject(string key)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            XsltSettings settings = new XsltSettings(false, false);
            XmlUrlResolver resolver = new XmlUrlResolver();
            XmlDocument styleSheet = new XmlDocument();
            styleSheet.LoadXml(key);
            transform.Load(styleSheet, settings, resolver);
            return transform;
        }
    }
}
