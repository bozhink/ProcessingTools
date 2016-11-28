namespace ProcessingTools.Bio.Data.Seed
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Data.Entity;
    using ProcessingTools.Bio.Data.Entity.Contracts;
    using ProcessingTools.Bio.Data.Entity.Models;
    using ProcessingTools.Data.Common.Entity.Seed;

    public class BioDataSeeder : IBioDataSeeder
    {
        private const string DataFilesDirectoryPathKey = "DataFilesDirectoryPath";
        private const string MorphologicalEpithetsFileNameKey = "MorphologicalEpithetsFileName";
        private const string TypeStatusesFileNameKey = "TypeStatusesFileName";

        private readonly IBioDbContextFactory contextFactory;
        private readonly Type stringType = typeof(string);

        private FileByLineDbContextSeeder<BioDbContext> seeder;
        private string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        public BioDataSeeder(IBioDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.contextFactory = contextFactory;
            this.seeder = new FileByLineDbContextSeeder<BioDbContext>(this.contextFactory);

            this.dataFilesDirectoryPath = ConfigurationManager.AppSettings[DataFilesDirectoryPathKey];
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        public async Task<object> Seed()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            var tasks = new List<Task>();

            tasks.Add(
                this.SeedMorphologicalEpithets(
                    ConfigurationManager.AppSettings[MorphologicalEpithetsFileNameKey]));
            tasks.Add(
                this.SeedTypeStatuses(
                    ConfigurationManager.AppSettings[TypeStatusesFileNameKey]));

            await Task.WhenAll(tasks.ToArray());

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
                    $"{dataFilesDirectoryPath}/{fileName}",
                    (context, line) =>
                    {
                        context.MorphologicalEpithets.AddOrUpdate(new MorphologicalEpithet
                        {
                            Name = line
                        });
                    });
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
                    $"{dataFilesDirectoryPath}/{fileName}",
                    (context, line) =>
                    {
                        context.TypesStatuses.AddOrUpdate(new TypeStatus
                        {
                            Name = line
                        });
                    });
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }
    }
}