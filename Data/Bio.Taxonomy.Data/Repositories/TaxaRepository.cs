namespace ProcessingTools.Bio.Taxonomy.Data.Repositories
{
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
        public TaxaRepository()
        {
            this.Config = ConfigBuilder.Create();
            this.Taxa = new ConcurrentDictionary<string, HashSet<string>>();
        }

        private Config Config { get; set; }

        private ConcurrentDictionary<string, HashSet<string>> Taxa { get; set; }

        public Task Add(Taxon entity)
        {
            return Task.Run(() => this.Add(entity.Name, entity.Ranks.ToArray()));
        }

        public Task<IQueryable<Taxon>> All()
        {
            return Task.Run(() =>
            {
                this.ReadTaxaFromFile();

                return this.Taxa
                    .Select(t => new Taxon
                    {
                        Name = t.Key,
                        Ranks = t.Value
                    })
                    .AsQueryable();
            });
        }

        public Task<IQueryable<Taxon>> All(int skip, int take)
        {
            return Task.Run(() =>
            {
                this.ReadTaxaFromFile();
                return this.Taxa
                    .OrderBy(t => t.Key)
                    .Skip(skip)
                    .Take(take)
                    .Select(t => new Taxon
                    {
                        Name = t.Key,
                        Ranks = t.Value
                    })
                    .AsQueryable();
            });
        }

        public Task Delete(object id)
        {
            return Task.Run(() =>
            {
                this.ReadTaxaFromFile();
                var value = new HashSet<string>();
                this.Taxa.TryRemove(id.ToString(), out value);
            });
        }

        public Task Delete(Taxon entity)
        {
            return this.Delete(entity.Name);
        }

        public Task<Taxon> Get(object id)
        {
            return Task.Run(() =>
            {
                this.ReadTaxaFromFile();
                var taxon = this.Taxa
                    .Where(t => t.Key == id.ToString())
                    .Select(t => new Taxon
                    {
                        Name = t.Key,
                        Ranks = t.Value
                    })
                    .FirstOrDefault();
                return taxon;
            });
        }

        public Task<int> SaveChanges()
        {
            return Task.FromResult(this.WriteTaxaToFile());
        }

        public Task Update(Taxon entity)
        {
            return Task.Run(() =>
            {
                this.ReadTaxaFromFile();

                var ranks = new HashSet<string>(entity.Ranks);
                if (this.Taxa.ContainsKey(entity.Name))
                {
                    this.Taxa[entity.Name] = ranks;
                }
                else
                {
                    this.Taxa.GetOrAdd(entity.Name, ranks);
                }
            });
        }

        private void Add(string name, params string[] ranks)
        {
            var ranksToAdd = new HashSet<string>(ranks.Where(r => !string.IsNullOrWhiteSpace(r)));
            if (this.Taxa.ContainsKey(name))
            {
                foreach (var rank in ranksToAdd)
                {
                    this.Taxa[name].Add(rank);
                }
            }
            else
            {
                this.Taxa.GetOrAdd(name, ranksToAdd);
            }
        }

        private void ReadTaxaFromFile()
        {
            XElement taxaList = XElement.Load(this.Config.RankListXmlFilePath);

            foreach (var taxon in taxaList.Descendants("taxon"))
            {
                string name = taxon.Element("part").Element("value").Value;

                string[] ranks = taxon.Element("part")
                    .Element("rank")
                    .Elements("value")
                    .Select(i => i.Value)
                    .ToArray();

                this.Add(name, ranks);
            }
        }

        private int WriteTaxaToFile()
        {
            this.ReadTaxaFromFile();

            var taxa = this.Taxa
                .Select(pair =>
                {
                    var ranks = pair.Value.Select(r => new XElement("value", r)).ToArray();

                    XElement rank = new XElement("rank", ranks);
                    XElement name = new XElement("value", pair.Key);
                    XElement part = new XElement("part", name, rank);

                    return part;
                })
                .ToArray();

            XElement taxaList = new XElement("taxa", taxa);

            taxaList.Save(this.Config.RankListXmlFilePath);

            return taxaList.Elements().Count();
        }
    }
}