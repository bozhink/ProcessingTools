namespace ProcessingTools.Xml.Processors
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Extensions;

    public class XslTransformer : IXslTransformer
    {
        public async Task<string> Transform(XmlReader reader, bool closeReader, IXslTransformProvider xslTransformProvider)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (xslTransformProvider == null)
            {
                throw new ArgumentNullException(nameof(xslTransformProvider));
            }

            string result = string.Empty;

            try
            {
                using (var stream = this.TransformToStream(reader, xslTransformProvider))
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

        public Task<string> Transform(XmlNode node, IXslTransformProvider xslTransformProvider)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (xslTransformProvider == null)
            {
                throw new ArgumentNullException(nameof(xslTransformProvider));
            }

            return this.Transform(node.OuterXml, xslTransformProvider);
        }

        public Task<string> Transform(string xml, IXslTransformProvider xslTransformProvider)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            if (xslTransformProvider == null)
            {
                throw new ArgumentNullException(nameof(xslTransformProvider));
            }

            return this.Transform(xml.ToXmlReader(), true, xslTransformProvider);
        }

        public Stream TransformToStream(XmlReader reader, IXslTransformProvider xslTransformProvider)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (xslTransformProvider == null)
            {
                throw new ArgumentNullException(nameof(xslTransformProvider));
            }

            var stream = new MemoryStream();

            var xslTransform = xslTransformProvider.GetXslTransform();
            xslTransform.Transform(reader, null, stream);
            stream.Position = 0;

            return stream;
        }

        public Stream TransformToStream(XmlNode node, IXslTransformProvider xslTransformProvider)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (xslTransformProvider == null)
            {
                throw new ArgumentNullException(nameof(xslTransformProvider));
            }

            return this.TransformToStream(node.OuterXml, xslTransformProvider);
        }

        public Stream TransformToStream(string xml, IXslTransformProvider xslTransformProvider)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            if (xslTransformProvider == null)
            {
                throw new ArgumentNullException(nameof(xslTransformProvider));
            }

            return this.TransformToStream(xml.ToXmlReader(), xslTransformProvider);
        }
    }
}
