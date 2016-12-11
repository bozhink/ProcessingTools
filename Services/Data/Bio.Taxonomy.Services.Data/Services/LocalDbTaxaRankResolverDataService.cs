﻿namespace ProcessingTools.Bio.Taxonomy.Services.Data.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Models;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Extensions;

    public class LocalDbTaxaRankResolverDataService : ILocalDbTaxaRankResolverDataService
    {
        private readonly ITaxonRankSearchableRepositoryProvider repositoryProvider;

        public LocalDbTaxaRankResolverDataService(ITaxonRankSearchableRepositoryProvider repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
        }

        public async Task<IQueryable<ITaxonRank>> Resolve(params string[] scientificNames)
        {
            if (scientificNames == null || scientificNames.Length < 1)
            {
                throw new ArgumentNullException(nameof(scientificNames));
            }

            var names = scientificNames.Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.ToLower())
                .Distinct()
                .ToList();

            var result = new ConcurrentQueue<ITaxonRank>();

            {
                var repository = this.repositoryProvider.Create();

                var tasks = new ConcurrentQueue<Task>();

                foreach (var name in names)
                {
                    tasks.Enqueue(this.FindRankForSingleTaxon(repository, name, result));
                }

                await Task.WhenAll(tasks);

                repository.TryDispose();
            }

            return new HashSet<ITaxonRank>(result).AsQueryable();
        }

        // TODO: EntityNotFoundException: Re-thick catching of this exception
        private async Task FindRankForSingleTaxon(ISearchableRepository<ITaxonRankEntity> repository, string name, ConcurrentQueue<ITaxonRank> outputCollection)
        {
            try
            {
                var entity = await repository.FindFirst(t => t.Name.ToLower() == name);

                if (entity == null)
                {
                    throw new EntityNotFoundException();
                }

                foreach (var rank in entity.Ranks)
                {
                    var taxon = new TaxonRankServiceModel
                    {
                        ScientificName = entity.Name,
                        Rank = rank
                    };

                    outputCollection.Enqueue(taxon);
                }
            }
            catch (EntityNotFoundException)
            {
            }
        }
    }
}
