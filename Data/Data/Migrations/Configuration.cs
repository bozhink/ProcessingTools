namespace ProcessingTools.Data.Migrations
{
    using System;
    using System.Configuration;
    using System.Data.Entity.Migrations;

    using Models;
    using ProcessingTools.Data.Common.Seed;

    public sealed class Configuration : DbMigrationsConfiguration<DataDbContext>
    {
        private Action<DataDbContext, string> addOrUpdateProduct = (context, line) =>
        {
            context.Products.AddOrUpdate(new Product
            {
                Name = line
            });
        };

        private Action<DataDbContext, string> addOrUpdateInstitution = (context, line) =>
        {
            context.Institutions.AddOrUpdate(new Institution
            {
                Name = line
            });
        };

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "ProcessingTools.Data.DataDbContext";
        }

        protected override void Seed(DataDbContext context)
        {
            var appSettingsReader = new AppSettingsReader();
            var dataFilesDirectoryPath = appSettingsReader.GetValue("DataFilesDirectoryPath", typeof(string)).ToString();

            var seeder = new DbContextSeeder<DataDbContext>();

            seeder.ImportSingleLineTextObjectsFromFile(
                $"{dataFilesDirectoryPath}/products.txt",
                this.addOrUpdateProduct);
            seeder.ImportSingleLineTextObjectsFromFile(
                $"{dataFilesDirectoryPath}/institutions.txt",
                this.addOrUpdateInstitution);
        }
    }
}