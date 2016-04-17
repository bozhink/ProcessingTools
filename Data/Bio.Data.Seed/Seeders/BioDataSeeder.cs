namespace ProcessingTools.Bio.Data.Seed
{
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Data;
    using ProcessingTools.Bio.Data.Migrations;
    using ProcessingTools.Bio.Data.Models;
    using ProcessingTools.Data.Common.Entity.Seed;

    public class BioDataSeeder : IBioDataSeeder
    {
        private const string DataFilesDirectoryPathKey = "DataFilesDirectoryPath";
        private const string MorphologicalEpithetsFileNameKey = "MorphologicalEpithetsFileName";
        private const string TypeStatusesFileNameKey = "TypeStatusesFileName";

        private readonly Type stringType = typeof(string);

        private AppSettingsReader appSettingsReader;
        private string dataFilesDirectoryPath;

        private DbContextSeeder<BioDbContext> seeder;

        public BioDataSeeder()
        {
            this.appSettingsReader = new AppSettingsReader();
            this.dataFilesDirectoryPath = this.appSettingsReader.GetValue(DataFilesDirectoryPathKey, this.stringType).ToString();

            this.seeder = new DbContextSeeder<BioDbContext>();
        }

        public async Task Init()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BioDbContext, Configuration>());

            using (var db = new BioDbContext())
            {
                db.Database.CreateIfNotExists();
                db.Database.Initialize(true);
                await db.SaveChangesAsync();
            }
        }

        public async Task Seed()
        {
            var seeder = new DbContextSeeder<BioDbContext>();

            await this.SeedMorphologicalEpithets(this.appSettingsReader
                .GetValue(MorphologicalEpithetsFileNameKey, this.stringType)
                .ToString());

            await this.SeedTypeStatuses(this.appSettingsReader
                .GetValue(TypeStatusesFileNameKey, this.stringType)
                .ToString());
        }

        private Task SeedMorphologicalEpithets(string fileName)
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
                        context.MorphologicalEpithets.AddOrUpdate(new MorphologicalEpithet
                        {
                            Name = line
                        });
                    });
            });
        }

        private Task SeedTypeStatuses(string fileName)
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
                        context.TypesStatuses.AddOrUpdate(new TypeStatus
                        {
                            Name = line
                        });
                    });
            });
        }
    }
}