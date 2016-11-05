namespace ProcessingTools.DataResources.Data.Seed
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.Threading.Tasks;
    using Contracts;
    using ProcessingTools.Data.Common.Entity.Seed;
    using ProcessingTools.DataResources.Data.Entity;
    using ProcessingTools.DataResources.Data.Entity.Contracts;
    using ProcessingTools.DataResources.Data.Entity.Models;

    public class DataResourcesDataSeeder : IDataResourcesDataSeeder
    {
        private const string DataFilesDirectoryPathKey = "DataFilesDirectoryPath";
        private const string ProductsSeedFileNameKey = "ProductsSeedFileName";
        private const string InstitutionsSeedFileNameKey = "InstitutionsSeedFileName";

        private readonly IDataResourcesDbContextProvider contextProvider;
        private readonly Type stringType = typeof(string);

        private DbContextSeeder<DataResourcesDbContext> seeder;
        private string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        public DataResourcesDataSeeder(IDataResourcesDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
            this.seeder = new DbContextSeeder<DataResourcesDbContext>(this.contextProvider);

            this.dataFilesDirectoryPath = ConfigurationManager.AppSettings[DataFilesDirectoryPathKey];
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        public async Task<object> Seed()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            var tasks = new List<Task>();

            tasks.Add(
                this.SeedProducts(
                    ConfigurationManager.AppSettings[ProductsSeedFileNameKey]));
            tasks.Add(
                this.SeedInstitutions(
                    ConfigurationManager.AppSettings[InstitutionsSeedFileNameKey]));

            await Task.WhenAll(tasks.ToArray());

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }

            return true;
        }

        private async Task SeedProducts(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            try
            {
                await this.seeder.ImportSingleLineTextObjectsFromFile(
                    $"{dataFilesDirectoryPath}/{fileName}",
                    (context, line) =>
                    {
                        context.Products.AddOrUpdate(new Product
                        {
                            Name = line
                        });
                    });
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task SeedInstitutions(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            try
            {
                await this.seeder.ImportSingleLineTextObjectsFromFile(
                    $"{dataFilesDirectoryPath}/{fileName}",
                    (context, line) =>
                    {
                        context.Institutions.AddOrUpdate(new Institution
                        {
                            Name = line
                        });
                    });
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }
    }
}
