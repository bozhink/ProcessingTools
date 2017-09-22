namespace ProcessingTools.Serialization.Serializers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using ProcessingTools.Contracts.Serialization;

    public class XmlDeserializer : IXmlDeserializer
    {
        public Task<T> DeserializeAsync<T>(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var serializer = new XmlSerializer(typeof(T));

            return Task.Run(() =>
            {
                stream.Position = 0;

                var result = serializer.Deserialize(stream);
                return (T)result;
            });
        }
    }
}
