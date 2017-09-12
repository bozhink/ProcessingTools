namespace ProcessingTools.Geo.Data.Seed.Seeders
{
    using System;
    using System.Collections.Concurrent;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Data.Common.Entity.Seed;
    using ProcessingTools.Geo.Data.Entity;
    using ProcessingTools.Geo.Data.Entity.Contracts;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Geo.Data.Seed.Contracts;

    public class GeoDataSeeder : IGeoDataSeeder
    {
        private const string UserName = "system";
        private static readonly DateTime Now = DateTime.UtcNow;

        private readonly FileByLineDbContextSeeder<GeoDbContext> seeder;
        private readonly string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        public GeoDataSeeder(IGeoDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.seeder = new FileByLineDbContextSeeder<GeoDbContext>(contextFactory);

            this.dataFilesDirectoryPath = AppSettings.DataFilesDirectoryName;
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        // TODO: Link countries and continents
        public async Task<object> Seed()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            var tasks = new Task[]
            {
                this.SeedGeoNamesAsync(AppSettings.GeoNamesSeedFileName),
                this.SeedGeoEpithetsAsync(AppSettings.GeoEpithetsSeedFileName),
                this.SeedContinentsAsync(AppSettings.ContinentsCodesSeedFileName),
                this.SeedCountryCodesAsync(AppSettings.CountryCodesSeedFileName)
            };

            await Task.WhenAll(tasks).ConfigureAwait(false);

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }

            return true;
        }

        private async Task SeedGeoNamesAsync(string fileName)
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
                        context.GeoNames.AddOrUpdate(new GeoName
                        {
                            Name = line,
                            CreatedBy = UserName,
                            CreatedOn = Now,
                            ModifiedBy = UserName,
                            ModifiedOn = Now
                        });
                    })
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task SeedGeoEpithetsAsync(string fileName)
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
                        context.GeoEpithets.AddOrUpdate(new GeoEpithet
                        {
                            Name = line,
                            CreatedBy = UserName,
                            CreatedOn = Now,
                            ModifiedBy = UserName,
                            ModifiedOn = Now
                        });
                    })
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task SeedContinentsAsync(string fileName)
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
                        var data = line.Split('\t');
                        if (data.Length > 0)
                        {
                            context.Continents.AddOrUpdate(new Continent
                            {
                                Name = data[0],
                                CreatedBy = UserName,
                                CreatedOn = Now,
                                ModifiedBy = UserName,
                                ModifiedOn = Now
                            });
                        }
                    })
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task SeedCountryCodesAsync(string fileName)
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
                        var data = line.Split('\t');
                        if (data.Length > 2 && data[0].Trim().Length > 1)
                        {
                            var languageCodes = data[2].Split('/').Select(l => l.Trim()).ToArray();
                            context.Countries.AddOrUpdate(new Country
                            {
                                Name = data[0],
                                CallingCode = data[1],
                                LanguageCode = languageCodes[0],
                                Iso639xCode = languageCodes[1],
                                CreatedBy = UserName,
                                CreatedOn = Now,
                                ModifiedBy = UserName,
                                ModifiedOn = Now
                            });
                        }
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
