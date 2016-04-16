namespace ProcessingTools.Geo.Data.Seed
{
    using System;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Data.Common.Entity.Seed;
    using ProcessingTools.Geo.Data.Models;

    public class GeoDataSeeder : IGeoDataSeeder
    {
        private const string DataFilesDirectoryPathKey = "DataFilesDirectoryPath";
        private const string GeoNamesSeedFileNameKey = "GeoNamesSeedFileName";
        private const string GeoEpithetsSeedFileNameKey = "GeoEpithetsSeedFileName";

        private readonly Type stringType = typeof(string);

        private AppSettingsReader appSettingsReader;
        private string dataFilesDirectoryPath;

        private DbContextSeeder<GeoDbContext> seeder;

        public GeoDataSeeder()
        {
            this.appSettingsReader = new AppSettingsReader();
            this.dataFilesDirectoryPath = this.appSettingsReader.GetValue(DataFilesDirectoryPathKey, this.stringType).ToString();

            this.seeder = new DbContextSeeder<GeoDbContext>();
        }

        public async Task Seed()
        {
            await this.SeedGeoNames(this.appSettingsReader
                .GetValue(GeoNamesSeedFileNameKey, this.stringType)
                .ToString());

            await this.SeedGeoEpithets(this.appSettingsReader
                .GetValue(GeoEpithetsSeedFileNameKey, this.stringType)
                .ToString());
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