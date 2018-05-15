// <copyright file="XslTransformer.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Xml
{
    using System;
    using System.Xml.Xsl;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Services.Contracts.IO;

    /// <summary>
    /// XSL transformer.
    /// </summary>
    public class XslTransformer : XslTransformerBase, IXslTransformer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XslTransformer"/> class.
        /// </summary>
        /// <param name="xslFileName">XSL file name.</param>
        /// <param name="cache">Cache.</param>
        /// <param name="xmlReadService">XML read service.</param>
        public XslTransformer(string xslFileName, IXslTransformCache cache, IXmlReadService xmlReadService)
            : base(xmlReadService)
        {
            if (string.IsNullOrWhiteSpace(xslFileName))
            {
                throw new ArgumentNullException(nameof(xslFileName));
            }

            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            this.XslCompiledTransform = cache[xslFileName];
        }

        /// <inheritdoc/>
        protected override XslCompiledTransform XslCompiledTransform { get; }
    }
}
