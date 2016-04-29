namespace ProcessingTools.Bio.Environments.Data.Seed
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Environments.Data.Models;
    using ProcessingTools.Bio.Environments.Data.Repositories.Contracts;

    public class BioEnvironmentsDataSeeder : IBioEnvironmentsDataSeeder
    {
        private const string DataFilesDirectoryPathKey = "DataFilesDirectoryPath";
        private const string EnvironmentsEntitiesFileNameKey = "EnvironmentsEntitiesFileName";
        private const string EnvironmentsNamesFileNameKey = "EnvironmentsNamesFileName";
        private const string EnvironmentsGroupsFileNameKey = "EnvironmentsGroupsFileName";
        private const string EnvironmentsGlobalFileNameKey = "EnvironmentsGlobalFileName";

        private readonly IBioEnvironmentsDbContextProvider contextProvider;
        private readonly Type stringType = typeof(string);

        private AppSettingsReader appSettingsReader;
        private string dataFilesDirectoryPath;

        public BioEnvironmentsDataSeeder(IBioEnvironmentsDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;

            this.appSettingsReader = new AppSettingsReader();
            this.dataFilesDirectoryPath = this.appSettingsReader
                .GetValue(DataFilesDirectoryPathKey, this.stringType)
                .ToString();
        }

        public async Task Seed()
        {
            await this.ImportEnvironmentsEntities(this.appSettingsReader
                .GetValue(EnvironmentsEntitiesFileNameKey, this.stringType)
                .ToString());

            await this.ImportEnvironmentsNames(this.appSettingsReader
                .GetValue(EnvironmentsNamesFileNameKey, this.stringType)
                .ToString());

            await this.ImportEnvironmentsGroups(this.appSettingsReader
                .GetValue(EnvironmentsGroupsFileNameKey, this.stringType)
                .ToString());

            await this.ImportEnvironmentsGlobals(this.appSettingsReader
                .GetValue(EnvironmentsGlobalFileNameKey, this.stringType)
                .ToString());
        }

        private async Task ImportEnvironmentsEntities(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            using (var context = this.contextProvider.Create())
            {
                var entities = new HashSet<EnvoEntity>(File.ReadAllLines($"{this.dataFilesDirectoryPath}/{fileName}")
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
                await context.SaveChangesAsync();
            }
        }

        private async Task ImportEnvironmentsNames(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            using (var context = this.contextProvider.Create())
            {
                var entities = new HashSet<EnvoEntity>(context.EnvoEntities.ToList());

                var names = new HashSet<EnvoName>(File.ReadAllLines($"{this.dataFilesDirectoryPath}/{fileName}")
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
                await context.SaveChangesAsync();
            }
        }

        private async Task ImportEnvironmentsGroups(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            using (var context = this.contextProvider.Create())
            {
                var groups = new HashSet<EnvoGroup>(File.ReadAllLines($"{this.dataFilesDirectoryPath}/{fileName}")
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
                await context.SaveChangesAsync();
            }
        }

        private async Task ImportEnvironmentsGlobals(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            using (var context = this.contextProvider.Create())
            {
                var globals = new HashSet<EnvoGlobal>(File.ReadAllLines($"{this.dataFilesDirectoryPath}/{fileName}")
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
                await context.SaveChangesAsync();
            }
        }
    }
}