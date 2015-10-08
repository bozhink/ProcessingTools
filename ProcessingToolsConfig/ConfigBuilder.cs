namespace ProcessingTools
{
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    public static class ConfigBuilder
    {
        /// <summary>
        /// This method reads a config Xml file and builds a config object from it.
        /// </summary>
        /// <param name="configFilePath">full path of the config fule</param>
        /// <returns>initialized config object</returns>
        public static Config CreateConfig(string configFilePath)
        {
            Config config = null;
            try
            {
                string jsonText = File.ReadAllText(configFilePath);
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonText));
                DataContractJsonSerializer data = new DataContractJsonSerializer(typeof(Config));
                config = (Config)data.ReadObject(stream);
            }
            catch
            {
                throw;
            }

            return config;
        }
    }
}
