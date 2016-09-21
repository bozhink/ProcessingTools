namespace ProcessingTools.Configuration.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [JsonObject]
    public class ConfigurationModel : IConfigurationModel
    {
        [JsonProperty("files")]
        public JsonKeyValuePair[] Files { get; set; }

        [JsonProperty("settings")]
        public JsonKeyValuePair[] Settings { get; set; }

        [JsonIgnore]
        IEnumerable<IJsonKeyValuePair> IConfigurationModel.Files => this.Files;

        [JsonIgnore]
        IEnumerable<IJsonKeyValuePair> IConfigurationModel.Settings => this.Settings;
    }
}
