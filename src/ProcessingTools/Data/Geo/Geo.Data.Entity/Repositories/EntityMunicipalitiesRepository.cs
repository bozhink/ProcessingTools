namespace ProcessingTools.Geo.Data.Entity.Repositories
{
    using System.Data.Entity;
    using System.Linq;
    using AutoMapper;
    using ProcessingTools.Contracts.Data.Repositories.Geo;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Geo.Data.Entity.Abstractions.Repositories;
    using ProcessingTools.Geo.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Models.Contracts.Geo;

    public class EntityMunicipalitiesRepository : AbstractGeoSynonymisableRepository<Municipality, IMunicipality, IMunicipalitiesFilter, MunicipalitySynonym, IMunicipalitySynonym, IMunicipalitySynonymsFilter>, IMunicipalitiesRepository
    {
        private readonly IMapper mapper;

        public EntityMunicipalitiesRepository(IGeoRepository<Municipality> repository, IGeoRepository<MunicipalitySynonym> synonymRepository, IEnvironment environment)
            : base(repository, synonymRepository, environment)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<Municipality, Municipality>()
                    .ForMember(d => d.Country, o => o.Ignore())
                    .ForMember(d => d.State, o => o.Ignore())
                    .ForMember(d => d.Province, o => o.Ignore())
                    .ForMember(d => d.Region, o => o.Ignore())
                    .ForMember(d => d.District, o => o.Ignore())
                    .ForMember(d => d.Counties, o => o.Ignore())
                    .ForMember(d => d.Cities, o => o.Ignore())
                    .ForMember(d => d.Synonyms, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore());

                c.CreateMap<MunicipalitySynonym, MunicipalitySynonym>()
                    .ForMember(d => d.Municipality, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore());

                c.CreateMap<IMunicipality, Municipality>()
                    .ForMember(d => d.Country, o => o.Ignore())
                    .ForMember(d => d.State, o => o.Ignore())
                    .ForMember(d => d.Province, o => o.Ignore())
                    .ForMember(d => d.Region, o => o.Ignore())
                    .ForMember(d => d.District, o => o.Ignore())
                    .ForMember(d => d.Counties, o => o.Ignore())
                    .ForMember(d => d.Cities, o => o.Ignore())
                    .ForMember(d => d.Synonyms, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore())
                    .ForMember(d => d.ModifiedBy, o => o.Ignore())
                    .ForMember(d => d.ModifiedOn, o => o.Ignore());

                c.CreateMap<IMunicipalitySynonym, MunicipalitySynonym>()
                    .ForMember(d => d.Municipality, o => o.Ignore())
                    .ForMember(d => d.MunicipalityId, o => o.ResolveUsing(x => x.ParentId))
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore())
                    .ForMember(d => d.ModifiedBy, o => o.Ignore())
                    .ForMember(d => d.ModifiedOn, o => o.Ignore());

                c.CreateMap<Municipality, IMunicipality>()
                    .ConstructUsing(m => new MunicipalityModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        AbbreviatedName = m.AbbreviatedName,
                        CountryId = m.CountryId,
                        DistrictId = m.DistrictId,
                        ProvinceId = m.ProvinceId,
                        RegionId = m.RegionId,
                        StateId = m.StateId,
                        Synonyms = m.Synonyms
                            .Select(s => new MunicipalitySynonymModel
                            {
                                Id = s.Id,
                                LanguageCode = s.LanguageCode,
                                Name = s.Name,
                                ParentId = m.Id
                            })
                            .ToList<IMunicipalitySynonym>()
                    });

                c.CreateMap<MunicipalitySynonym, IMunicipalitySynonym>()
                    .ConstructUsing(s => new MunicipalitySynonymModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        LanguageCode = s.LanguageCode,
                        ParentId = s.MunicipalityId
                    });
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;

        protected override IQueryable<Municipality> GetQuery(IMunicipalitiesFilter filter)
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
                         (string.IsNullOrEmpty(filter.State) || c.State.Name.ToLower().Contains(filter.State.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Province) || c.Province.Name.ToLower().Contains(filter.Province.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Region) || c.Region.Name.ToLower().Contains(filter.Region.ToLower())) &&
                         (string.IsNullOrEmpty(filter.District) || c.District.Name.ToLower().Contains(filter.District.ToLower())) &&
                         (string.IsNullOrEmpty(filter.County) || c.Counties.Any(s => s.Name.ToLower().Contains(filter.County.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.City) || c.Cities.Any(s => s.Name.ToLower().Contains(filter.City.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.Synonym) || c.Synonyms.Any(s => s.Name.ToLower().Contains(filter.Synonym.ToLower()))));
            }

            return query;
        }
    }
}
