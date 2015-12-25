namespace ProcessingTools.Services.Data
{
    using AutoMapper;
    using Common.Factories;
    using Contracts;
    using Models.Contracts;
    using ProcessingTools.Data.Models;
    using ProcessingTools.Data.Repositories;

    public class InstitutionsDataService : EfGenericCrudDataServiceFactory<Institution, IInstitution, int>, IInstitutionsDataService
    {
        public InstitutionsDataService(IDataRepository<Institution> repository)
            : base(repository, i => i.Name.Length)
        {
            Mapper.CreateMap<Institution, IInstitution>();
            Mapper.CreateMap<IInstitution, Institution>();
        }
    }
}