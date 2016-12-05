namespace ProcessingTools.Bio.Biorepositories.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Models;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.Contracts;
    using ProcessingTools.Bio.Biorepositories.Services.Data.Contracts;
    using ProcessingTools.Bio.Biorepositories.Services.Data.Factories;
    using ProcessingTools.Bio.Biorepositories.Services.Data.Models;

    public class BiorepositoriesInstitutionsDataService : BiorepositoriesDataServiceFactory<Institution, BiorepositoriesInstitutionServiceModel>, IBiorepositoriesInstitutionsDataService
    {
        public BiorepositoriesInstitutionsDataService(IBiorepositoriesRepositoryProvider<Institution> repositoryProvider)
            : base(repositoryProvider)
        {
        }

        protected override Expression<Func<Institution, bool>> Filter => i => i.InstitutionCode.Length > 1 && i.InstitutionName.Length > 1;

        protected override Expression<Func<Institution, BiorepositoriesInstitutionServiceModel>> Project => i => new BiorepositoriesInstitutionServiceModel
        {
            InstitutionalCode = i.InstitutionCode,
            NameOfInstitution = i.InstitutionName,
            Url = i.Url
        };
    }
}
