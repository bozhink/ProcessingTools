namespace ProcessingTools.Data.Seed.Files.Models
{
    using Newtonsoft.Json;

    public class ExtensionJson
    {
        [JsonProperty("extension")]
        public string Extension { get; set; }

        [JsonProperty("mimetype")]
        public string Mimetype { get; set; }

        [JsonProperty("mimesubtype")]
        public string Mimesubtype { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}