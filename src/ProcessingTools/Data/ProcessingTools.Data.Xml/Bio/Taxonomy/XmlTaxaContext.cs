// <copyright file="XmlTaxaContext.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Xml.Bio.Taxonomy
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
    using ProcessingTools.Data.Models.Xml.Bio.Taxonomy;
    using ProcessingTools.Extensions;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// XML taxa context.
    /// </summary>
    public class XmlTaxaContext : IXmlTaxaContext
    {
        private readonly Regex matchHigherTaxa = new Regex(TaxaRegexPatterns.HigherTaxaMatchPattern);

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlTaxaContext"/> class.
        /// </summary>
        public XmlTaxaContext()
        {
            this.Taxa = new ConcurrentDictionary<string, ITaxonRankItem>();
        }

        /// <inheritdoc/>
        public IEnumerable<ITaxonRankItem> DataSet => new HashSet<ITaxonRankItem>(this.Taxa.Values);

        private ConcurrentDictionary<string, ITaxonRankItem> Taxa { get; }

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

        /// <inheritdoc/>
        public object Upsert(ITaxonRankItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            ITaxonRankItem update(string k, ITaxonRankItem t)
            {
                var ranks = entity.Ranks.Concat(t.Ranks);

                var result = new Taxon
                {
                    IsWhiteListed = entity.IsWhiteListed,
                    Name = entity.Name,
                    Ranks = new HashSet<TaxonRankType>(ranks)
                };

                return result;
            }

            return this.Taxa.AddOrUpdate(entity.Name, entity, update);
        }

        /// <inheritdoc/>
        public object Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.Taxa.TryRemove(id.ToString(), out ITaxonRankItem taxon);
            return taxon;
        }

        /// <inheritdoc/>
        public ITaxonRankItem Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.Taxa.TryGetValue(id.ToString(), out ITaxonRankItem taxon);
            return taxon;
        }

        /// <inheritdoc/>
        public long LoadFromFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            IList<ITaxonRankItem> taxa;
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var serializer = new XmlSerializer(typeof(RankListXmlModel));
                var result = (RankListXmlModel)serializer.Deserialize(stream);

                taxa = result.Taxa.Select(this.MapTaxonXmlModelToTaxonRankEntity).ToList();
            }

            taxa.AsParallel().ForAll(taxon => this.Upsert(taxon));

            return taxa.LongCount();
        }

        /// <inheritdoc/>
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
