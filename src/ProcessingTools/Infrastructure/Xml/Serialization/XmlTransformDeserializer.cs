namespace ProcessingTools.Xml.Serialization
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts.Serialization;
    using ProcessingTools.Contracts;
    using ProcessingTools.Serialization.Contracts;

    public class XmlTransformDeserializer : IXmlTransformDeserializer
    {
        private readonly IXmlDeserializer deserializer;

        public XmlTransformDeserializer(IXmlDeserializer deserializer)
        {
            if (deserializer == null)
            {
                throw new ArgumentNullException(nameof(deserializer));
            }

            this.deserializer = deserializer;
        }

        public async Task<T> Deserialize<T>(IXmlTransformer transformer, string xml)
        {
            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            var stream = transformer.TransformToStream(xml);

            var result = await this.deserializer.Deserialize<T>(stream);

            stream.Close();
            stream.Dispose();

            return result;
        }

        public async Task<T> Deserialize<T>(IXmlTransformer transformer, XmlNode node)
        {
            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var stream = transformer.TransformToStream(node);

            var result = await this.deserializer.Deserialize<T>(stream);

            stream.Close();
            stream.Dispose();

            return result;
        }
    }
}
