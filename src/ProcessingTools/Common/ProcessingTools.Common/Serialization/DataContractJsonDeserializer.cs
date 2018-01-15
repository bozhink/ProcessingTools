namespace ProcessingTools.Common.Serialization
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Serialization;

    public class DataContractJsonDeserializer : IDataContractJsonDeserializer
    {
        public Task<T> DeserializeAsync<T>(Stream stream)
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
