// <copyright file="BioDataSeeder.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Seed.Bio
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Data.Entity.Abstractions;
    using ProcessingTools.Data.Entity.Bio;
    using ProcessingTools.Data.Models.Entity.Bio;

    /// <summary>
    /// Bio-data seeder.
    /// </summary>
    public class BioDataSeeder : IBioDataSeeder
    {
        private readonly FileByLineDbContextSeeder<BioDbContext> seeder;
        private readonly string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="BioDataSeeder"/> class.
        /// </summary>
        /// <param name="contextFactory">DbContext factory.</param>
        public BioDataSeeder(Func<BioDbContext> contextFactory)
        {
            if (contextFactory is null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.seeder = new FileByLineDbContextSeeder<BioDbContext>(contextFactory);

            this.dataFilesDirectoryPath = "DataFiles";
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        /// <inheritdoc/>
        public async Task<object> SeedAsync()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            var tasks = new[]
            {
                this.SeedMorphologicalEpithets(AppSettings.MorphologicalEpithetsFileName),
                this.SeedTypeStatuses(AppSettings.TypeStatusesFileName),
            };

            await Task.WhenAll(tasks).ConfigureAwait(false);

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }

            return true;
        }

        private async Task SeedMorphologicalEpithets(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                this.exceptions.Enqueue(new FileNotFoundException(string.Empty, fileName));
                return;
            }

            try
            {
                await this.seeder.ImportSingleLineTextObjectsFromFile(
                    Path.Combine(this.dataFilesDirectoryPath, fileName),
                    (context, line) =>
                    {
                        context.MorphologicalEpithets.AddRange(new MorphologicalEpithet
                        {
                            Name = line,
                        });
                    })
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task SeedTypeStatuses(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                this.exceptions.Enqueue(new FileNotFoundException(string.Empty, fileName));
                return;
            }

            try
            {
                await this.seeder.ImportSingleLineTextObjectsFromFile(
                    Path.Combine(this.dataFilesDirectoryPath, fileName),
                    (context, line) =>
                    {
                        context.TypesStatuses.AddRange(new TypeStatus
                        {
                            Name = line,
                        });
                    })
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }
    }
}
