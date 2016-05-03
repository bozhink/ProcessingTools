namespace ProcessingTools.Bio.Biorepositories.Data.Seed.Seeders
{
    using System;
    using System.Collections.Concurrent;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories;
    using ProcessingTools.Data.Common.Seed;
    using ProcessingTools.Extensions;
    using ProcessingTools.Serialization.Csv;

    public class BiorepositoriesDataSeeder : IBiorepositoriesDataSeeder
    {
        private readonly IBiorepositoriesMongoDatabaseProvider contextProvider;
        private readonly string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        public BiorepositoriesDataSeeder(IBiorepositoriesMongoDatabaseProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;

            this.dataFilesDirectoryPath = ConfigurationManager.AppSettings["BiorepositoriesSeedCsvDataFiles"];
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        /// <summary>
        /// Seeds databases with data.
        /// </summary>
        public async Task Seed()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            await this.ImportCsvFileToMongo<Models.Csv.CollectionLabel, ProcessingTools.Bio.Biorepositories.Data.Mongo.Models.CollectionLabel>();
            await this.ImportCsvFileToMongo<Models.Csv.CollectionPer, ProcessingTools.Bio.Biorepositories.Data.Mongo.Models.CollectionPer>();
            await this.ImportCsvFileToMongo<Models.Csv.CollectionPerLabel, ProcessingTools.Bio.Biorepositories.Data.Mongo.Models.CollectionPerLabel>();
            await this.ImportCsvFileToMongo<Models.Csv.Institution, ProcessingTools.Bio.Biorepositories.Data.Mongo.Models.Institution>();
            await this.ImportCsvFileToMongo<Models.Csv.InstitutionLabel, ProcessingTools.Bio.Biorepositories.Data.Mongo.Models.InstitutionLabel>();
            await this.ImportCsvFileToMongo<Models.Csv.Staff, ProcessingTools.Bio.Biorepositories.Data.Mongo.Models.Staff>();
            await this.ImportCsvFileToMongo<Models.Csv.StaffLabel, ProcessingTools.Bio.Biorepositories.Data.Mongo.Models.StaffLabel>();

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        private async Task ImportCsvFileToMongo<TSeedModel, TEntityModel>()
            where TSeedModel : class
            where TEntityModel : class
        {
            try
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
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }
    }
}