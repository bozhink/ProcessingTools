// <copyright file="XslTransformerFromFile.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Xml
{
    using System;
    using System.Xml.Xsl;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Services.Contracts.IO;

    /// <summary>
    /// XSL transformer from file with specified file name.
    /// </summary>
    public class XslTransformerFromFile : XslTransformerBase, IXslTransformerFromFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XslTransformerFromFile"/> class.
        /// </summary>
        /// <param name="xslFileName">XSL file name.</param>
        /// <param name="cache">Transform cache.</param>
        /// <param name="xmlReadService">XML read service.</param>
        public XslTransformerFromFile(string xslFileName, IXslTransformCache cache, IXmlReadService xmlReadService)
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
