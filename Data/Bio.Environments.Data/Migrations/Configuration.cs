namespace ProcessingTools.Bio.Environments.Data.Migrations
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<BioEnvironmentsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "ProcessingTools.Bio.Environments.Data.BioEnvironmentsDbContext";
        }

        protected override void Seed(BioEnvironmentsDbContext context)
        {
            if (context.EnvoNames.Count() > 0)
            {
                // Do not seed seeded database
                return;
            }

            context.Configuration.UseDatabaseNullSemantics = true;
            context.Configuration.ValidateOnSaveEnabled = false;

            var appSettingsReader = new AppSettingsReader();
            var envoTsvFilesDirectoryPath = appSettingsReader.GetValue("EnvoTsvFilesDirectoryPath", typeof(string)).ToString();

            this.ImportEnvoEntities(context, envoTsvFilesDirectoryPath + "/environments_entities.tsv");
            this.ImportEnvoNames(context, envoTsvFilesDirectoryPath + "/environments_names.tsv");
            this.ImportEnvoGroups(context, envoTsvFilesDirectoryPath + "/environments_groups.tsv");
            this.ImportEnvoGlobals(context, envoTsvFilesDirectoryPath + "/environments_global.tsv");
        }

        private void ImportEnvoEntities(BioEnvironmentsDbContext context, string fileName)
        {
            var entities = new HashSet<EnvoEntity>(File.ReadAllLines(fileName)
                .Select(l =>
                {
                    var entity = l.Split('\t');
                    return new EnvoEntity
                    {
                        Id = entity[0],
                        Index = int.Parse(entity[1]),
                        EnvoId = entity[2]
                    };
                }))
                .ToArray();

            context.EnvoEntities.AddOrUpdate(entities);
            context.SaveChanges();
        }

        private void ImportEnvoNames(BioEnvironmentsDbContext context, string fileName)
        {
            var entities = new HashSet<EnvoEntity>(context.EnvoEntities.ToList());

            var names = new HashSet<EnvoName>(File.ReadAllLines(fileName)
                .Select(l =>
                {
                    var name = l.Split('\t');
                    return new EnvoName
                    {
                        EnvoEntityId = name[0],
                        EnvoEntity = entities.FirstOrDefault(e => e.Id == name[0]),
                        Content = name[1]
                    };
                }))
                .ToArray();

            context.EnvoNames.AddOrUpdate(names);
            context.SaveChanges();
        }

        private void ImportEnvoGroups(BioEnvironmentsDbContext context, string fileName)
        {
            var groups = new HashSet<EnvoGroup>(File.ReadAllLines(fileName)
                .Select(l =>
                {
                    var group = l.Split('\t');
                    return new EnvoGroup
                    {
                        EnvoEntityId = group[0],
                        EnvoGroupId = group[1]
                    };
                }))
                .ToArray();

            context.EnvoGroups.AddOrUpdate(groups);
            context.SaveChanges();
        }

        private void ImportEnvoGlobals(BioEnvironmentsDbContext context, string fileName)
        {
            var globals = new HashSet<EnvoGlobal>(File.ReadAllLines(fileName)
                .Select(l =>
                {
                    var line = l.Split('\t');
                    return new EnvoGlobal
                    {
                        Content = line[0],
                        Status = line[1]
                    };
                }))
                .ToArray();

            context.EnvoGlobals.AddOrUpdate(globals);
            context.SaveChanges();
        }
    }
}