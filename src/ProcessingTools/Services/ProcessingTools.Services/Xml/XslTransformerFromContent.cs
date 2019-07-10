// <copyright file="XslTransformerFromContent.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.IO;
using ProcessingTools.Contracts.Services.Xml;

namespace ProcessingTools.Services.Xml
{
    using System;
    using System.Xml.Xsl;

    /// <summary>
    /// XSL transformer from specified XSL content.
    /// </summary>
    public class XslTransformerFromContent : XslTransformerBase, IXslTransformerFromContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XslTransformerFromContent"/> class.
        /// </summary>
        /// <param name="xslContent">XSL content.</param>
        /// <param name="cache">Transform cache.</param>
        /// <param name="xmlReadService">XML read service.</param>
        public XslTransformerFromContent(string xslContent, IXslTransformCache cache, IXmlReadService xmlReadService)
            : base(xmlReadService)
        {
            if (string.IsNullOrWhiteSpace(xslContent))
            {
                throw new ArgumentNullException(nameof(xslContent));
            }

            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            this.XslCompiledTransform = cache[xslContent];
        }

        /// <inheritdoc/>
        protected override XslCompiledTransform XslCompiledTransform { get; }
    }
}
