namespace ProcessingTools.Geo.Data.Seed
{
    using System;
    using System.Collections.Concurrent;
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

        private DbContextSeeder<GeoDbContext> seeder;
        private string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        public GeoDataSeeder(IGeoDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
            this.seeder = new DbContextSeeder<GeoDbContext>(this.contextProvider);

            this.dataFilesDirectoryPath = ConfigurationManager.AppSettings[DataFilesDirectoryPathKey];
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        public async Task Seed()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            await this.SeedGeoNames(ConfigurationManager.AppSettings[GeoNamesSeedFileNameKey]);

            await this.SeedGeoEpithets(ConfigurationManager.AppSettings[GeoEpithetsSeedFileNameKey]);

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }
        }

        private async Task SeedGeoNames(string fileName)
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
                        context.GeoNames.AddOrUpdate(new GeoName
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

        private async Task SeedGeoEpithets(string fileName)
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
                        context.GeoEpithets.AddOrUpdate(new GeoEpithet
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