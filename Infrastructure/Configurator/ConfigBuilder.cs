namespace ProcessingTools.Configurator
{
    using System.Configuration;
    using System.IO;

    using Infrastructure.Serialization.Json;

    public static class ConfigBuilder
    {
        /// <summary>
        /// Reads the default config JSON file and builds a Config object from it.
        /// </summary>
        /// <returns>Initialized Config object.</returns>
        public static Config Create()
        {
            string configJsonFilePath = ConfigurationManager.AppSettings["ConfigJsonFilePath"];

            return Create(configJsonFilePath);
        }

        /// <summary>
        /// Reads a config JSON file and builds a Config object from it.
        /// </summary>
        /// <param name="configFilePath">Full path of the config file.</param>
        /// <returns>Initialized Config object.</returns>
        public static Config Create(string configFilePath)
        {
            string jsonText = File.ReadAllText(configFilePath);

            return JsonSerializer.Deserialize<Config>(jsonText);
        }
    }
}
