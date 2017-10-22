namespace ProcessingTools.Bio.Biorepositories.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts.Repositories;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Models;
    using ProcessingTools.Bio.Biorepositories.Services.Data.Contracts;
    using ProcessingTools.Bio.Biorepositories.Services.Data.Factories;

    public class BiorepositoriesPersonalCollectionsDataService : BiorepositoriesDataServiceFactory<CollectionPer, Models.Collection>, IBiorepositoriesPersonalCollectionsDataService
    {
        public BiorepositoriesPersonalCollectionsDataService(IBiorepositoriesRepositoryProvider<CollectionPer> repositoryProvider)
            : base(repositoryProvider)
        {
        }

        protected override Expression<Func<CollectionPer, bool>> Filter => c => c.CollectionCode.Length > 1 && c.CollectionName.Length > 1;

        protected override Expression<Func<CollectionPer, Models.Collection>> Project => c => new Models.Collection
        {
            Code = c.CollectionCode,
            Name = c.CollectionName,
            Url = c.Url
        };
    }
}
