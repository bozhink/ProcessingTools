namespace ProcessingTools.Bio.Taxonomy.Data.Seed
{
    using System;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data;
    using ProcessingTools.Bio.Taxonomy.Data.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Models;
    using ProcessingTools.Data.Common.Entity.Seed;

    public class BioTaxonomyDataSeeder : IBioTaxonomyDataSeeder
    {
        private const string DataFilesDirectoryPathKey = "DataFilesDirectoryPath";
        private const string RanksDataFileNameKey = "RanksDataFileName";

        private readonly ITaxonomyDbContextProvider contextProvider;
        private readonly Type stringType = typeof(string);

        private AppSettingsReader appSettingsReader;
        private string dataFilesDirectoryPath;

        private DbContextSeeder<TaxonomyDbContext> seeder;

        public BioTaxonomyDataSeeder(ITaxonomyDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
            this.seeder = new DbContextSeeder<TaxonomyDbContext>(this.contextProvider);

            this.appSettingsReader = new AppSettingsReader();
            this.dataFilesDirectoryPath = this.appSettingsReader
                .GetValue(DataFilesDirectoryPathKey, this.stringType)
                .ToString();
        }

        public async Task Seed()
        {
            await this.SeedTaxonRanks(this.appSettingsReader
                .GetValue(RanksDataFileNameKey, this.stringType)
                .ToString());
        }

        private Task SeedTaxonRanks(string fileName)
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
                        context.TaxonRanks.AddOrUpdate(new TaxonRank
                        {
                            Name = line
                        });
                    });
            });
        }
    }
}