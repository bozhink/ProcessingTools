﻿// <copyright file="BioEnvironmentsDataSeeder.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Seed.Bio.Environments
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Data.Entity.Bio.Environments;
    using ProcessingTools.Data.Models.Entity.Bio.Environments;

    /// <summary>
    /// Bio environments data seeder.
    /// </summary>
    public class BioEnvironmentsDataSeeder : IBioEnvironmentsDataSeeder
    {
        private readonly Func<BioEnvironmentsDbContext> contextFactory;
        private readonly string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="BioEnvironmentsDataSeeder"/> class.
        /// </summary>
        /// <param name="contextFactory">DB context factory.</param>
        public BioEnvironmentsDataSeeder(Func<BioEnvironmentsDbContext> contextFactory)
        {
            this.contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            this.dataFilesDirectoryPath = "DataFiles";
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        /// <inheritdoc/>
        public async Task<object> SeedAsync()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            var tasks = new[]
            {
                this.ImportEnvironmentsEntitiesAsync(AppSettings.EnvironmentsEntitiesFileName),
                this.ImportEnvironmentsNamesAsync(AppSettings.EnvironmentsNamesFileName),
                this.ImportEnvironmentsGroupsAsync(AppSettings.EnvironmentsGroupsFileName),
                this.ImportEnvironmentsGlobalsAsync(AppSettings.EnvironmentsGlobalFileName),
            };

            await Task.WhenAll(tasks).ConfigureAwait(false);

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }

            return true;
        }

        private async Task ImportEnvironmentsEntitiesAsync(string fileName)
        {
            if (fileName is null)
            {
                this.exceptions.Enqueue(new FileNotFoundException(string.Empty, fileName));
                return;
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
                                Index = int.Parse(entity[1], CultureInfo.InvariantCulture),
                                EnvoId = entity[2],
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
            if (fileName is null)
            {
                this.exceptions.Enqueue(new FileNotFoundException(string.Empty, fileName));
                return;
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
                                EntityId = name[0],
                                Entity = entities.FirstOrDefault(e => e.Id == name[0]),
                                Value = name[1],
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
            if (fileName is null)
            {
                this.exceptions.Enqueue(new FileNotFoundException(string.Empty, fileName));
                return;
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
                                Entity1Id = group[0],
                                Entity2Id = group[1],
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
            if (fileName is null)
            {
                this.exceptions.Enqueue(new FileNotFoundException(string.Empty, fileName));
                return;
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
                                Value = line[0],
                                Status = line[1],
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
