namespace ProcessingTools.Data.Bio.Taxonomy.Xml
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Data.Bio.Taxonomy.Xml.Contracts;
    using ProcessingTools.Data.Models.Bio.Taxonomy.Xml;
    using ProcessingTools.Extensions;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    public class XmlTaxaContext : IXmlTaxaContext
    {
        private readonly Regex matchHigherTaxa = new Regex(TaxaRegexPatterns.HigherTaxaMatchPattern);

        public XmlTaxaContext()
        {
            this.Taxa = new ConcurrentDictionary<string, ITaxonRankItem>();
        }

        public IQueryable<ITaxonRankItem> DataSet => new HashSet<ITaxonRankItem>(this.Taxa.Values).AsQueryable();

        protected ConcurrentDictionary<string, ITaxonRankItem> Taxa { get; private set; }

        private Func<ITaxonRankItem, TaxonXmlModel> MapTaxonRankEntityToTaxonXmlModel => t => new TaxonXmlModel
        {
            IsWhiteListed = !this.matchHigherTaxa.IsMatch(t.Name),
            Parts = new[]
            {
                new TaxonPartXmlModel
                {
                    Value = t.Name,
                    Ranks = new TaxonRankXmlModel
                    {
                        Values = t.Ranks.Select(r => r.MapTaxonRankTypeToTaxonRankString()).ToArray()
                    }
                }
            }
        };

        public Task<object> AddAsync(ITaxonRankItem entity) => Task.Run<object>(() => this.Upsert(entity));

        public Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.Taxa.TryRemove(id.ToString(), out ITaxonRankItem taxon);
            return Task.FromResult<object>(taxon);
        }

        public Task<ITaxonRankItem> GetAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.Taxa.TryGetValue(id.ToString(), out ITaxonRankItem taxon);
            return Task.FromResult(taxon);
        }

        public async Task<long> LoadFromFileAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            return await Task.Run(() =>
            {
                IEnumerable<ITaxonRankItem> taxa;
                using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var serializer = new XmlSerializer(typeof(RankListXmlModel));
                    var result = (RankListXmlModel)serializer.Deserialize(stream);

                    taxa = result.Taxa.Select(this.MapTaxonXmlModelToTaxonRankEntity).ToList();
                }

                taxa.AsParallel().ForAll(taxon => this.Upsert(taxon));

                return taxa.LongCount();
            })
            .ConfigureAwait(false);
        }

        public Task<object> UpdateAsync(ITaxonRankItem entity) => Task.Run<object>(() => this.Upsert(entity));

        public async Task<long> WriteToFileAsync(string fileName)
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

            using (var stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                var serializer = new XmlSerializer(typeof(RankListXmlModel));
                serializer.Serialize(stream, rankList);
                await stream.FlushAsync().ConfigureAwait(false);
            }

            return rankList.Taxa.Length;
        }

        private ITaxonRankItem Upsert(ITaxonRankItem taxon)
        {
            if (taxon == null)
            {
                throw new ArgumentNullException(nameof(taxon));
            }

            ITaxonRankItem update(string k, ITaxonRankItem t)
            {
                var ranks = taxon.Ranks.Concat(t.Ranks);

                var result = new Taxon
                {
                    IsWhiteListed = taxon.IsWhiteListed,
                    Name = taxon.Name,
                    Ranks = new HashSet<TaxonRankType>(ranks)
                };

                return result;
            }

            return this.Taxa.AddOrUpdate(taxon.Name, taxon, update);
        }

        private ITaxonRankItem MapTaxonXmlModelToTaxonRankEntity(TaxonXmlModel taxon)
        {
            var firstPart = taxon.Parts.FirstOrDefault();

            var ranks = firstPart.Ranks.Values
                .Where(r => !string.IsNullOrWhiteSpace(r))
                .Select(r => r.MapTaxonRankStringToTaxonRankType())
                .ToArray();

            var taxonName = firstPart.Value;

            return new Taxon
            {
                Name = taxonName,
                IsWhiteListed = !this.matchHigherTaxa.IsMatch(taxonName),
                Ranks = new HashSet<TaxonRankType>(ranks)
            };
        }
    }
}
