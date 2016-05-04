namespace ProcessingTools.Bio.Taxonomy.Data.Seed
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data;
    using ProcessingTools.Bio.Taxonomy.Data.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories.Contracts;
    using ProcessingTools.Data.Common.Entity.Seed;

    public class BioTaxonomyDataSeeder : IBioTaxonomyDataSeeder
    {
        private const int NumberOfItemsToImportAtOnce = 100;
        private const string DataFilesDirectoryPathKey = "DataFilesDirectoryPath";
        private const string RanksDataFileNameKey = "RanksDataFileName";

        private readonly ITaxaRepositoryProvider repositoryProvider;
        private readonly IBioTaxonomyDbContextProvider contextProvider;
        private readonly Type stringType = typeof(string);

        private DbContextSeeder<BioTaxonomyDbContext> seeder;
        private string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        public BioTaxonomyDataSeeder(IBioTaxonomyDbContextProvider contextProvider, ITaxaRepositoryProvider repositoryProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.contextProvider = contextProvider;
            this.repositoryProvider = repositoryProvider;
            this.seeder = new DbContextSeeder<BioTaxonomyDbContext>(this.contextProvider);

            this.dataFilesDirectoryPath = ConfigurationManager.AppSettings[DataFilesDirectoryPathKey];
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        public async Task Seed()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            await this.SeedTaxaRanks(ConfigurationManager.AppSettings[RanksDataFileNameKey]);

            await this.SeedTaxaNames();

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }
        }

        private async Task SeedTaxaRanks(string fileName)
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
                        context.TaxonRanks.AddOrUpdate(new TaxonRank
                        {
                            Name = line
                        });
                    });

                var repository = this.repositoryProvider.Create();
                var ranks = new HashSet<string>((await repository.All())
                    .SelectMany(t => t.Ranks)
                    .ToList());

                using (var context = this.contextProvider.Create())
                {
                    foreach (var rank in ranks)
                    {
                        try
                        {
                            if (context.TaxonRanks.Where(r => r.Name == rank).ToList().Count > 0)
                            {
                                continue;
                            }
                        }
                        catch
                        {
                            continue;
                        }

                        context.TaxonRanks.AddOrUpdate(new TaxonRank
                        {
                            Name = rank
                        });
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task SeedTaxaNames()
        {
            try
            {
                var repository = this.repositoryProvider.Create();

                var context = this.contextProvider.Create();

                for (int i = 0; true; i++)
                {
                    try
                    {
                        var ranks = context.TaxonRanks.ToList();

                        var taxa = (await repository.All())
                            .OrderBy(t => t.Name)
                            .Skip(i * NumberOfItemsToImportAtOnce)
                            .Take(NumberOfItemsToImportAtOnce)
                            .Select(taxon => new TaxonName
                            {
                                Name = taxon.Name,
                                Ranks = taxon.Ranks.Select(rank => ranks.FirstOrDefault(r => r.Name == rank)).ToList()
                            })
                            .ToArray();

                        if (taxa == null || taxa.Length < 1)
                        {
                            break;
                        }

                        context.TaxonNames.AddOrUpdate(taxa);

                        await context.SaveChangesAsync();
                        context.Dispose();
                        context = this.contextProvider.Create();
                    }
                    catch (Exception e)
                    {
                        this.exceptions.Enqueue(e);
                        break;
                    }
                }

                await context.SaveChangesAsync();
                context.Dispose();

            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }
    }
}