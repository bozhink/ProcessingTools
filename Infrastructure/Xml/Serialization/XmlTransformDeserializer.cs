namespace ProcessingTools.Xml.Serialization
{
    using System;
    using System.Threading.Tasks;

    using Contracts.Serialization;
    using Contracts.Transformers;

    using ProcessingTools.Serialization.Contracts;

    public class XmlTransformDeserializer<TTransformer, TResult> : IXmlTransformDeserializer<TTransformer, TResult>
        where TTransformer : IXmlTransformer
    {
        private readonly TTransformer transformer;
        private readonly IXmlDeserializer<TResult> deserializer;

        public XmlTransformDeserializer(TTransformer transformer, IXmlDeserializer<TResult> deserializer)
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

        public async Task<TResult> Deserialize(string xml)
        {
            var stream = this.transformer.TransformToStream(xml);

            var result = await this.deserializer.Deserialize(stream);

            stream.Close();
            stream.Dispose();

            return result;
        }
    }
}
