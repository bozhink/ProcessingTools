namespace ProcessingTools.Infrastructure.Serialization.Json
{
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    public class JsonSerializer
    {
        public static T Deserialize<T>(string jsonString)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            var serializer = new DataContractJsonSerializer(typeof(T));
            return (T)serializer.ReadObject(stream);
        }
    }
}
