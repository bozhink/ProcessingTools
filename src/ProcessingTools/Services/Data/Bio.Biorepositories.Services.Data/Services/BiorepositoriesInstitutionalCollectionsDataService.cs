﻿namespace ProcessingTools.Bio.Biorepositories.Services.Data.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts.Repositories;
    using ProcessingTools.Bio.Biorepositories.Services.Data.Contracts;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Constants;
    using ProcessingTools.Exceptions;

    public class BiorepositoriesInstitutionalCollectionsDataService : IBiorepositoriesInstitutionalCollectionsDataService
    {
        private readonly IBiorepositoriesRepositoryProvider<Biorepositories.Data.Mongo.Models.Collection> repositoryProvider;

        public BiorepositoriesInstitutionalCollectionsDataService(IBiorepositoriesRepositoryProvider<Biorepositories.Data.Mongo.Models.Collection> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        public Task<Models.Collection[]> GetAsync(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (take < 1 || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            return Task.Run(() =>
            {
                var repository = this.repositoryProvider.Create();

                var data = repository.Query
                    .Where(c => c.CollectionCode.Length > 1 && c.CollectionName.Length > 1)
                    .OrderBy(i => i.Id)
                    .Skip(skip)
                    .Take(take)
                    .Select(c => new Models.Collection
                    {
                        Code = c.CollectionCode,
                        Name = c.CollectionName,
                        Url = c.Url
                    })
                    .ToArray();

                repository.TryDispose();

                return data;
            });
        }
    }
}
