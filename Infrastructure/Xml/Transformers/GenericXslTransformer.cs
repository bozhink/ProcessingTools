namespace ProcessingTools.Xml.Transformers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Extensions;

    public class XslTransformer<T> : IXslTransformer<T>
        where T : IXslTransformProvider
    {
        private readonly T xslTransformProvider;

        public XslTransformer(T xslTransformProvider)
        {
            if (xslTransformProvider == null)
            {
                throw new ArgumentNullException(nameof(xslTransformProvider));
            }

            this.xslTransformProvider = xslTransformProvider;
        }

        protected T XslTransformProvider => this.xslTransformProvider;

        public async Task<string> Transform(XmlReader reader, bool closeReader)
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
                    result = await streamReader.ReadToEndAsync();
                    stream.Close();
                }
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

            return result;
        }

        public Task<string> Transform(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return this.Transform(node.OuterXml);
        }

        public Task<string> Transform(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            return this.Transform(xml.ToXmlReader(), true);
        }

        public Stream TransformToStream(XmlReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            var stream = new MemoryStream();

            var xslTransform = this.xslTransformProvider.GetXslTransform();
            xslTransform.Transform(reader, null, stream);
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
