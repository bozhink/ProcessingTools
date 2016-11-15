namespace ProcessingTools.Serialization.Serializers
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Threading.Tasks;
    using Contracts;

    public class GenericDataContractJsonDeserializer<T> : IGenericDataContractJsonDeserializer<T>
    {
        private readonly DataContractJsonSerializer serializer;

        public GenericDataContractJsonDeserializer()
        {
            this.serializer = new DataContractJsonSerializer(typeof(T));
        }

        public Task<T> Deserialize(Stream stream)
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
