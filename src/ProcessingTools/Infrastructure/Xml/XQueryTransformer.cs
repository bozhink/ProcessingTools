using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using ProcessingTools.Extensions;
using ProcessingTools.Services.Contracts.Xml;

namespace ProcessingTools.Xml
{
    /// <summary>
    /// XQuery transformer.
    /// </summary>
    public class XQueryTransformer : IXQueryTransformer
    {
        private readonly IXQueryTransformCache cache;
        private readonly string xqueryFileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="XQueryTransformer"/> class.
        /// </summary>
        /// <param name="xqueryFileName">XQuery file name.</param>
        /// <param name="cache">XQuery transform cache.</param>
        public XQueryTransformer(string xqueryFileName, IXQueryTransformCache cache)
        {
            if (string.IsNullOrWhiteSpace(xqueryFileName))
            {
                throw new ArgumentNullException(nameof(xqueryFileName));
            }

            this.xqueryFileName = xqueryFileName;
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        /// <inheritdoc/>
        public Task<string> TransformAsync(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return Task.Run(() =>
            {
                return this.TransformNode(node)?.OuterXml;
            });
        }

        /// <inheritdoc/>
        public Task<string> TransformAsync(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            var document = xml.ToXmlDocument();
            return this.TransformAsync(document.DocumentElement);
        }

        /// <inheritdoc/>
        public Task<string> TransformAsync(XmlReader reader, bool closeReader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            try
            {
                var document = new XmlDocument
                {
                    PreserveWhitespace = true,
                };

                document.Load(reader);

                return this.TransformAsync(document.DocumentElement);
            }
            finally
            {
                if (closeReader && reader?.ReadState != ReadState.Closed)
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
        }

        /// <inheritdoc/>
        public Stream TransformToStream(XmlReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            var document = new XmlDocument
            {
                PreserveWhitespace = true,
            };

            document.Load(reader);

            return this.TransformToStream(document.DocumentElement);
        }

        /// <inheritdoc/>
        public Stream TransformToStream(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var stream = new MemoryStream();
            var writer = XmlWriter.Create(stream);
            this.TransformNode(node)?.WriteTo(writer);
            writer.Flush();

            return stream;
        }

        /// <inheritdoc/>
        public Stream TransformToStream(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            var document = xml.ToXmlDocument();
            return this.TransformToStream(document.DocumentElement);
        }

        private XmlDocument TransformNode(XmlNode node)
        {
            var transform = this.cache[this.xqueryFileName];
            var document = transform.Evaluate(node);
            return document;
        }
    }
}
