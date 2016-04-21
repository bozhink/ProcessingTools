namespace ProcessingTools.Bio.Taxonomy.Data.Repositories
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Models;

    using ProcessingTools.Bio.Taxonomy.Constants;
    using ProcessingTools.Configurator;

    public class TaxaRepository : ITaxaRepository
    {
        private const int MillisecondsToUpdate = 500;
        private const string RootNodeName = "taxa";
        private const string TaxonNodeName = "taxon";
        private const string WhiteListedAttributeName = "white-listed";
        private const string TaxonPartNodeName = "part";
        private const string TaxonNameNodeName = "value";
        private const string TaxonRanksNodeName = "rank";
        private const string TaxonRankNodeName = "value";

        private DateTime? lastUpdated;

        private Regex matchNonWhiteListedHigherTaxon = new Regex(TaxaRegexPatterns.HigherTaxaMatchPattern);

        public TaxaRepository()
            : this(ConfigBuilder.Create())
        {
        }

        public TaxaRepository(Config config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            this.Config = config;
            this.TaxaWhiteListed = new ConcurrentDictionary<string, bool>();
            this.TaxaRanks = new ConcurrentDictionary<string, HashSet<string>>();

            this.ReadTaxaFromFile(true).Wait();
        }

        private Config Config { get; set; }

        private ConcurrentDictionary<string, bool> TaxaWhiteListed { get; set; }

        private ConcurrentDictionary<string, HashSet<string>> TaxaRanks { get; set; }

        public async Task<object> Add(Taxon entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return await this.AddTaxon(entity, true);
        }

        public async Task<IQueryable<Taxon>> All()
        {
            await this.ReadTaxaFromFile(false);

            return this.TaxaRanks
                .Select(t => new Taxon
                {
                    Name = t.Key,
                    Ranks = t.Value,
                    IsWhiteListed = this.TaxaWhiteListed.GetOrAdd(
                        t.Key,
                        !this.matchNonWhiteListedHigherTaxon.IsMatch(t.Key))
                })
                .AsQueryable();
        }

        public async Task<IQueryable<Taxon>> All(Expression<Func<Taxon, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return (await this.All()).Where(filter);
        }

        public async Task<IQueryable<Taxon>> All(Expression<Func<Taxon, object>> sort, int skip, int take)
        {
            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }

            if (skip < 0)
            {
                throw new ArgumentException(string.Empty, nameof(skip));
            }

            if (take < 1)
            {
                throw new ArgumentException(string.Empty, nameof(take));
            }

            return (await this.All())
                .OrderBy(sort)
                .Skip(skip)
                .Take(take);
        }

        public async Task<IQueryable<Taxon>> All(Expression<Func<Taxon, bool>> filter, Expression<Func<Taxon, object>> sort, int skip, int take)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }

            if (skip < 0)
            {
                throw new ArgumentException(string.Empty, nameof(skip));
            }

            if (take < 1)
            {
                throw new ArgumentException(string.Empty, nameof(take));
            }

            return (await this.All())
                .Where(filter)
                .OrderBy(sort)
                .Skip(skip)
                .Take(take);
        }

        public async Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await this.ReadTaxaFromFile(false);
            var value = new HashSet<string>();
            return this.TaxaRanks.TryRemove(id.ToString(), out value);
        }

        public async Task<object> Delete(Taxon entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return await this.Delete(entity.Name);
        }

        public async Task<Taxon> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return (await this.All())
                .FirstOrDefault(t => t.Name == id.ToString());
        }

        public Task<int> SaveChanges()
        {
            return this.WriteTaxaToFile();
        }

        public async Task<object> Update(Taxon entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await this.ReadTaxaFromFile(false);

            string name = entity.Name;

            this.TaxaWhiteListed.AddOrUpdate(name, entity.IsWhiteListed, (k, b) => entity.IsWhiteListed);

            var ranks = new HashSet<string>(entity.Ranks);
            if (this.TaxaRanks.ContainsKey(name))
            {
                this.TaxaRanks[name] = ranks;
            }
            else
            {
                this.TaxaRanks.GetOrAdd(name, ranks);
            }

            return entity;
        }

        private Task<Taxon> AddTaxon(Taxon taxon, bool update)
        {
            return Task.Run(() =>
            {
                string name = taxon.Name;

                if (update)
                {
                    this.TaxaWhiteListed.AddOrUpdate(name, taxon.IsWhiteListed, (k, b) => taxon.IsWhiteListed);
                }

                var ranksToAdd = new HashSet<string>(taxon.Ranks.Where(r => !string.IsNullOrWhiteSpace(r)));
                if (this.TaxaRanks.ContainsKey(name))
                {
                    foreach (var rank in ranksToAdd)
                    {
                        this.TaxaRanks[name].Add(rank);
                    }
                }
                else
                {
                    this.TaxaRanks.GetOrAdd(name, ranksToAdd);
                }

                return taxon;
            });
        }

        private async Task ReadTaxaFromFile(bool update)
        {
            var timeSpan = this.lastUpdated - DateTime.Now;
            if (timeSpan.HasValue &&
                timeSpan.Value.Milliseconds < MillisecondsToUpdate)
            {
                return;
            }

            XElement taxaList = XElement.Load(this.Config.RankListXmlFilePath);

            foreach (var taxon in taxaList.Descendants(TaxonNodeName))
            {
                string name = taxon.Element(TaxonPartNodeName).Element(TaxonNameNodeName).Value;

                string[] ranks = taxon.Element(TaxonPartNodeName)
                    .Element(TaxonRanksNodeName)
                    .Elements(TaxonRankNodeName)
                    .Select(i => i.Value)
                    .ToArray();

                var taxonToAdd = new Taxon
                {
                    Name = name,
                    Ranks = ranks,
                    IsWhiteListed = !this.matchNonWhiteListedHigherTaxon.IsMatch(name)
                };

                await this.AddTaxon(taxonToAdd, update);
            }

            this.lastUpdated = DateTime.Now;
        }

        private async Task<int> WriteTaxaToFile()
        {
            await this.ReadTaxaFromFile(false);

            var taxa = this.TaxaRanks
                .Select(pair =>
                {
                    var ranks = pair.Value.Select(r => new XElement(TaxonRankNodeName, r)).ToArray();

                    XElement rank = new XElement(TaxonRanksNodeName, ranks);
                    XElement name = new XElement(TaxonNameNodeName, pair.Key);
                    XElement part = new XElement(TaxonPartNodeName, name, rank);

                    XAttribute whiteListed = new XAttribute(
                        WhiteListedAttributeName,
                        this.TaxaWhiteListed.GetOrAdd(pair.Key, false));

                    return new XElement(TaxonNodeName, whiteListed, part);
                })
                .ToArray();

            XElement taxaList = new XElement(RootNodeName, taxa);

            taxaList.Save(this.Config.RankListXmlFilePath, SaveOptions.DisableFormatting);

            return taxaList.Elements().Count();
        }
    }
}