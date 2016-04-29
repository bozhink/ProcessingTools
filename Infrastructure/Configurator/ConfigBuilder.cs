namespace ProcessingTools.Configurator
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Runtime.Serialization.Json;

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
            if (string.IsNullOrWhiteSpace(configFilePath))
            {
                throw new ArgumentNullException(nameof(configFilePath));
            }

            using (var stream = new FileStream(configFilePath, FileMode.Open, FileAccess.Read))
            {
                var serializer = new DataContractJsonSerializer(typeof(Config));
                return (Config)serializer.ReadObject(stream);
            }
        }
    }
}