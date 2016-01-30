namespace ProcessingTools.Bio.Data.Migrations
{
    using System;
    using System.Configuration;
    using System.Data.Entity.Migrations;

    using Models;
    using ProcessingTools.Data.Common.Seed;

    public sealed class Configuration : DbMigrationsConfiguration<BioDbContext>
    {
        private Action<BioDbContext, string> addOrUpdateMorphologicalEpithet = (context, line) =>
        {
            context.MorphologicalEpithets.AddOrUpdate(new MorphologicalEpithet
            {
                Name = line
            });
        };

        private Action<BioDbContext, string> addOrUpdateTypeStatus = (context, line) =>
        {
            context.TypesStatuses.AddOrUpdate(new TypeStatus
            {
                Name = line
            });
        };

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "ProcessingTools.Bio.Data.BioDbContext";
        }

        protected override void Seed(BioDbContext context)
        {
            var appSettingsReader = new AppSettingsReader();
            var dataFilesDirectoryPath = appSettingsReader.GetValue("DataFilesDirectoryPath", typeof(string)).ToString();

            var seeder = new DbContextSeeder<BioDbContext>();

            seeder.ImportSingleLineTextObjectsFromFile(
                $"{dataFilesDirectoryPath}/morphological-epithets.txt",
                this.addOrUpdateMorphologicalEpithet);

            seeder.ImportSingleLineTextObjectsFromFile(
                $"{dataFilesDirectoryPath}/type-statuses.txt",
                this.addOrUpdateTypeStatus);
        }
    }
}
