namespace ProcessingTools.DataResources.Data.Seed.Seeders
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Data.Entity.Abstractions;
    using ProcessingTools.Data.Entity.DataResources;
    using ProcessingTools.Data.Models.Entity.DataResources;
    using ProcessingTools.DataResources.Data.Seed.Contracts;

    public class ResourcesDataSeeder : IResourcesDataSeeder
    {
        private readonly FileByLineDbContextSeeder<ResourcesDbContext> seeder;
        private readonly string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        public ResourcesDataSeeder(Func<ResourcesDbContext> contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.seeder = new FileByLineDbContextSeeder<ResourcesDbContext>(contextFactory);

            this.dataFilesDirectoryPath = AppSettings.DataFilesDirectoryName;
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        public async Task<object> SeedAsync()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            var tasks = new[]
            {
                this.SeedProductsAsync(AppSettings.ProductsSeedFileName),
                this.SeedInstitutionsAsync(AppSettings.InstitutionsSeedFileName)
            };

            await Task.WhenAll(tasks).ConfigureAwait(false);

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }

            return true;
        }

        private async Task SeedProductsAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            try
            {
                await this.seeder.ImportSingleLineTextObjectsFromFile(
                    Path.Combine(this.dataFilesDirectoryPath, fileName),
                    (context, line) =>
                    {
                        context.Products.AddRange(new Product
                        {
                            Name = line
                        });
                    })
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task SeedInstitutionsAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            try
            {
                await this.seeder.ImportSingleLineTextObjectsFromFile(
                    Path.Combine(this.dataFilesDirectoryPath, fileName),
                    (context, line) =>
                    {
                        context.Institutions.AddRange(new Institution
                        {
                            Name = line
                        });
                    })
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }
    }
}
