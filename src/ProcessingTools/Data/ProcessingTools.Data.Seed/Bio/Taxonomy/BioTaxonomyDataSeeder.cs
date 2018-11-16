namespace ProcessingTools.Data.Seed.Bio.Taxonomy
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Data.Bio.Taxonomy.Xml.Contracts.Repositories;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Entity.Abstractions;
    using ProcessingTools.Data.Entity.Bio.Taxonomy;
    using ProcessingTools.Data.Models.Entity.Bio.Taxonomy;
    using ProcessingTools.Extensions;
    using ProcessingTools.Extensions.Linq;

    public class BioTaxonomyDataSeeder : IBioTaxonomyDataSeeder
    {
        private const int NumberOfItemsToImportAtOnce = 100;

        private readonly IRepositoryFactory<IXmlBiotaxonomicBlackListRepository> blackListRepositoryFactory;
        private readonly Func<BioTaxonomyDbContext> contextFactory;
        private readonly IRepositoryFactory<IXmlTaxonRankRepository> taxonomicRepositoryFactory;
        private readonly FileByLineDbContextSeeder<BioTaxonomyDbContext> seeder;
        private readonly string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        public BioTaxonomyDataSeeder(
            Func<BioTaxonomyDbContext> contextFactory,
            IRepositoryFactory<IXmlTaxonRankRepository> taxonomicRepositoryFactory,
            IRepositoryFactory<IXmlBiotaxonomicBlackListRepository> blackListRepositoryFactory)
        {
            this.contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            this.taxonomicRepositoryFactory = taxonomicRepositoryFactory ?? throw new ArgumentNullException(nameof(taxonomicRepositoryFactory));
            this.blackListRepositoryFactory = blackListRepositoryFactory ?? throw new ArgumentNullException(nameof(blackListRepositoryFactory));
            this.seeder = new FileByLineDbContextSeeder<BioTaxonomyDbContext>(this.contextFactory);

            this.dataFilesDirectoryPath = AppSettings.DataFilesDirectoryName;
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        public async Task<object> SeedAsync()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            await this.SeedTaxaRanksAsync(AppSettings.RanksDataFileName).ConfigureAwait(false);
            await this.SeedTaxaNamesAsync().ConfigureAwait(false);
            await this.SeedBlackListAsync().ConfigureAwait(false);

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }

            return true;
        }

        private async Task SeedBlackListAsync()
        {
            try
            {
                var repository = this.blackListRepositoryFactory.Create();

                var context = this.contextFactory.Invoke();

                for (int i = 0; ; ++i)
                {
                    try
                    {
                        var blackListItems = repository.Entities
                            .OrderBy(t => t)
                            .Skip(i * NumberOfItemsToImportAtOnce)
                            .Take(NumberOfItemsToImportAtOnce)
                            .Select(item => new BlackListEntity
                            {
                                Content = item.Content
                            })
                            .ToArray();

                        if (blackListItems == null || blackListItems.Length < 1)
                        {
                            break;
                        }

                        context.BlackListedItems.AddRange(blackListItems);

                        await context.SaveChangesAsync().ConfigureAwait(false);
                        context.Dispose();
                        context = this.contextFactory.Invoke();
                    }
                    catch (Exception e)
                    {
                        this.exceptions.Enqueue(e);
                        break;
                    }
                }

                await context.SaveChangesAsync().ConfigureAwait(false);
                context.Dispose();
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task SeedTaxaNamesAsync()
        {
            try
            {
                var repository = this.taxonomicRepositoryFactory.Create();

                var context = this.contextFactory.Invoke();

                for (int i = 0; ; ++i)
                {
                    try
                    {
                        var ranks = context.TaxonRanks.ToList();

                        var taxa = await repository.Query
                            .OrderBy(t => t.Name)
                            .Skip(i * NumberOfItemsToImportAtOnce)
                            .Take(NumberOfItemsToImportAtOnce)
                            .Select(taxon => new TaxonName
                            {
                                Name = taxon.Name,
                                Ranks = taxon.Ranks.Select(rank => ranks.FirstOrDefault(r => r.Name == rank.MapTaxonRankTypeToTaxonRankString())).ToList(),
                                WhiteListed = taxon.IsWhiteListed
                            })
                            .ToArrayAsync()
                            .ConfigureAwait(false);

                        if (taxa == null || taxa.Length < 1)
                        {
                            break;
                        }

                        context.TaxonNames.AddRange(taxa);

                        await context.SaveChangesAsync().ConfigureAwait(false);
                        context.Dispose();
                        context = this.contextFactory.Invoke();
                    }
                    catch (Exception e)
                    {
                        this.exceptions.Enqueue(e);
                        break;
                    }
                }

                await context.SaveChangesAsync().ConfigureAwait(false);
                context.Dispose();
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task SeedTaxaRanksAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            try
            {
                await this.seeder.ImportSingleLineTextObjectsFromFile(
                    Path.Combine(this.dataFilesDirectoryPath, fileName),
                    (context, line) =>
                    {
                        context.TaxonRanks.AddRange(new TaxonRank
                        {
                            Name = line
                        });
                    })
                    .ConfigureAwait(false);

                var repository = this.taxonomicRepositoryFactory.Create();

                var ranks = await repository.Query
                    .SelectMany(t => t.Ranks)
                    .Select(r => r.MapTaxonRankTypeToTaxonRankString())
                    .Distinct()
                    .ToArrayAsync()
                    .ConfigureAwait(false);

                using (var context = this.contextFactory.Invoke())
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

                        context.TaxonRanks.AddRange(new TaxonRank
                        {
                            Name = rank
                        });
                    }

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
