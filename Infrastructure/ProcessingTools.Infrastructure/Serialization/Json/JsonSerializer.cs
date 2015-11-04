namespace ProcessingTools.Infrastructure.Serialization.Json
{
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    public class JsonSerializer
    {
        public static T Serialize<T>(string jsonString)
        {
            DataContractJsonSerializer data = new DataContractJsonSerializer(typeof(T));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            return (T)data.ReadObject(stream);
        }
    }
}
