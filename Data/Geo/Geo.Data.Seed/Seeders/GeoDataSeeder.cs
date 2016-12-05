namespace ProcessingTools.Geo.Data.Seed.Seeders
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Common.Entity.Seed;
    using ProcessingTools.Geo.Data.Entity;
    using ProcessingTools.Geo.Data.Entity.Contracts;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Geo.Data.Seed.Contracts;

    public class GeoDataSeeder : IGeoDataSeeder
    {
        private const string DataFilesDirectoryPathKey = "DataFilesDirectoryPath";
        private const string GeoNamesSeedFileNameKey = "GeoNamesSeedFileName";
        private const string GeoEpithetsSeedFileNameKey = "GeoEpithetsSeedFileName";
        private const string CountryCodesSeedFileNameKey = "CountryCodesSeedFileName";
        private const string ContinentsCodesSeedFileNameKey = "ContinentsCodesSeedFileName";

        private readonly IGeoDbContextFactory contextFactory;
        private readonly Type stringType = typeof(string);

        private FileByLineDbContextSeeder<GeoDbContext> seeder;
        private string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        public GeoDataSeeder(IGeoDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.contextFactory = contextFactory;
            this.seeder = new FileByLineDbContextSeeder<GeoDbContext>(this.contextFactory);

            this.dataFilesDirectoryPath = ConfigurationManager.AppSettings[DataFilesDirectoryPathKey];
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        // TODO: Link countries and continents
        public async Task<object> Seed()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            var tasks = new List<Task>();

            tasks.Add(
                this.SeedGeoNames(
                    ConfigurationManager.AppSettings[GeoNamesSeedFileNameKey]));
            tasks.Add(
                this.SeedGeoEpithets(
                    ConfigurationManager.AppSettings[GeoEpithetsSeedFileNameKey]));
            tasks.Add(
                this.SeedContinents(
                    ConfigurationManager.AppSettings[ContinentsCodesSeedFileNameKey]));
            tasks.Add(
                this.SeedCountryCodes(
                    ConfigurationManager.AppSettings[CountryCodesSeedFileNameKey]));

            await Task.WhenAll(tasks.ToArray());

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }

            return true;
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

        private async Task SeedContinents(string fileName)
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
                        var data = line.Split('\t');
                        if (data.Length > 0)
                        {
                            context.Continents.AddOrUpdate(new Continent
                            {
                                Name = data[0]
                            });
                        }
                    });
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task SeedCountryCodes(string fileName)
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
                        var data = line.Split('\t');
                        if (data.Length > 2)
                        {
                            context.Countries.AddOrUpdate(new Country
                            {
                                Name = data[0],
                                CallingCode = data[1],
                                Iso639xCode = data[2]
                            });
                        }
                    });
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }
    }
}
