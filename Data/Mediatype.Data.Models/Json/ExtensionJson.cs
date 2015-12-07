namespace ProcessingTools.MediaType.Data.Models.Json
{
    using Newtonsoft.Json;

    public class ExtensionJson
    {
        [JsonProperty("extension")]
        public string Extension { get; set; }

        [JsonProperty("mimeType")]
        public string MimeType { get; set; }

        [JsonProperty("mimeSubtype")]
        public string MimeSubtype { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}