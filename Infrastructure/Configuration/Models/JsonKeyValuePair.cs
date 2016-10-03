namespace ProcessingTools.Configuration.Models
{
    using Newtonsoft.Json;

    [JsonObject]
    public class JsonKeyValuePair : IJsonKeyValuePair
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
