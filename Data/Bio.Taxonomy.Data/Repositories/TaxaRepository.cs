namespace ProcessingTools.Bio.Taxonomy.Data.Repositories
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using Contracts;
    using Models;

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

            this.ReadTaxaFromFile(true);
        }

        private Config Config { get; set; }

        private ConcurrentDictionary<string, bool> TaxaWhiteListed { get; set; }

        private ConcurrentDictionary<string, HashSet<string>> TaxaRanks { get; set; }

        public Task Add(Taxon entity)
        {
            return Task.Run(() => this.AddTaxon(entity, true));
        }

        public Task<IQueryable<Taxon>> All()
        {
            return Task.Run(() =>
            {
                this.ReadTaxaFromFile(false);

                return this.TaxaRanks
                    .Select(t => new Taxon
                    {
                        Name = t.Key,
                        Ranks = t.Value,
                        IsWhiteListed = this.TaxaWhiteListed.GetOrAdd(t.Key, false)
                    })
                    .AsQueryable();
            });
        }

        public async Task<IQueryable<Taxon>> All(int skip, int take)
        {
            return (await this.All())
                .OrderBy(t => t.Name)
                .Skip(skip)
                .Take(take);
        }

        public Task Delete(object id)
        {
            return Task.Run(() =>
            {
                this.ReadTaxaFromFile(false);
                var value = new HashSet<string>();
                this.TaxaRanks.TryRemove(id.ToString(), out value);
            });
        }

        public Task Delete(Taxon entity)
        {
            return this.Delete(entity.Name);
        }

        public async Task<Taxon> Get(object id)
        {
            return (await this.All())
                .FirstOrDefault(t => t.Name == id.ToString());
        }

        public Task<int> SaveChanges()
        {
            return Task.FromResult(this.WriteTaxaToFile());
        }

        public Task Update(Taxon entity)
        {
            return Task.Run(() =>
            {
                this.ReadTaxaFromFile(false);

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
            });
        }

        private void AddTaxon(Taxon taxon, bool update)
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
        }

        private void ReadTaxaFromFile(bool update)
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
                    IsWhiteListed = false
                };

                string whiteListedAttribute = taxon.Attribute(WhiteListedAttributeName)?.Value;

                bool whiteListed;
                if (bool.TryParse(whiteListedAttribute, out whiteListed))
                {
                    taxonToAdd.IsWhiteListed = whiteListed;
                }

                this.AddTaxon(taxonToAdd, update);
            }

            this.lastUpdated = DateTime.Now;
        }

        private int WriteTaxaToFile()
        {
            this.ReadTaxaFromFile(false);

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