namespace ProcessingTools.Bio.Biorepositories.Data.Seed.Seeders
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Biorepositories.Data.Contracts;
    using ProcessingTools.Data.Common.Seed;
    using ProcessingTools.Extensions;
    using ProcessingTools.Serialization.Csv;

    using Repositories;
    public class BiorepositoriesDataSeeder : IBiorepositoriesDataSeeder
    {
        private readonly IBiorepositoriesMongoDatabaseProvider contextProvider;
        private readonly string dataFilesDirectoryPath;

        public BiorepositoriesDataSeeder(IBiorepositoriesMongoDatabaseProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;

            this.dataFilesDirectoryPath = ConfigurationManager.AppSettings["BiorepositoriesSeedCsvDataFiles"];
        }

        public Task Init()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Seeds databases with data.
        /// </summary>
        public async Task Seed()
        {
            await this.ImportCsvFileToMongo<Models.Seed.Csv.Collection, Models.Bson.Collection>();
            await this.ImportCsvFileToMongo<Models.Seed.Csv.CollectionLabel, Models.Bson.CollectionLabel>();
            await this.ImportCsvFileToMongo<Models.Seed.Csv.CollectionPer, Models.Bson.CollectionPer>();
            await this.ImportCsvFileToMongo<Models.Seed.Csv.CollectionPerLabel, Models.Bson.CollectionPerLabel>();
            await this.ImportCsvFileToMongo<Models.Seed.Csv.Institution, Models.Bson.Institution>();
            await this.ImportCsvFileToMongo<Models.Seed.Csv.InstitutionLabel, Models.Bson.InstitutionLabel>();
            await this.ImportCsvFileToMongo<Models.Seed.Csv.Staff, Models.Bson.Staff>();
            await this.ImportCsvFileToMongo<Models.Seed.Csv.StaffLabel, Models.Bson.StaffLabel>();
        }

        private async Task ImportCsvFileToMongo<TSeedModel, TEntityModel>()
            where TSeedModel : class
            where TEntityModel : class
        {
            Type seedModelType = typeof(TSeedModel);
            var fileNameAttribute = seedModelType.GetCustomAttributes(typeof(FileNameAttribute), false)?.FirstOrDefault() as FileNameAttribute;
            if (fileNameAttribute == null)
            {
                throw new ApplicationException($"Invalid seed model {seedModelType.Name}: There is no FileNameAttribute.");
            }

            string fileName = string.Format("{0}/{1}", this.dataFilesDirectoryPath, fileNameAttribute.Name);
            if (!File.Exists(fileName))
            {
                throw new ApplicationException($"File {fileName} does not exist.");
            }

            string csvText = File.ReadAllText(fileName);
            var serializer = new CsvSerializer();
            var items = serializer.Deserialize<TSeedModel>(csvText)?.Select(i => i.Map<TEntityModel>())
                .ToArray();
            if (items == null || items.Length < 1)
            {
                throw new ApplicationException("Deserialized items are not valid.");
            }

            var repositoryProvider = new BiorepositoriesMongoRepositoryProvider<TEntityModel>(this.contextProvider);
            var seeder = new SimpleRepositorySeeder<TEntityModel>(repositoryProvider);
            await seeder.Seed(items);
        }
    }
}