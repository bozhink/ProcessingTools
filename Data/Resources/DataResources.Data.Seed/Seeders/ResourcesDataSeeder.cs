namespace ProcessingTools.DataResources.Data.Seed.Seeders
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Common.Entity.Seed;
    using ProcessingTools.DataResources.Data.Entity;
    using ProcessingTools.DataResources.Data.Entity.Contracts;
    using ProcessingTools.DataResources.Data.Entity.Models;
    using ProcessingTools.DataResources.Data.Seed.Contracts;

    public class ResourcesDataSeeder : IResourcesDataSeeder
    {
        private const string DataFilesDirectoryPathKey = "DataFilesDirectoryPath";
        private const string ProductsSeedFileNameKey = "ProductsSeedFileName";
        private const string InstitutionsSeedFileNameKey = "InstitutionsSeedFileName";

        private readonly IResourcesDbContextFactory contextFactory;
        private readonly Type stringType = typeof(string);

        private FileByLineDbContextSeeder<ResourcesDbContext> seeder;
        private string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        public ResourcesDataSeeder(IResourcesDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.contextFactory = contextFactory;
            this.seeder = new FileByLineDbContextSeeder<ResourcesDbContext>(this.contextFactory);

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
