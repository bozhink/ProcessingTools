namespace ProcessingTools.Xml.Serialization
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts.Serialization;
    using ProcessingTools.Contracts;
    using ProcessingTools.Serialization.Contracts;

    public class XmlTransformDeserializer<TTransformer> : IXmlTransformDeserializer<TTransformer>
        where TTransformer : IXmlTransformer
    {
        private readonly TTransformer transformer;
        private readonly IXmlDeserializer deserializer;

        public XmlTransformDeserializer(TTransformer transformer, IXmlDeserializer deserializer)
        {
            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            if (deserializer == null)
            {
                throw new ArgumentNullException(nameof(deserializer));
            }

            this.transformer = transformer;
            this.deserializer = deserializer;
        }

        public async Task<T> Deserialize<T>(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            var stream = this.transformer.TransformToStream(xml);

            var result = await this.deserializer.Deserialize<T>(stream);

            stream.Close();
            stream.Dispose();

            return result;
        }

        public async Task<T> Deserialize<T>(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var stream = this.transformer.TransformToStream(node);

            var result = await this.deserializer.Deserialize<T>(stream);

            stream.Close();
            stream.Dispose();

            return result;
        }
    }
}
