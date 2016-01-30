namespace ProcessingTools.Bio.Services.Data.Services
{
    using AutoMapper;

    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Bio.Data.Models;
    using ProcessingTools.Bio.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class MorphologicalEpithetsDataService : EfGenericCrudDataServiceFactory<MorphologicalEpithet, IMorphologicalEpithet, int>, IMorphologicalEpithetsDataService
    {
        public MorphologicalEpithetsDataService(IBioDataRepository<MorphologicalEpithet> repository)
            : base(repository, e => e.Name.Length)
        {
            Mapper.CreateMap<MorphologicalEpithet, IMorphologicalEpithet>();
            Mapper.CreateMap<IMorphologicalEpithet, MorphologicalEpithet>();
        }
    }
}