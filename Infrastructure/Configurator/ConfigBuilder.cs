namespace ProcessingTools.Configurator
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Runtime.Serialization.Json;

    public static class ConfigBuilder
    {
        /// <summary>
        /// Reads the default config JSON file and builds a IConfig object from it.
        /// </summary>
        /// <returns>Initialized IConfig object.</returns>
        public static IConfig Create()
        {
            string configJsonFilePath = ConfigurationManager.AppSettings["ConfigJsonFilePath"];

            return Create(configJsonFilePath);
        }

        /// <summary>
        /// Reads a config JSON file and builds a IConfig object from it.
        /// </summary>
        /// <param name="configFilePath">Full path of the config file.</param>
        /// <returns>Initialized IConfig object.</returns>
        public static IConfig Create(string configFilePath)
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