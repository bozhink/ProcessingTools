namespace ProcessingTools.Bio.Biorepositories.Data.Seed.Tests
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ProcessingTools.Attributes;
    using ProcessingTools.Serialization.Csv;

    [TestClass]
    public class CsvSeedModelTests
    {
        [TestMethod]
        public void CsvSeedFiles_Deserialization_ShouldWork()
        {
            var appSettingsReader = new AppSettingsReader();
            var dataFilesDirectoryPath = appSettingsReader.GetValue("SeedCsvDataFiles", typeof(string)).ToString();

            var modelTypes = ProcessingTools.Bio.Biorepositories.Data.Seed.Assembly.Assembly
                .GetType()
                .Assembly
                .GetTypes()
                .Where(t => !t.IsAbstract && t.IsClass && Attribute.IsDefined(t, typeof(FileNameAttribute)) && Attribute.IsDefined(t, typeof(CsvObjectAttribute)));

            foreach (var modelType in modelTypes)
            {
                Console.WriteLine(modelType.FullName);

                var fileNameAttribute = modelType.GetCustomAttributes(typeof(FileNameAttribute), false).FirstOrDefault() as FileNameAttribute;

                string fileName = string.Format("{0}/{1}", dataFilesDirectoryPath, fileNameAttribute.Name);
                Assert.IsTrue(File.Exists(fileName), $"FileName ‘{fileName}’ should be valid.");

                string csvText = File.ReadAllText(fileName);
                Assert.IsFalse(string.IsNullOrWhiteSpace(csvText), $"Text of ‘{fileName}’ should not be null or whitespace.");

                var serializer = new CsvSerializer();
                var items = serializer.Deserialize(modelType, csvText)?.ToList();

                Assert.IsNotNull(items, "Deserialized items should not be null.");
                Assert.IsTrue(items.Count > 0, "Number of deserialized items should be greater than 0.");
            }
        }
    }
}