namespace ProcessingTools.NlmArchiveConsoleManager.Settings
{
    using System.Configuration;
    using Contracts.Settings;

    public class ApplicationSettings : IApplicationSettings
    {
        public string JournalJsonFileName
        {
            get
            {
                var appSettingsReader = new AppSettingsReader();
                return appSettingsReader.GetValue("JournalConfigJsonFile", typeof(string)).ToString();
            }
        }
    }
}