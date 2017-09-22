namespace ProcessingTools.Serialization.Serializers
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Serialization;

    public class DataContractJsonDeserializer<T> : IDataContractJsonDeserializer<T>
    {
        private readonly DataContractJsonSerializer serializer;

        public DataContractJsonDeserializer()
        {
            this.serializer = new DataContractJsonSerializer(typeof(T));
        }

        public Task<T> DeserializeAsync(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var result = (T)this.serializer.ReadObject(stream);
            return Task.FromResult(result);
        }
    }
}
