namespace ProcessingTools.Serialization.Serializers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using ProcessingTools.Contracts.Serialization;

    public class XmlDeserializer<T> : IXmlDeserializer<T>
    {
        private readonly XmlSerializer serializer;

        public XmlDeserializer()
        {
            this.serializer = new XmlSerializer(typeof(T));
        }

        public Task<T> DeserializeAsync(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return Task.Run(() =>
            {
                stream.Position = 0;

                var result = this.serializer.Deserialize(stream);
                return (T)result;
            });
        }
    }
}
