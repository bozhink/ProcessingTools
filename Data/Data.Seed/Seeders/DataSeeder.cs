namespace ProcessingTools.Data.Seed
{
    using System;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Data.Common.Entity.Seed;
    using ProcessingTools.Data.Models;
    using ProcessingTools.Data.Repositories.Contracts;

    public class DataSeeder : IDataSeeder
    {
        private const string DataFilesDirectoryPathKey = "DataFilesDirectoryPath";
        private const string ProductsSeedFileNameKey = "ProductsSeedFileName";
        private const string InstitutionsSeedFileNameKey = "InstitutionsSeedFileName";

        private readonly IDataDbContextProvider contextProvider;
        private readonly Type stringType = typeof(string);

        private AppSettingsReader appSettingsReader;
        private string dataFilesDirectoryPath;

        private DbContextSeeder<DataDbContext> seeder;

        public DataSeeder(IDataDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;

            this.appSettingsReader = new AppSettingsReader();
            this.dataFilesDirectoryPath = this.appSettingsReader.GetValue(DataFilesDirectoryPathKey, this.stringType).ToString();

            this.seeder = new DbContextSeeder<DataDbContext>(this.contextProvider);
        }

        public async Task Seed()
        {
            await this.SeedProducts(this.appSettingsReader
                .GetValue(ProductsSeedFileNameKey, this.stringType)
                .ToString());

            await this.SeedInstitutions(this.appSettingsReader
                .GetValue(InstitutionsSeedFileNameKey, this.stringType)
                .ToString());
        }

        private Task SeedProducts(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            return Task.Run(() =>
            {
                this.seeder.ImportSingleLineTextObjectsFromFile(
                    $"{dataFilesDirectoryPath}/{fileName}",
                    (context, line) =>
                    {
                        context.Products.AddOrUpdate(new Product
                        {
                            Name = line
                        });
                    });
            });
        }

        private Task SeedInstitutions(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            return Task.Run(() =>
            {
                this.seeder.ImportSingleLineTextObjectsFromFile(
                    $"{dataFilesDirectoryPath}/{fileName}",
                    (context, line) =>
                    {
                        context.Institutions.AddOrUpdate(new Institution
                        {
                            Name = line
                        });
                    });
            });
        }
    }
}