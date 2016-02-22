namespace ProcessingTools.Data.Common.Redis.Extensions
{
    using ServiceStack.Text;

    public static class SerializationExtensions
    {
        public static string Serialize<T>(this T item)
        {
            return new JsonStringSerializer().SerializeToString(item);
        }

        public static T Deserialize<T>(this string json)
        {
            return new JsonStringSerializer().DeserializeFromString<T>(json);
        }
    }
}
