namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Constants;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class TaxonRankDataService : MultiDataServiceWithRepositoryProviderFactory<Taxon, ITaxonRankWithWhiteListing>, ITaxonRankDataService
    {
        private Regex matchNonWhiteListedHigherTaxon = new Regex(TaxaRegexPatterns.HigherTaxaMatchPattern);

        public TaxonRankDataService(IXmlTaxonRankRepositoryProvider repositoryProvider)
            : base(repositoryProvider)
        {
        }

        protected override Expression<Func<Taxon, IEnumerable<ITaxonRankWithWhiteListing>>> MapDbModelToServiceModel => e => e.Ranks.Select(rank => new TaxonRankWithWhiteListingServiceModel
        {
            IsWhiteListed = e.IsWhiteListed,
            ScientificName = e.Name,
            Rank = rank
        });

        protected override Expression<Func<ITaxonRankWithWhiteListing, IEnumerable<Taxon>>> MapServiceModelToDbModel => m => new Taxon[]
        {
            new Taxon
            {
                Name = m.ScientificName,
                IsWhiteListed = m.IsWhiteListed,
                Ranks = new string[] { m.Rank }
            }
        };

        protected override Expression<Func<Taxon, object>> SortExpression => t => t.Name;

        public override Task<int> Add(params ITaxonRankWithWhiteListing[] models)
        {
            if (models == null)
            {
                throw new ArgumentNullException(nameof(models));
            }

            foreach (var model in models)
            {
                model.IsWhiteListed = !this.matchNonWhiteListedHigherTaxon.IsMatch(model.ScientificName);
            }

            return base.Add(models);
        }

        public override Task<int> Update(params ITaxonRankWithWhiteListing[] models)
        {
            if (models == null)
            {
                throw new ArgumentNullException(nameof(models));
            }

            foreach (var model in models)
            {
                model.IsWhiteListed = !this.matchNonWhiteListedHigherTaxon.IsMatch(model.ScientificName);
            }

            return base.Update(models);
        }
    }
}
