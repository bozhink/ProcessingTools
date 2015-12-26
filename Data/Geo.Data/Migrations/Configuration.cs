namespace ProcessingTools.Geo.Data.Migrations
{
    using System;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Text;

    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<GeoDbContext>
    {
        private const int NumberOfItemsToResetContext = 100;
        private readonly Encoding encoding = Encoding.UTF8;

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

            this.ImportSimpleObjects(dataFilesDirectoryPath + "/geo-epithets.txt", this.addOrUpdateGeoEpithet);
            this.ImportSimpleObjects(dataFilesDirectoryPath + "/geo-names.txt", this.addOrUpdateGeoName);
        }

        private void ImportSimpleObjects(string fileName, Action<GeoDbContext, string> createObject)
        {
            using (var stream = new StreamReader(fileName, this.encoding))
            {
                var context = new GeoDbContext();

                string line = stream.ReadLine();
                for (int i = 0; line != null; ++i, line = stream.ReadLine())
                {
                    createObject(context, line.Trim(' ', ',', ';', '/', '\\'));

                    if (i % NumberOfItemsToResetContext == 0)
                    {
                        context.SaveChanges();
                        context.Dispose();

                        context = new GeoDbContext();
                    }
                }

                context.SaveChanges();
                context.Dispose();
            }
        }
    }
}
