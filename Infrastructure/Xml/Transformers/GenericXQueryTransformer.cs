namespace ProcessingTools.Xml.Transformers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts.Providers;
    using Contracts.Transformers;
    using Extensions;

    public class XQueryTransformer<T> : IXQueryTransformer<T>
        where T : IXQueryTransformProvider
    {
        private readonly T xqueryTransformProvider;

        public XQueryTransformer(T xqueryTransformProvider)
        {
            if (xqueryTransformProvider == null)
            {
                throw new ArgumentNullException(nameof(xqueryTransformProvider));
            }

            this.xqueryTransformProvider = xqueryTransformProvider;
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
            var transform = this.xqueryTransformProvider.GetXQueryTransform();
            var document = transform.Evaluate(node);
            return document;
        }
    }
}
