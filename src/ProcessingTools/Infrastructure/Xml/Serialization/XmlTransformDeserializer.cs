namespace ProcessingTools.Xml.Serialization
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Serialization;
    using ProcessingTools.Contracts.Processors;
    using ProcessingTools.Xml.Contracts.Serialization;

    public class XmlTransformDeserializer : IXmlTransformDeserializer
    {
        private readonly IXmlDeserializer deserializer;

        public XmlTransformDeserializer(IXmlDeserializer deserializer)
        {
            this.deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
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

            var result = await this.deserializer.DeserializeAsync<T>(stream).ConfigureAwait(false);

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

            var result = await this.deserializer.DeserializeAsync<T>(stream).ConfigureAwait(false);

            stream.Close();
            stream.Dispose();

            return result;
        }
    }
}
