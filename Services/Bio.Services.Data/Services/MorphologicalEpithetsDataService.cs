namespace ProcessingTools.Bio.Services.Data
{
    using AutoMapper;

    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Bio.Data.Models;
    using ProcessingTools.Bio.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class MorphologicalEpithetsDataService : EfGenericCrudDataServiceFactory<MorphologicalEpithet, IMorphologicalEpithet, int>, IMorphologicalEpithetsDataService
    {
        private readonly IMapper mapper;

        public MorphologicalEpithetsDataService(IBioDataRepository<MorphologicalEpithet> repository)
            : base(repository, e => e.Name.Length)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<MorphologicalEpithet, IMorphologicalEpithet>();
                c.CreateMap<IMorphologicalEpithet, MorphologicalEpithet>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;
    }
}