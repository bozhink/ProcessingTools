using System;
using System.Linq.Expressions;
using ProcessingTools.Bio.Biorepositories.Data.Mongo.Models;
using ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.Contracts;
using ProcessingTools.Bio.Biorepositories.Services.Data.Contracts;
using ProcessingTools.Bio.Biorepositories.Services.Data.Factories;
using ProcessingTools.Bio.Biorepositories.Services.Data.Models;

namespace ProcessingTools.Bio.Biorepositories.Services.Data.Services
{
    public class BiorepositoriesInstitutionalCollectionsDataService : BiorepositoriesDataServiceFactory<Collection, BiorepositoriesCollectionServiceModel>, IBiorepositoriesInstitutionalCollectionsDataService
    {
        public BiorepositoriesInstitutionalCollectionsDataService(IBiorepositoriesRepositoryProvider<Collection> repositoryProvider)
            : base(repositoryProvider)
        {
        }

        protected override Expression<Func<Collection, bool>> Filter => c => c.CollectionCode.Length > 1 && c.CollectionName.Length > 1;

        protected override Expression<Func<Collection, BiorepositoriesCollectionServiceModel>> Project => c => new BiorepositoriesCollectionServiceModel
        {
            CollectionCode = c.CollectionCode,
            CollectionName = c.CollectionName,
            Url = c.Url
        };
    }
}
