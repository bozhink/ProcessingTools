namespace ProcessingTools.Bio.Environments.Data.Seed.Seeders
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Environments.Data.Entity.Contracts;
    using ProcessingTools.Bio.Environments.Data.Entity.Models;
    using ProcessingTools.Bio.Environments.Data.Seed.Contracts;
    using ProcessingTools.Constants.Configuration;

    public class BioEnvironmentsDataSeeder : IBioEnvironmentsDataSeeder
    {
        private readonly IBioEnvironmentsDbContextProvider contextProvider;
        private readonly string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        public BioEnvironmentsDataSeeder(IBioEnvironmentsDbContextProvider contextProvider)
        {
            this.contextProvider = contextProvider ?? throw new ArgumentNullException(nameof(contextProvider));
            this.dataFilesDirectoryPath = AppSettings.DataFilesDirectoryName;
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        public async Task<object> Seed()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            await this.ImportEnvironmentsEntitiesAsync(AppSettings.EnvironmentsEntitiesFileName).ConfigureAwait(false);
            await this.ImportEnvironmentsNamesAsync(AppSettings.EnvironmentsNamesFileName).ConfigureAwait(false);
            await this.ImportEnvironmentsGroupsAsync(AppSettings.EnvironmentsGroupsFileName).ConfigureAwait(false);
            await this.ImportEnvironmentsGlobalsAsync(AppSettings.EnvironmentsGlobalFileName).ConfigureAwait(false);

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }

            return true;
        }

        private async Task ImportEnvironmentsEntitiesAsync(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            try
            {
                using (var context = this.contextProvider.Create())
                {
                    context.Configuration.UseDatabaseNullSemantics = false;
                    context.Configuration.ValidateOnSaveEnabled = false;

                    var entities = new HashSet<EnvoEntity>(File.ReadAllLines(Path.Combine(this.dataFilesDirectoryPath, fileName))
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
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task ImportEnvironmentsNamesAsync(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            try
            {
                using (var context = this.contextProvider.Create())
                {
                    context.Configuration.UseDatabaseNullSemantics = false;
                    context.Configuration.ValidateOnSaveEnabled = false;

                    var entities = new HashSet<EnvoEntity>(context.EnvoEntities.ToList());

                    var names = new HashSet<EnvoName>(File.ReadAllLines(Path.Combine(this.dataFilesDirectoryPath, fileName))
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
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task ImportEnvironmentsGroupsAsync(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            try
            {
                using (var context = this.contextProvider.Create())
                {
                    context.Configuration.UseDatabaseNullSemantics = false;
                    context.Configuration.ValidateOnSaveEnabled = false;

                    var groups = new HashSet<EnvoGroup>(File.ReadAllLines(Path.Combine(this.dataFilesDirectoryPath, fileName))
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
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task ImportEnvironmentsGlobalsAsync(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            try
            {
                using (var context = this.contextProvider.Create())
                {
                    context.Configuration.UseDatabaseNullSemantics = false;
                    context.Configuration.ValidateOnSaveEnabled = false;

                    var globals = new HashSet<EnvoGlobal>(File.ReadAllLines(Path.Combine(this.dataFilesDirectoryPath, fileName))
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
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }
    }
}
