namespace ProcessingTools.Bio.Services.Data
{
    using AutoMapper;
    using Bio.Data.Models;
    using Bio.Data.Repositories;
    using Contracts;
    using Models.Contracts;
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