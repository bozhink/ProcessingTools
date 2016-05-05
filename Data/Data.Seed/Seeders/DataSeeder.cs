namespace ProcessingTools.Data.Seed
{
    using System;
    using System.Collections.Concurrent;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Data.Common.Entity.Seed;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Models;

    public class DataSeeder : IDataSeeder
    {
        private const string DataFilesDirectoryPathKey = "DataFilesDirectoryPath";
        private const string ProductsSeedFileNameKey = "ProductsSeedFileName";
        private const string InstitutionsSeedFileNameKey = "InstitutionsSeedFileName";

        private readonly IDataDbContextProvider contextProvider;
        private readonly Type stringType = typeof(string);

        private DbContextSeeder<DataDbContext> seeder;
        private string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        public DataSeeder(IDataDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
            this.seeder = new DbContextSeeder<DataDbContext>(this.contextProvider);

            this.dataFilesDirectoryPath = ConfigurationManager.AppSettings[DataFilesDirectoryPathKey];
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        public async Task Seed()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            await this.SeedProducts(ConfigurationManager.AppSettings[ProductsSeedFileNameKey]);

            await this.SeedInstitutions(ConfigurationManager.AppSettings[InstitutionsSeedFileNameKey]);

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }
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