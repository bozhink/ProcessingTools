namespace ProcessingTools.Geo.Data.Migrations
{
    using System;
    using System.Configuration;
    using System.Data.Entity.Migrations;

    using Models;
    using ProcessingTools.Data.Common.Entity.Seed;

    public sealed class Configuration : DbMigrationsConfiguration<GeoDbContext>
    {
        private Action<GeoDbContext, string> addOrUpdateGeoEpithet = (context, line) =>
        {
            context.GeoEpithets.AddOrUpdate(new GeoEpithet
            {
                Name = line
            });
        };

        private Action<GeoDbContext, string> addOrUpdateGeoName = (context, line) =>
        {
            context.GeoNames.AddOrUpdate(new GeoName
            {
                Name = line
            });
        };

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "ProcessingTools.Geo.Data.GeoDbContext";
        }

        protected override void Seed(GeoDbContext context)
        {
            var appSettingsReader = new AppSettingsReader();
            var dataFilesDirectoryPath = appSettingsReader.GetValue("DataFilesDirectoryPath", typeof(string)).ToString();

            var seeder = new DbContextSeeder<GeoDbContext>();

            seeder.ImportSingleLineTextObjectsFromFile(
                $"{dataFilesDirectoryPath}/geo-epithets.txt",
                this.addOrUpdateGeoEpithet);
            seeder.ImportSingleLineTextObjectsFromFile(
                $"{dataFilesDirectoryPath}/geo-names.txt",
                this.addOrUpdateGeoName);
        }
    }
}
