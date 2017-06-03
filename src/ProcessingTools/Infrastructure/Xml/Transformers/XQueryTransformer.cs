namespace ProcessingTools.Xml.Transformers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Xml.Contracts.Cache;
    using ProcessingTools.Xml.Contracts.Transformers;

    public class XQueryTransformer : IXQueryTransformer
    {
        private readonly IXQueryTransformCache cache;
        private readonly string xqueryFileName;

        public XQueryTransformer(string xqueryFileName, IXQueryTransformCache cache)
        {
            if (string.IsNullOrWhiteSpace(xqueryFileName))
            {
                throw new ArgumentNullException(nameof(xqueryFileName));
            }

            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            this.xqueryFileName = xqueryFileName;
            this.cache = cache;
        }

        public Task<string> Transform(XmlNode node)
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

        public Task<string> Transform(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            var document = xml.ToXmlDocument();
            return this.Transform(document.DocumentElement);
        }

        public Task<string> Transform(XmlReader reader, bool closeReader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            try
            {
                var document = new XmlDocument
                {
                    PreserveWhitespace = true
                };

                document.Load(reader);

                return this.Transform(document.DocumentElement);
            }
            catch (Exception e)
            {
                throw e;
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
                    }
                }
            }
        }

        public Stream TransformToStream(XmlReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            var document = new XmlDocument
            {
                PreserveWhitespace = true
            };

            document.Load(reader);

            return this.TransformToStream(document.DocumentElement);
        }

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
