namespace ProcessingTools.Bio.Taxonomy.Data.Seed
{
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data;
    using ProcessingTools.Bio.Taxonomy.Data.Migrations;
    using ProcessingTools.Bio.Taxonomy.Data.Models;
    using ProcessingTools.Data.Common.Entity.Seed;

    public class BioTaxonomyDataSeeder : IBioTaxonomyDataSeeder
    {
        private const string DataFilesDirectoryPathKey = "DataFilesDirectoryPath";
        private const string RanksDataFileNameKey = "RanksDataFileName";

        private readonly Type stringType = typeof(string);

        private AppSettingsReader appSettingsReader;
        private string dataFilesDirectoryPath;

        private DbContextSeeder<TaxonomyDbContext> seeder;

        public BioTaxonomyDataSeeder()
        {
            this.appSettingsReader = new AppSettingsReader();
            this.dataFilesDirectoryPath = this.appSettingsReader
                .GetValue(DataFilesDirectoryPathKey, this.stringType)
                .ToString();

            this.seeder = new DbContextSeeder<TaxonomyDbContext>();
        }

        public async Task Init()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TaxonomyDbContext, Configuration>());

            using (var db = new TaxonomyDbContext())
            {
                db.Database.CreateIfNotExists();
                db.Database.Initialize(true);
                await db.SaveChangesAsync();
            }
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