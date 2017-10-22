namespace ProcessingTools.Bio.Biorepositories.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts.Repositories;
    using ProcessingTools.Bio.Biorepositories.Services.Data.Contracts;
    using ProcessingTools.Bio.Biorepositories.Services.Data.Factories;

    public class BiorepositoriesInstitutionalCollectionsDataService : BiorepositoriesDataServiceFactory<Biorepositories.Data.Mongo.Models.Collection, Models.Collection>, IBiorepositoriesInstitutionalCollectionsDataService
    {
        public BiorepositoriesInstitutionalCollectionsDataService(IBiorepositoriesRepositoryProvider<Biorepositories.Data.Mongo.Models.Collection> repositoryProvider)
            : base(repositoryProvider)
        {
        }

        protected override Expression<Func<Biorepositories.Data.Mongo.Models.Collection, bool>> Filter => c => c.CollectionCode.Length > 1 && c.CollectionName.Length > 1;

        protected override Expression<Func<Biorepositories.Data.Mongo.Models.Collection, Models.Collection>> Project => c => new Models.Collection
        {
            Code = c.CollectionCode,
            Name = c.CollectionName,
            Url = c.Url
        };
    }
}
