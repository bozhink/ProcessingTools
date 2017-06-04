namespace ProcessingTools.Bio.Taxonomy.Data.Xml
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Models;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Data.Bio.Taxonomy.Models;
    using ProcessingTools.Enumerations;

    public class XmlTaxaContext : IXmlTaxaContext
    {
        private readonly Regex matchHigherTaxa = new Regex(TaxaRegexPatterns.HigherTaxaMatchPattern);

        public XmlTaxaContext()
        {
            this.Taxa = new ConcurrentDictionary<string, ITaxonRankEntity>();
        }

        public IQueryable<ITaxonRankEntity> DataSet => new HashSet<ITaxonRankEntity>(this.Taxa.Values).AsQueryable();

        protected ConcurrentDictionary<string, ITaxonRankEntity> Taxa { get; private set; }

        private Func<ITaxonRankEntity, TaxonXmlModel> MapTaxonRankEntityToTaxonXmlModel => t => new TaxonXmlModel
        {
            IsWhiteListed = !this.matchHigherTaxa.IsMatch(t.Name),
            Parts = new TaxonPartXmlModel[]
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

        private Func<TaxonXmlModel, ITaxonRankEntity> MapTaxonXmlModelToTaxonRankEntity => t =>
        {
            var firstPart = t.Parts.FirstOrDefault();

            var ranks = firstPart.Ranks.Values
                .Where(r => !string.IsNullOrWhiteSpace(r))
                .Select(r => r.MapTaxonRankStringToTaxonRankType())
                .ToArray();

            var taxonName = firstPart.Value;

            var taxon = new Taxon
            {
                Name = taxonName,
                IsWhiteListed = !this.matchHigherTaxa.IsMatch(taxonName),
                Ranks = new HashSet<TaxonRankType>(ranks)
            };

            return taxon;
        };

        public Task<object> Add(ITaxonRankEntity taxon) => Task.Run<object>(() => this.Upsert(taxon));

        public Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.Taxa.TryRemove(id.ToString(), out ITaxonRankEntity taxon);
            return Task.FromResult<object>(taxon);
        }

        public Task<ITaxonRankEntity> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.Taxa.TryGetValue(id.ToString(), out ITaxonRankEntity taxon);
            return Task.FromResult(taxon);
        }

        public Task<long> LoadFromFile(string fileName) => Task.Run(() =>
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

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

        public Task<object> Update(ITaxonRankEntity taxon) => Task.Run<object>(() => this.Upsert(taxon));

        public async Task<long> WriteToFile(string fileName)
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
