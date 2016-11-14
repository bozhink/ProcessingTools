namespace ProcessingTools.Serialization.Serializers
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Threading.Tasks;
    using Contracts;

    public class DataContractJsonDeserializer : IDataContractJsonDeserializer
    {
        public Task<T> Deserialize<T>(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var serializer = new DataContractJsonSerializer(typeof(T));
            var result = (T)serializer.ReadObject(stream);
            return Task.FromResult(result);
        }
    }
}
