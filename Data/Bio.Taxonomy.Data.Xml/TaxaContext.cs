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

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Bio.Taxonomy.Types;

    public class TaxaContext : ITaxaContext
    {
        public TaxaContext()
        {
            this.Taxa = new ConcurrentDictionary<string, ITaxonRankEntity>();
        }

        protected ConcurrentDictionary<string, ITaxonRankEntity> Taxa { get; private set; }

        private Func<TaxonXmlModel, ITaxonRankEntity> MapTaxonXmlModelToTaxonRankEntity => t =>
        {
            var ranks = t.Parts.FirstOrDefault().Ranks.Values
                .Where(r => !string.IsNullOrWhiteSpace(r))
                .Select(r => r.MapTaxonRankStringToTaxonRankType())
                .ToList();

            var taxon = new Taxon
            {
                Name = t.Parts.FirstOrDefault().Value,
                IsWhiteListed = t.IsWhiteListed,
                Ranks = new HashSet<TaxonRankType>(ranks)
            };

            return taxon;
        };

        private Func<ITaxonRankEntity, TaxonXmlModel> MapTaxonRankEntityToTaxonXmlModel => t => new TaxonXmlModel
        {
            IsWhiteListed = t.IsWhiteListed,
            Parts = new TaxonPartXmlModel[]
            {
                new TaxonPartXmlModel
                {
                    Value = t.Name,
                    Ranks = new TaxonRankXmlModel
                    {
                        Values = t.Ranks.Select(r => r.ToString().ToLower()).ToArray()
                    }
                }
            }
        };

        public Task<IQueryable<ITaxonRankEntity>> All() => Task.Run(() => new HashSet<ITaxonRankEntity>(this.Taxa.Values).AsQueryable());

        public Task<ITaxonRankEntity> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            ITaxonRankEntity taxon;
            this.Taxa.TryGetValue(id.ToString(), out taxon);
            return Task.FromResult(taxon);
        }

        public Task<object> Add(ITaxonRankEntity taxon) => Task.Run<object>(() => this.Upsert(taxon));

        public Task<object> Update(ITaxonRankEntity taxon) => Task.Run<object>(() => this.Upsert(taxon));

        public Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            ITaxonRankEntity taxon;
            this.Taxa.TryRemove(id.ToString(), out taxon);
            return Task.FromResult<object>(taxon);
        }

        public Task<long> LoadTaxa(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            return Task.Run(() =>
            {
                IEnumerable<ITaxonRankEntity> taxa;
                using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var serializer = new XmlSerializer(typeof(RankListXmlModel));
                    var result = (RankListXmlModel)serializer.Deserialize(stream);

                    taxa = result.Taxa.Select(this.MapTaxonXmlModelToTaxonRankEntity).ToList();
                }

                taxa.AsParallel().ForAll(taxon => this.Upsert(taxon));

                return taxa.LongCount();
            });
        }

        public async Task<long> WriteTaxa(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var taxa = this.Taxa.Values.Select(this.MapTaxonRankEntityToTaxonXmlModel).ToArray();

            var rankList = new RankListXmlModel
            {
                Taxa = taxa
            };

            using (var stream = new FileStream(fileName, FileMode.OpenOrCreate | FileMode.Truncate, FileAccess.Write))
            {
                var serializer = new XmlSerializer(typeof(RankListXmlModel));
                serializer.Serialize(stream, rankList);
                await stream.FlushAsync();
            }

            return rankList.Taxa.Length;
        }

        private ITaxonRankEntity Upsert(ITaxonRankEntity taxon)
        {
            if (taxon == null)
            {
                throw new ArgumentNullException(nameof(taxon));
            }

            Func<string, ITaxonRankEntity, ITaxonRankEntity> update = (k, t) =>
            {
                var ranks = taxon.Ranks.Concat(t.Ranks);

                var result = new Taxon
                {
                    IsWhiteListed = taxon.IsWhiteListed,
                    Name = taxon.Name,
                    Ranks = new HashSet<TaxonRankType>(ranks)
                };

                return result;
            };

            return this.Taxa.AddOrUpdate(taxon.Name, taxon, update);
        }
    }
}
