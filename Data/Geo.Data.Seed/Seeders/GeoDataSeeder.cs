namespace ProcessingTools.Geo.Data.Seed
{
    using System;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Data.Common.Entity.Seed;
    using ProcessingTools.Geo.Data.Contracts;
    using ProcessingTools.Geo.Data.Models;

    public class GeoDataSeeder : IGeoDataSeeder
    {
        private const string DataFilesDirectoryPathKey = "DataFilesDirectoryPath";
        private const string GeoNamesSeedFileNameKey = "GeoNamesSeedFileName";
        private const string GeoEpithetsSeedFileNameKey = "GeoEpithetsSeedFileName";

        private readonly IGeoDbContextProvider contextProvider;
        private readonly Type stringType = typeof(string);

        private string dataFilesDirectoryPath;

        private DbContextSeeder<GeoDbContext> seeder;

        public GeoDataSeeder(IGeoDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
            this.seeder = new DbContextSeeder<GeoDbContext>(this.contextProvider);

            this.dataFilesDirectoryPath = ConfigurationManager.AppSettings[DataFilesDirectoryPathKey];
        }

        public async Task Seed()
        {
            await this.SeedGeoNames(ConfigurationManager.AppSettings[GeoNamesSeedFileNameKey]);

            await this.SeedGeoEpithets(ConfigurationManager.AppSettings[GeoEpithetsSeedFileNameKey]);
        }

        private Task SeedGeoNames(string fileName)
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
                        context.GeoNames.AddOrUpdate(new GeoName
                        {
                            Name = line
                        });
                    });
            });
        }

        private Task SeedGeoEpithets(string fileName)
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
                        context.GeoEpithets.AddOrUpdate(new GeoEpithet
                        {
                            Name = line
                        });
                    });
            });
        }
    }
}