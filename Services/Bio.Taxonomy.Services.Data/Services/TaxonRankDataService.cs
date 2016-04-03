namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Data.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class TaxonRankDataService : RepositoryDataServiceAbstractFactory<Taxon, TaxonRankServiceModel>, ITaxonRankDataService
    {
        public TaxonRankDataService(ITaxaRepository repository)
            : base(repository)
        {
        }

        protected override IEnumerable<Taxon> MapServiceModelToDbModel(params TaxonRankServiceModel[] models)
        {
            if (models == null)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var result = new HashSet<Taxon>();
            foreach (var model in models)
            {
                var entity = new Taxon
                {
                    Name = model.ScientificName,
                    IsWhiteListed = model.IsWhiteListed,
                    Ranks = new string[] { model.Rank }
                };

                result.Add(entity);
            }

            return result;
        }

        protected override IEnumerable<TaxonRankServiceModel> MapDbModelToServiceModel(params Taxon[] entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            var result = new List<TaxonRankServiceModel>();
            foreach (var entity in entities)
            {
                var models = entity.Ranks.Select(rank => new TaxonRankServiceModel
                {
                    IsWhiteListed = entity.IsWhiteListed,
                    ScientificName = entity.Name,
                    Rank = rank
                });

                result.AddRange(models);
            }

            return result;
        }
    }
}