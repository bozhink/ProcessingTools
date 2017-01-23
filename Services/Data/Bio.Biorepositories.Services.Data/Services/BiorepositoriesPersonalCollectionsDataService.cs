namespace ProcessingTools.Bio.Biorepositories.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;

    using Contracts;
    using Factories;
    using Models;

    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts.Repositories;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Models;

    public class BiorepositoriesPersonalCollectionsDataService : BiorepositoriesDataServiceFactory<CollectionPer, BiorepositoriesCollectionServiceModel>, IBiorepositoriesPersonalCollectionsDataService
    {
        public BiorepositoriesPersonalCollectionsDataService(IBiorepositoriesRepositoryProvider<CollectionPer> repositoryProvider)
            : base(repositoryProvider)
        {
        }

        protected override Expression<Func<CollectionPer, bool>> Filter => c => c.CollectionCode.Length > 1 && c.CollectionName.Length > 1;

        protected override Expression<Func<CollectionPer, BiorepositoriesCollectionServiceModel>> Project => c => new BiorepositoriesCollectionServiceModel
        {
            CollectionCode = c.CollectionCode,
            CollectionName = c.CollectionName,
            Url = c.Url
        };
    }
}
