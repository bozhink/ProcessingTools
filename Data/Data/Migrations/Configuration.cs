namespace ProcessingTools.Data.Migrations
{
    using System;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Text;

    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<DataDbContext>
    {
        private const int NumberOfItemsToResetContext = 100;
        private readonly Encoding encoding = Encoding.UTF8;

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

            this.ImportSimpleObjects(dataFilesDirectoryPath + "/products.txt", this.addOrUpdateProduct);
            this.ImportSimpleObjects(dataFilesDirectoryPath + "/institutions.txt", this.addOrUpdateInstitution);
        }

        private void ImportSimpleObjects(string fileName, Action<DataDbContext, string> createObject)
        {
            using (var stream = new StreamReader(fileName, this.encoding))
            {
                var context = new DataDbContext();

                string line = stream.ReadLine();
                for (int i = 0; line != null; ++i, line = stream.ReadLine())
                {
                    createObject(context, line);

                    if (i % NumberOfItemsToResetContext == 0)
                    {
                        context.SaveChanges();
                        context.Dispose();

                        context = new DataDbContext();
                    }
                }

                context.SaveChanges();
                context.Dispose();
            }
        }
    }
}