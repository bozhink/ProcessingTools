namespace ProcessingTools.Services.Data
{
    using AutoMapper;

    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Data.Models;
    using ProcessingTools.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class InstitutionsDataService : EfGenericCrudDataServiceFactory<Institution, IInstitution, int>, IInstitutionsDataService
    {
        private readonly IMapper mapper;

        public InstitutionsDataService(IDataRepository<Institution> repository)
            : base(repository, i => i.Name.Length)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<Institution, IInstitution>();
                c.CreateMap<IInstitution, Institution>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;
    }
}
