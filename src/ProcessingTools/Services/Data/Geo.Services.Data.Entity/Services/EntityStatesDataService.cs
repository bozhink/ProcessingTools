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

    public class EntityStatesDataService : AbstractGeoSynonymisableDataService<State, IState, IStatesFilter, StateSynonym, IStateSynonym, IStateSynonymsFilter>, IEntityStatesDataService
    {
        private readonly IMapper mapper;

        public EntityStatesDataService(IGeoRepository<State> repository, IGeoRepository<StateSynonym> synonymRepository, IEnvironment environment)
            : base(repository, synonymRepository, environment)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<State, State>()
                    .ForMember(d => d.Country, o => o.Ignore())
                    .ForMember(d => d.Provinces, o => o.Ignore())
                    .ForMember(d => d.Regions, o => o.Ignore())
                    .ForMember(d => d.Districts, o => o.Ignore())
                    .ForMember(d => d.Municipalities, o => o.Ignore())
                    .ForMember(d => d.Counties, o => o.Ignore())
                    .ForMember(d => d.Cities, o => o.Ignore())
                    .ForMember(d => d.Synonyms, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore());

                c.CreateMap<StateSynonym, StateSynonym>()
                    .ForMember(d => d.State, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore());

                c.CreateMap<IState, State>()
                    .ForMember(d => d.Country, o => o.Ignore())
                    .ForMember(d => d.Provinces, o => o.Ignore())
                    .ForMember(d => d.Regions, o => o.Ignore())
                    .ForMember(d => d.Districts, o => o.Ignore())
                    .ForMember(d => d.Municipalities, o => o.Ignore())
                    .ForMember(d => d.Counties, o => o.Ignore())
                    .ForMember(d => d.Cities, o => o.Ignore())
                    .ForMember(d => d.Synonyms, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore())
                    .ForMember(d => d.ModifiedBy, o => o.Ignore())
                    .ForMember(d => d.ModifiedOn, o => o.Ignore());

                c.CreateMap<IStateSynonym, StateSynonym>()
                    .ForMember(d => d.State, o => o.Ignore())
                    .ForMember(d => d.StateId, o => o.ResolveUsing(x => x.ParentId))
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore())
                    .ForMember(d => d.ModifiedBy, o => o.Ignore())
                    .ForMember(d => d.ModifiedOn, o => o.Ignore());

                c.CreateMap<State, IState>()
                    .ConstructUsing(m => new ProcessingTools.Geo.Services.Data.Entity.Models.State
                    {
                        Id = m.Id,
                        Name = m.Name,
                        AbbreviatedName = m.AbbreviatedName,
                        CountryId = m.CountryId,
                        Synonyms = m.Synonyms
                            .Select(s => new ProcessingTools.Geo.Services.Data.Entity.Models.StateSynonym
                            {
                                Id = s.Id,
                                LanguageCode = s.LanguageCode,
                                Name = s.Name,
                                ParentId = m.Id
                            })
                            .ToList<IStateSynonym>()
                    });

                c.CreateMap<StateSynonym, IStateSynonym>()
                    .ConstructUsing(s => new ProcessingTools.Geo.Services.Data.Entity.Models.StateSynonym
                    {
                        Id = s.Id,
                        Name = s.Name,
                        LanguageCode = s.LanguageCode,
                        ParentId = s.StateId
                    });
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;

        protected override IQueryable<State> GetQuery(IStatesFilter filter)
        {
            var query = this.Repository.Queryable()
                .Include(e => e.Country)
                .Include(e => e.Synonyms);

            if (filter != null)
            {
                query = query.Where(
                    c =>
                         (!filter.Id.HasValue || c.Id == filter.Id) &&
                         (string.IsNullOrEmpty(filter.Name) || c.Name.ToLower().Contains(filter.Name.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Country) || c.Country.Name.ToLower().Contains(filter.Country.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Province) || c.Provinces.Any(s => s.Name.ToLower().Contains(filter.Province.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.Region) || c.Regions.Any(s => s.Name.ToLower().Contains(filter.Region.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.District) || c.Districts.Any(s => s.Name.ToLower().Contains(filter.District.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.Municipality) || c.Municipalities.Any(s => s.Name.ToLower().Contains(filter.Municipality.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.County) || c.Counties.Any(s => s.Name.ToLower().Contains(filter.County.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.City) || c.Cities.Any(s => s.Name.ToLower().Contains(filter.City.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.Synonym) || c.Synonyms.Any(s => s.Name.ToLower().Contains(filter.Synonym.ToLower()))));
            }

            return query;
        }
    }
}