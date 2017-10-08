namespace ProcessingTools.Xml.Transformers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Xsl;
    using ProcessingTools.Extensions;
    using ProcessingTools.Xml.Contracts.Cache;
    using ProcessingTools.Xml.Contracts.Transformers;

    public class XslTransformer : IXslTransformer
    {
        private readonly XslCompiledTransform xslCompiledTransform;

        public XslTransformer(string xslFileName, IXslTransformCache cache)
        {
            if (string.IsNullOrWhiteSpace(xslFileName))
            {
                throw new ArgumentNullException(nameof(xslFileName));
            }

            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            this.xslCompiledTransform = cache[xslFileName];
        }

        public async Task<string> TransformAsync(XmlReader reader, bool closeReader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

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

            return result;
        }

        public Task<string> TransformAsync(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return this.TransformAsync(node.OuterXml);
        }

        public Task<string> TransformAsync(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            return this.TransformAsync(xml.ToXmlReader(), true);
        }

        public Stream TransformToStream(XmlReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            var stream = new MemoryStream();
            this.xslCompiledTransform.Transform(reader, null, stream);
            stream.Position = 0;

            return stream;
        }

        public Stream TransformToStream(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return this.TransformToStream(node.OuterXml);
        }

        public Stream TransformToStream(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            return this.TransformToStream(xml.ToXmlReader());
        }
    }
}
