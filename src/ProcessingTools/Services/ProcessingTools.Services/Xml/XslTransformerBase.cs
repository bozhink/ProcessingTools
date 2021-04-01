﻿// <copyright file="XslTransformerBase.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Xml
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Xsl;
    using ProcessingTools.Contracts.Services.IO;
    using ProcessingTools.Contracts.Services.Xml;

    /// <summary>
    /// XSL transformer base class.
    /// </summary>
    public abstract class XslTransformerBase : IXslTransformer
    {
        private readonly IXmlReadService xmlReadService;

        /// <summary>
        /// Initializes a new instance of the <see cref="XslTransformerBase"/> class.
        /// </summary>
        /// <param name="xmlReadService">XML read service.</param>
        protected XslTransformerBase(IXmlReadService xmlReadService)
        {
            this.xmlReadService = xmlReadService ?? throw new ArgumentNullException(nameof(xmlReadService));
        }

        /// <summary>
        /// Gets the <see cref="XslCompiledTransform"/> object.
        /// </summary>
        protected abstract XslCompiledTransform XslCompiledTransform { get; }

        /// <inheritdoc/>
        public Task<string> TransformAsync(XmlReader reader, bool closeReader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            return this.TransformInternalAsync(reader, closeReader);
        }

        /// <inheritdoc/>
        public Task<string> TransformAsync(XmlNode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return this.TransformAsync(node.OuterXml);
        }

        /// <inheritdoc/>
        public Task<string> TransformAsync(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            var reader = this.xmlReadService.GetXmlReaderForXmlString(xml);
            return this.TransformAsync(reader, true);
        }

        /// <inheritdoc/>
        public Stream TransformToStream(XmlReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            var stream = new MemoryStream();
            this.XslCompiledTransform.Transform(reader, null, stream);
            stream.Position = 0;

            return stream;
        }

        /// <inheritdoc/>
        public Stream TransformToStream(XmlNode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return this.TransformToStream(node.OuterXml);
        }

        /// <inheritdoc/>
        public Stream TransformToStream(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            var reader = this.xmlReadService.GetXmlReaderForXmlString(xml);
            return this.TransformToStream(reader);
        }

        private async Task<string> TransformInternalAsync(XmlReader reader, bool closeReader)
        {
            string result = string.Empty;

            try
            {
                using (var stream = this.TransformToStream(reader))
                {
                    stream.Position = 0;
                    var streamReader = new StreamReader(stream);
                    result = await streamReader.ReadToEndAsync().ConfigureAwait(false);
                    stream.Close();
                }
            }
            finally
            {
                if (closeReader && reader != null && reader.ReadState != ReadState.Closed)
                {
                    try
                    {
                        reader.Close();
                        reader.Dispose();
                    }
                    catch
                    {
                        // Skip
                    }
                }
            }

            return result;
        }
    }
}
