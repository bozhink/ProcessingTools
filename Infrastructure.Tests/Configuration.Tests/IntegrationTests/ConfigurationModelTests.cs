namespace ProcessingTools.Configuration.Tests.IntegrationTests
{
    using System;
    using System.IO;
    using System.Reflection;

    using Newtonsoft.Json;
    using NUnit.Framework;

    using ProcessingTools.Configuration.Models;

    [TestFixture(Author = "Bozhin Karaivanov", Category = "Integration", TestOf = typeof(ConfigurationModel))]
    public class ConfigurationModelTests
    {
        private string GetDirectoryOfTestAssembly
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        [Test(Description = "ConfigurationModel with default constructor should create valid object.", Author = "Bozhin Karaivanov", TestOf = typeof(ConfigurationModel))]
        public void ConfigurationModel_WithDefaultConstructor_ShouldCreateValidObject()
        {
            var model = new ConfigurationModel();
            Assert.IsNotNull(model, "Created object should not be null.");
        }

        [Test(Description = "ConfigurationModel: deserialization of a valid configuration JSON file should work.", Author = "Bozhin Karaivanov", TestOf = typeof(ConfigurationModel))]
        public void ConfigurationModel_DeserializationOfValidConfigurationJsonFile_ShouldWork()
        {
            var fileName = "configuration.json";
            var directory = this.GetDirectoryOfTestAssembly;
            var filePath = Path.Combine(directory, Path.Combine("Files", fileName));

            Assert.IsTrue(File.Exists(filePath), "The configuration JSON file should be present.");

            var json = File.ReadAllText(filePath);
            Assert.IsFalse(string.IsNullOrWhiteSpace(json), "The configuration JSON file should not be empty.");

            var model = JsonConvert.DeserializeObject<ConfigurationModel>(json);
            Assert.IsNotNull(model, "De-serialized model object should not be null.");

            const int NumberOfFileEntries = 2;
            const int NumberOfSettingEntries = 1;

            Assert.AreEqual(NumberOfFileEntries, model.Files.Length, $"Number of file entries should be {NumberOfFileEntries}");
            Assert.AreEqual(NumberOfSettingEntries, model.Settings.Length, $"Number of setting entries should be {NumberOfSettingEntries}");

            Assert.AreEqual("NlmInitialFormatXslPath", model.Files[0].Key, "Invalid Key-value in the first file object.");
            Assert.AreEqual("Xsl/format.nlm.initial.xsl", model.Files[0].Value, "Invalid Value-value in the first file object.");

            Assert.AreEqual("SystemInitialFormatXslPath", model.Files[1].Key, "Invalid Key-value in the second file object.");
            Assert.AreEqual("Xsl/format.system.initial.xsl", model.Files[1].Value, "Invalid Value-value in the second file object.");

            Assert.AreEqual("RedisServerAddress", model.Settings[0].Key, "Invalid Key-value in the setting object.");
            Assert.AreEqual("redis://localhost:6379", model.Settings[0].Value, "Invalid Value-value in the setting object.");
        }
    }
}
