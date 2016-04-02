namespace ProcessingTools.Bio.Taxonomy.Data.Repositories
{
    using System;
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
            this.Taxa = new HashSet<Taxon>();
        }

        private Config Config { get; set; }

        private ICollection<Taxon> Taxa { get; set; }

        public async Task Add(Taxon entity)
        {
            await this.Add(entity.Name, entity.Ranks.ToArray());
        }

        public async Task<IQueryable<Taxon>> All()
        {
            await this.ReadTaxaFromFile();
            return this.Taxa.AsQueryable();
        }

        public async Task<IQueryable<Taxon>> All(int skip, int take)
        {
            await this.ReadTaxaFromFile();
            return this.Taxa
                .OrderBy(t => t.Name)
                .Skip(skip)
                .Take(take)
                .AsQueryable();
        }

        public async Task Delete(object id)
        {
            await this.ReadTaxaFromFile();
            try
            {
                var taxon = this.Taxa.First(t => t.Name == id.ToString());
                this.Taxa.Remove(taxon);
            }
            catch
            {
            }
        }

        public async Task Delete(Taxon entity)
        {
            await this.ReadTaxaFromFile();
            try
            {
                this.Taxa.Remove(entity);
            }
            catch
            {
            }
        }

        public async Task<Taxon> Get(object id)
        {
            await this.ReadTaxaFromFile();
            var taxon = this.Taxa.FirstOrDefault(t => t.Name == id.ToString());
            return taxon;
        }

        public Task<int> SaveChanges()
        {
            return WriteTaxaToFile();
        }

        public async Task Update(Taxon entity)
        {
            await this.ReadTaxaFromFile();
            await this.Delete(entity.Name);
            await this.Add(entity);
        }

        private Task Add(string name, params string[] ranks)
        {
            return Task.Run(() =>
            {
                var taxon = new Taxon
                {
                    Name = name
                };

                try
                {
                    foreach (var item in this.Taxa.Where(t => t.Name == taxon.Name))
                    {
                        foreach (var rank in item.Ranks)
                        {
                            taxon.Ranks.Add(rank);
                        }

                        this.Taxa.Remove(item);
                    }
                }
                catch
                {
                }

                foreach (var rank in ranks)
                {
                    taxon.Ranks.Add(rank);
                }

                this.Taxa.Add(taxon);
            });
        }

        private async Task ReadTaxaFromFile()
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

                await this.Add(name, ranks);
            }
        }

        private async Task<int> WriteTaxaToFile()
        {
            await this.ReadTaxaFromFile();

            var taxa = this.Taxa
                .Select(taxon =>
                {
                    var ranks = taxon.Ranks.Select(r => new XElement("value", r)).ToArray();

                    XElement rank = new XElement("rank", ranks);
                    XElement name = new XElement("value", taxon.Name);
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