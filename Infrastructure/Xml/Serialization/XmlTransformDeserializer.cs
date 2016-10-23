namespace ProcessingTools.Xml.Serialization
{
    using System;
    using System.Threading.Tasks;

    using Contracts.Serialization;
    using Contracts.Transformers;

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
            var stream = this.transformer.TransformToStream(xml);

            var result = await this.deserializer.Deserialize<T>(stream);

            stream.Close();
            stream.Dispose();

            return result;
        }
    }
}
