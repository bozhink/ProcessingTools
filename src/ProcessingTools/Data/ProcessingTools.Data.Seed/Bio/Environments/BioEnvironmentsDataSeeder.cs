namespace ProcessingTools.Data.Seed.Bio.Environments
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Data.Entity.Bio.Environments;
    using ProcessingTools.Data.Models.Entity.Bio.Environments;

    public class BioEnvironmentsDataSeeder : IBioEnvironmentsDataSeeder
    {
        private readonly Func<BioEnvironmentsDbContext> contextFactory;
        private readonly string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        public BioEnvironmentsDataSeeder(Func<BioEnvironmentsDbContext> contextFactory)
        {
            this.contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            this.dataFilesDirectoryPath = AppSettings.DataFilesDirectoryName;
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        public async Task<object> SeedAsync()
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
                using (var context = this.contextFactory.Invoke())
                {
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

                    context.EnvoEntities.AddRange(entities);
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
                using (var context = this.contextFactory.Invoke())
                {
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

                    context.EnvoNames.AddRange(names);
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
                using (var context = this.contextFactory.Invoke())
                {
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

                    context.EnvoGroups.AddRange(groups);
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
                using (var context = this.contextFactory.Invoke())
                {
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

                    context.EnvoGlobals.AddRange(globals);
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
