namespace ProcessingTools.Bio.Data.Seed.Seeders
{
    using System;
    using System.Collections.Concurrent;
    using System.Data.Entity.Migrations;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Data.Entity;
    using ProcessingTools.Bio.Data.Entity.Contracts;
    using ProcessingTools.Bio.Data.Entity.Models;
    using ProcessingTools.Bio.Data.Seed.Contracts;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Data.Common.Entity.Seed;

    public class BioDataSeeder : IBioDataSeeder
    {
        private readonly FileByLineDbContextSeeder<BioDbContext> seeder;
        private readonly string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        public BioDataSeeder(IBioDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.seeder = new FileByLineDbContextSeeder<BioDbContext>(contextFactory);

            this.dataFilesDirectoryPath = AppSettings.DataFilesDirectoryName;
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        public async Task<object> Seed()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            var tasks = new Task[]
            {
                this.SeedMorphologicalEpithets(AppSettings.MorphologicalEpithetsFileName),
                this.SeedTypeStatuses(AppSettings.TypeStatusesFileName)
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
                throw new ArgumentNullException(nameof(fileName));
            }

            try
            {
                await this.seeder.ImportSingleLineTextObjectsFromFile(
                    $"{this.dataFilesDirectoryPath}/{fileName}",
                    (context, line) =>
                    {
                        context.MorphologicalEpithets.AddOrUpdate(new MorphologicalEpithet
                        {
                            Name = line
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
                throw new ArgumentNullException(nameof(fileName));
            }

            try
            {
                await this.seeder.ImportSingleLineTextObjectsFromFile(
                    $"{this.dataFilesDirectoryPath}/{fileName}",
                    (context, line) =>
                    {
                        context.TypesStatuses.AddOrUpdate(new TypeStatus
                        {
                            Name = line
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
