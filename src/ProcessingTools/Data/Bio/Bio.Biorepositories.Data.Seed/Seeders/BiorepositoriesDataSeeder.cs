namespace ProcessingTools.Bio.Biorepositories.Data.Seed.Seeders
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Models;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories;
    using ProcessingTools.Bio.Biorepositories.Data.Seed.Contracts;
    using ProcessingTools.Bio.Biorepositories.Data.Seed.Models.Csv;
    using ProcessingTools.Common.Code.Data.Seed;
    using ProcessingTools.Common.Code.Serialization.Csv;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Extensions;

    public class BiorepositoriesDataSeeder : IBiorepositoriesDataSeeder
    {
        private readonly IMongoDatabaseProvider contextProvider;
        private readonly string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        public BiorepositoriesDataSeeder(IMongoDatabaseProvider contextProvider)
        {
            this.contextProvider = contextProvider ?? throw new ArgumentNullException(nameof(contextProvider));

            this.dataFilesDirectoryPath = AppSettings.BiorepositoriesSeedCsvDataFilesDirectoryName;
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        /// <summary>
        /// Seeds databases with data.
        /// </summary>
        /// <returns>Object to be awaited</returns>
        public async Task<object> SeedAsync()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            var tasks = new List<Task>
            {
                this.ImportCsvFileToMongo<CollectionCsv, Collection>(),
                this.ImportCsvFileToMongo<CollectionLabelCsv, CollectionLabel>(),
                this.ImportCsvFileToMongo<CollectionPerCsv, CollectionPer>(),
                this.ImportCsvFileToMongo<CollectionPerLabelCsv, CollectionPerLabel>(),
                this.ImportCsvFileToMongo<InstitutionCsv, Institution>(),
                this.ImportCsvFileToMongo<InstitutionLabelCsv, InstitutionLabel>(),
                this.ImportCsvFileToMongo<StaffCsv, Staff>(),
                this.ImportCsvFileToMongo<StaffLabelCsv, StaffLabel>()
            };
            await Task.WhenAll(tasks.ToArray()).ConfigureAwait(false);

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }

            return true;
        }

        private async Task ImportCsvFileToMongo<TSeedModel, TEntityModel>()
            where TSeedModel : class
            where TEntityModel : class, new()
        {
            try
            {
                Type seedModelType = typeof(TSeedModel);
                var fileNameAttribute = seedModelType.GetCustomAttributes(typeof(FileNameAttribute), false)?.FirstOrDefault() as FileNameAttribute;
                if (fileNameAttribute == null)
                {
                    throw new ProcessingTools.Common.Exceptions.InvalidModelException($"Invalid seed model {seedModelType.Name}: There is no FileNameAttribute.");
                }

                string fileName = string.Format("{0}/{1}", this.dataFilesDirectoryPath, fileNameAttribute.Name);
                if (!File.Exists(fileName))
                {
                    throw new FileNotFoundException($"File {fileName} does not exist.");
                }

                string csvText = File.ReadAllText(fileName);
                var serializer = new CsvSerializer();
                var items = serializer.Deserialize<TSeedModel>(csvText)?.Select(i => i.Map<TEntityModel>())
                    .ToArray();
                if (items == null || items.Length < 1)
                {
                    throw new ProcessingTools.Common.Exceptions.InvalidDataException("De-serialized items are not valid.");
                }

                var repositoryProvider = new BiorepositoriesRepositoryProvider<TEntityModel>(this.contextProvider);
                var seeder = new SimpleRepositorySeeder<TEntityModel>(repositoryProvider);
                await seeder.SeedAsync(items).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }
    }
}
