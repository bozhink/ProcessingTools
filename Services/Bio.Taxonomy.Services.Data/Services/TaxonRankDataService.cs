namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Contracts;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Data.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class TaxonRankDataService : GenericRepositoryDataServiceFactory<Taxon, TaxonRankServiceModel>, ITaxonRankDataService
    {
        public TaxonRankDataService(ITaxaRepository repository)
            : base(repository)
        {
        }

        protected override Expression<Func<Taxon, IEnumerable<TaxonRankServiceModel>>> MapDbModelToServiceModel => e => e.Ranks.Select(rank => new TaxonRankServiceModel
        {
            IsWhiteListed = e.IsWhiteListed,
            ScientificName = e.Name,
            Rank = rank
        });

        protected override Expression<Func<TaxonRankServiceModel, IEnumerable<Taxon>>> MapServiceModelToDbModel => m => new Taxon[]
        {
            new Taxon
            {
                Name = m.ScientificName,
                IsWhiteListed = m.IsWhiteListed,
                Ranks = new string[] { m.Rank }
            }
        };

        protected override Expression<Func<Taxon, object>> SortExpression => t => t.Name;
    }
}