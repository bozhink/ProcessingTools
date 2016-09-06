namespace ProcessingTools.Bio.Biorepositories.Services.Data
{
    using System;
    using System.Linq.Expressions;

    using Contracts;
    using Factories;
    using Models;

    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Models;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.Contracts;

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
