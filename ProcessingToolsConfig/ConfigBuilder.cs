namespace ProcessingTools.Configurator
{
    using System.IO;
    using Globals.Json;

    public static class ConfigBuilder
    {
        /// <summary>
        /// This method reads a config Xml file and builds a config object from it.
        /// </summary>
        /// <param name="configFilePath">full path of the config fule</param>
        /// <returns>initialized config object</returns>
        public static Config CreateConfig(string configFilePath)
        {
            try
            {
                string jsonText = File.ReadAllText(configFilePath);

                return JsonSerializer.Serialize<Config>(jsonText);
            }
            catch
            {
                throw;
            }
        }
    }
}
