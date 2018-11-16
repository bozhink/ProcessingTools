namespace ProcessingTools.Geo.Data.Seed.Seeders
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Data.Entity.Abstractions;
    using ProcessingTools.Data.Entity.Geo;
    using ProcessingTools.Data.Models.Entity.Geo;
    using ProcessingTools.Geo.Data.Seed.Contracts;

    public class GeoDataSeeder : IGeoDataSeeder
    {
        private const string UserName = "system";
        private static readonly DateTime Now = DateTime.UtcNow;

        private readonly FileByLineDbContextSeeder<GeoDbContext> seeder;
        private readonly string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        public GeoDataSeeder(Func<GeoDbContext> contextFactory)
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
        public async Task<object> SeedAsync()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            var tasks = new[]
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
                        context.GeoNames.AddRange(new GeoName
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
                        context.GeoEpithets.AddRange(new GeoEpithet
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
                            context.Continents.AddRange(new Continent
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
                            context.Countries.AddRange(new Country
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
