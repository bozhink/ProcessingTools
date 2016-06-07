namespace ProcessingTools.Bio.Taxonomy.Data.Xml
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    using Contracts;
    using Models;

    public class TaxaContext : ITaxaContext
    {
        public TaxaContext()
        {
            this.Taxa = new ConcurrentDictionary<string, Taxon>();
        }

        protected ConcurrentDictionary<string, Taxon> Taxa { get; set; }

        public Task<IQueryable<Taxon>> All()
        {
            return Task.FromResult(new HashSet<Taxon>(this.Taxa.Values).AsQueryable());
        }

        public Task<Taxon> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Taxon taxon;
            this.Taxa.TryGetValue(id.ToString(), out taxon);
            return Task.FromResult(taxon);
        }

        public Task<object> Add(Taxon taxon) => Task.Run<object>(() => this.Upsert(taxon));

        public Task<object> Update(Taxon taxon) => Task.Run<object>(() => this.Upsert(taxon));

        public Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Taxon taxon;
            this.Taxa.TryRemove(id.ToString(), out taxon);
            return Task.FromResult<object>(taxon);
        }

        public Task<int> LoadTaxa(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            return Task.Run(() =>
            {
                IEnumerable<Taxon> taxa = new HashSet<Taxon>();
                using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var serializer = new XmlSerializer(typeof(RankListXmlModel));
                    var result = (RankListXmlModel)serializer.Deserialize(stream);

                    taxa = result.Taxa.Select(t => new Taxon
                    {
                        Name = t.Parts.FirstOrDefault().Value,
                        Ranks = new HashSet<string>(t.Parts.FirstOrDefault().Ranks.Values),
                        IsWhiteListed = t.IsWhiteListed
                    });
                }

                taxa.AsParallel().ForAll(taxon => this.Upsert(taxon));

                return taxa.Count();
            });
        }

        public async Task<int> WriteTaxa(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var taxa = this.Taxa.Values.Select(t => new TaxonXmlModel
            {
                IsWhiteListed = t.IsWhiteListed,
                Parts = new TaxonPartXmlModel[]
                    {
                        new TaxonPartXmlModel
                        {
                            Value = t.Name,
                            Ranks = new TaxonRankXmlModel
                            {
                                Values = t.Ranks.ToArray()
                            }
                        }
                    }
            });

            var rankList = new RankListXmlModel
            {
                Taxa = taxa.ToArray()
            };

            using (var stream = new FileStream(fileName, FileMode.OpenOrCreate | FileMode.Truncate, FileAccess.Write))
            {
                var serializer = new XmlSerializer(typeof(RankListXmlModel));
                serializer.Serialize(stream, rankList);
                await stream.FlushAsync();
            }

            return rankList.Taxa.Length;
        }

        private Taxon Upsert(Taxon taxon)
        {
            if (taxon == null)
            {
                throw new ArgumentNullException(nameof(taxon));
            }

            Func<string, Taxon, Taxon> update = (k, t) =>
            {
                var result = new Taxon
                {
                    IsWhiteListed = taxon.IsWhiteListed,
                    Name = taxon.Name
                };

                result.Ranks = new HashSet<string>(taxon.Ranks.Concat(t.Ranks));

                return result;
            };

            return this.Taxa.AddOrUpdate(taxon.Name, taxon, update);
        }
    }
}
