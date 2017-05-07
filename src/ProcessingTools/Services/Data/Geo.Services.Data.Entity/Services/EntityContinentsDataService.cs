namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using System.Data.Entity;
    using System.Linq;
    using AutoMapper;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Data.Geo.Filters;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;
    using ProcessingTools.Geo.Data.Entity.Contracts;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Geo.Services.Data.Entity.Abstractions;
    using ProcessingTools.Geo.Services.Data.Entity.Contracts.Services;

    public class EntityContinentsDataService : AbstractGeoSynonymisableDataService<Continent, IContinent, IContinentsFilter, ContinentSynonym, IContinentSynonym, IContinentSynonymsFilter>, IEntityContinentsDataService
    {
        private readonly IMapper mapper;

        public EntityContinentsDataService(IGeoRepository<Continent> repository, IGeoRepository<ContinentSynonym> synonymRepository, IEnvironment environment)
            : base(repository, synonymRepository, environment)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<Continent, Continent>()
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore());

                c.CreateMap<ContinentSynonym, ContinentSynonym>()
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore());

                c.CreateMap<IContinent, Continent>()
                    .ForMember(d => d.Countries, o => o.Ignore())
                    .ForMember(d => d.Synonyms, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore())
                    .ForMember(d => d.ModifiedBy, o => o.Ignore())
                    .ForMember(d => d.ModifiedOn, o => o.Ignore());

                c.CreateMap<IContinentSynonym, ContinentSynonym>()
                    .ForMember(d => d.Continent, o => o.Ignore())
                    .ForMember(d => d.ContinentId, o => o.ResolveUsing(x => x.ParentId))
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore())
                    .ForMember(d => d.ModifiedBy, o => o.Ignore())
                    .ForMember(d => d.ModifiedOn, o => o.Ignore());

                c.CreateMap<Continent, IContinent>()
                    .ConstructUsing(m => new ProcessingTools.Geo.Services.Data.Entity.Models.Continent
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Synonyms = m.Synonyms
                            .Select(s => new ProcessingTools.Geo.Services.Data.Entity.Models.ContinentSynonym
                            {
                                Id = s.Id,
                                Name = s.Name,
                                LanguageCode = s.LanguageCode,
                                ParentId = m.Id
                            })
                            .ToList<IContinentSynonym>()
                    });

                c.CreateMap<ContinentSynonym, IContinentSynonym>()
                    .ConstructUsing(s => new ProcessingTools.Geo.Services.Data.Entity.Models.ContinentSynonym
                    {
                        Id = s.Id,
                        Name = s.Name,
                        LanguageCode = s.LanguageCode,
                        ParentId = s.ContinentId
                    });
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;

        protected override IQueryable<Continent> GetQuery(IContinentsFilter filter)
        {
            {
                var query = this.Repository.Queryable()
                    .Include(c => c.Synonyms);

                if (filter != null)
                {
                    query = query.Where(
                        c =>
                             (!filter.Id.HasValue || c.Id == filter.Id) &&
                             (string.IsNullOrEmpty(filter.Name) || c.Name.ToLower().Contains(filter.Name.ToLower())) &&
                             (string.IsNullOrEmpty(filter.Country) || c.Countries.Any(s => s.Name.ToLower().Contains(filter.Country.ToLower()))) &&
                             (string.IsNullOrEmpty(filter.Synonym) || c.Synonyms.Any(s => s.Name.ToLower().Contains(filter.Synonym.ToLower()))));
                }

                return query;
            }
        }
    }
}