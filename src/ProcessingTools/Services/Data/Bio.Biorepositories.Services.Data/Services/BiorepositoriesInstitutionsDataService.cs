namespace ProcessingTools.Bio.Biorepositories.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts.Repositories;
    using ProcessingTools.Bio.Biorepositories.Services.Data.Contracts;
    using ProcessingTools.Bio.Biorepositories.Services.Data.Factories;

    public class BiorepositoriesInstitutionsDataService : BiorepositoriesDataServiceFactory<Biorepositories.Data.Mongo.Models.Institution, Models.Institution>, IBiorepositoriesInstitutionsDataService
    {
        public BiorepositoriesInstitutionsDataService(IBiorepositoriesRepositoryProvider<Biorepositories.Data.Mongo.Models.Institution> repositoryProvider)
            : base(repositoryProvider)
        {
        }

        protected override Expression<Func<Biorepositories.Data.Mongo.Models.Institution, bool>> Filter => i => i.InstitutionCode.Length > 1 && i.InstitutionName.Length > 1;

        protected override Expression<Func<Biorepositories.Data.Mongo.Models.Institution, Models.Institution>> Project => i => new Models.Institution
        {
            Code = i.InstitutionCode,
            Name = i.InstitutionName,
            Url = i.Url
        };
    }
}
