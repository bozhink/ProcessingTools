namespace ProcessingTools.Bio.Taxonomy.Data.Seed.Seeders
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Data.Entity;
    using ProcessingTools.Bio.Taxonomy.Data.Entity.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Entity.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Seed.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts.Repositories;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Data.Common.Entity.Seed;

    public class BioTaxonomyDataSeeder : IBioTaxonomyDataSeeder
    {
        private const int NumberOfItemsToImportAtOnce = 100;
        private const string DataFilesDirectoryPathKey = "DataFilesDirectoryPath";
        private const string RanksDataFileNameKey = "RanksDataFileName";

        private readonly IRepositoryFactory<IXmlBiotaxonomicBlackListIterableRepository> blackListRepositoryFactory;
        private readonly IRepositoryFactory<IXmlTaxonRankRepository> taxonomicRepositoryFactory;
        private readonly IBioTaxonomyDbContextFactory contextFactory;
        private readonly Type stringType = typeof(string);

        private FileByLineDbContextSeeder<BioTaxonomyDbContext> seeder;
        private string dataFilesDirectoryPath;
        private ConcurrentQueue<Exception> exceptions;

        public BioTaxonomyDataSeeder(
            IBioTaxonomyDbContextFactory contextFactory,
            IRepositoryFactory<IXmlTaxonRankRepository> taxonomicRepositoryFactory,
            IRepositoryFactory<IXmlBiotaxonomicBlackListIterableRepository> blackListRepositoryFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            if (taxonomicRepositoryFactory == null)
            {
                throw new ArgumentNullException(nameof(taxonomicRepositoryFactory));
            }

            if (blackListRepositoryFactory == null)
            {
                throw new ArgumentNullException(nameof(blackListRepositoryFactory));
            }

            this.contextFactory = contextFactory;
            this.taxonomicRepositoryFactory = taxonomicRepositoryFactory;
            this.blackListRepositoryFactory = blackListRepositoryFactory;
            this.seeder = new FileByLineDbContextSeeder<BioTaxonomyDbContext>(this.contextFactory);

            this.dataFilesDirectoryPath = ConfigurationManager.AppSettings[DataFilesDirectoryPathKey];
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        public async Task<object> Seed()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            await this.SeedTaxaRanks(ConfigurationManager.AppSettings[RanksDataFileNameKey]);

            await this.SeedTaxaNames();

            await this.SeedBlackList();

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }

            return true;
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
                var ranks = new HashSet<string>((await repository.All())
                    .SelectMany(t => t.Ranks)
                    .Select(r => r.MapTaxonRankTypeToTaxonRankString())
                    .ToList());

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

                        var taxa = (await repository.All())
                            .OrderBy(t => t.Name)
                            .Skip(i * NumberOfItemsToImportAtOnce)
                            .Take(NumberOfItemsToImportAtOnce)
                            .Select(taxon => new TaxonName
                            {
                                Name = taxon.Name,
                                Ranks = taxon.Ranks.Select(rank => ranks.FirstOrDefault(r => r.Name == rank.MapTaxonRankTypeToTaxonRankString())).ToList(),
                                WhiteListed = taxon.IsWhiteListed
                            })
                            .ToArray();

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
    }
}
