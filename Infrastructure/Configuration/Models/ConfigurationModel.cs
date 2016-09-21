namespace ProcessingTools.Configuration.Models
{
    using Newtonsoft.Json;

    [JsonObject]
    public class ConfigurationModel
    {
        [JsonProperty("files")]
        public JsonKeyValuePair[] Files { get; set; }

        [JsonProperty("settings")]
        public JsonKeyValuePair[] Settings { get; set; }
    }
}
