namespace ProcessingTools.Bio.Biorepositories.Data.Seed
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;

    using ProcessingTools.Infrastructure.Attributes;
    using ProcessingTools.Infrastructure.Serialization.Csv;

    public class Configuration
    {
        public Configuration()
        {
        }

        /// <summary>
        /// Seeds databases with data.
        /// </summary>
        public void Seed()
        {
            string dataFilesDirectoryPath = ConfigurationManager.AppSettings["BiorepositoriesSeedCsvDataFiles"];

            var seedModelTypes = ProcessingTools.Bio.Biorepositories.Data.Models.Assembly.Assembly
                .GetType()
                .Assembly
                .GetTypes()
                .Where(t => !t.IsAbstract && t.IsClass && Attribute.IsDefined(t, typeof(FileNameAttribute)) && Attribute.IsDefined(t, typeof(CsvObjectAttribute)));

            foreach (var seedModelType in seedModelTypes)
            {
                Console.WriteLine(seedModelType.FullName);

                var fileNameAttribute = seedModelType.GetCustomAttributes(typeof(FileNameAttribute), false).FirstOrDefault() as FileNameAttribute;

                string fileName = string.Format("{0}/{1}", dataFilesDirectoryPath, fileNameAttribute.Name);
                if (!File.Exists(fileName))
                {
                    throw new ApplicationException($"File {fileName} does not exist.");
                }

                string csvText = File.ReadAllText(fileName);
                var serializer = new CsvSerializer();
                var items = serializer.Deserialize(seedModelType, csvText)?.ToList();
                if (items == null || items.Count < 1)
                {
                    throw new ApplicationException("Deserialized items are not valid.");
                }

                ////
            }
        }
    }
}