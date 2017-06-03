namespace ProcessingTools.Bio.Taxonomy.Data.Seed.Seeders
{
    using System;
    using System.Collections.Concurrent;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Data.Entity;
    using ProcessingTools.Bio.Taxonomy.Data.Entity.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Entity.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Seed.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts.Repositories;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Data.Common.Entity.Seed;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Common.Extensions.Linq;

    public class BioTaxonomyDataSeeder : IBioTaxonomyDataSeeder
    {
        private const int NumberOfItemsToImportAtOnce = 100;

        private readonly IRepositoryFactory<IXmlBiotaxonomicBlackListRepository> blackListRepositoryFactory;
        private readonly IBioTaxonomyDbContextFactory contextFactory;
        private readonly Type stringType = typeof(string);
        private readonly IRepositoryFactory<IXmlTaxonRankRepository> taxonomicRepositoryFactory;
        private string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;
        private FileByLineDbContextSeeder<BioTaxonomyDbContext> seeder;

        public BioTaxonomyDataSeeder(
            IBioTaxonomyDbContextFactory contextFactory,
            IRepositoryFactory<IXmlTaxonRankRepository> taxonomicRepositoryFactory,
            IRepositoryFactory<IXmlBiotaxonomicBlackListRepository> blackListRepositoryFactory)
        {
            this.contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            this.taxonomicRepositoryFactory = taxonomicRepositoryFactory ?? throw new ArgumentNullException(nameof(taxonomicRepositoryFactory));
            this.blackListRepositoryFactory = blackListRepositoryFactory ?? throw new ArgumentNullException(nameof(blackListRepositoryFactory));
            this.seeder = new FileByLineDbContextSeeder<BioTaxonomyDbContext>(this.contextFactory);

            this.dataFilesDirectoryPath = ConfigurationManager.AppSettings[AppSettingsKeys.DataFilesDirectoryName];
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        public async Task<object> Seed()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            await this.SeedTaxaRanks(ConfigurationManager.AppSettings[AppSettingsKeys.RanksDataFileName]);

            await this.SeedTaxaNames();

            await this.SeedBlackList();

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }

            return true;
        }

        private async Task SeedBlackList()
        {
            try
            {
                var repository = this.blackListRepositoryFactory.Create();

                var context = this.contextFactory.Create();

                for (int i = 0; true; ++i)
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

                        context.BlackListedItems.AddOrUpdate(blackListItems);

                        await context.SaveChangesAsync();
                        context.Dispose();
                        context = this.contextFactory.Create();
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

        private async Task SeedTaxaNames()
        {
            try
            {
                var repository = this.taxonomicRepositoryFactory.Create();

                var context = this.contextFactory.Create();

                for (int i = 0; true; ++i)
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
                            .ToArrayAsync();

                        if (taxa == null || taxa.Length < 1)
                        {
                            break;
                        }

                        context.TaxonNames.AddOrUpdate(taxa);

                        await context.SaveChangesAsync();
                        context.Dispose();
                        context = this.contextFactory.Create();
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

                var repository = this.taxonomicRepositoryFactory.Create();

                var ranks = await repository.Query
                    .SelectMany(t => t.Ranks)
                    .Select(r => r.MapTaxonRankTypeToTaxonRankString())
                    .Distinct()
                    .ToArrayAsync();

                using (var context = this.contextFactory.Create())
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
    }
}
