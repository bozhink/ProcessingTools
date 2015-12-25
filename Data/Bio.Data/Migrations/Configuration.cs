namespace ProcessingTools.Bio.Data.Migrations
{
    using System;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Text;

    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<BioDbContext>
    {
        private const int NumberOfItemsToResetContext = 100;
        private readonly Encoding encoding = Encoding.UTF8;

        private Action<BioDbContext, string> addOrUpdateMorphologicalEpithet = (context, line) =>
        {
            context.MorphologicalEpithets.AddOrUpdate(new MorphologicalEpithet
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

            this.ImportSimpleObjects(dataFilesDirectoryPath + "/morphological-epithets.txt", this.addOrUpdateMorphologicalEpithet);
        }

        private void ImportSimpleObjects(string fileName, Action<BioDbContext, string> createObject)
        {
            using (var stream = new StreamReader(fileName, this.encoding))
            {
                var context = new BioDbContext();

                string line = stream.ReadLine();
                for (int i = 0; line != null; ++i, line = stream.ReadLine())
                {
                    createObject(context, line);

                    if (i % NumberOfItemsToResetContext == 0)
                    {
                        context.SaveChanges();
                        context.Dispose();

                        context = new BioDbContext();
                    }
                }

                context.SaveChanges();
                context.Dispose();
            }
        }
    }
}
