namespace ProcessingTools.Geo.Data.Entity.Repositories
{
    using System.Data.Entity;
    using System.Linq;
    using AutoMapper;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Geo.Data.Entity.Abstractions.Repositories;
    using ProcessingTools.Geo.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Models.Contracts.Geo;

    public class EntityDistrictsRepository : AbstractGeoSynonymisableRepository<District, IDistrict, IDistrictsFilter, DistrictSynonym, IDistrictSynonym, IDistrictSynonymsFilter>, IDistrictsRepository
    {
        private readonly IMapper mapper;

        public EntityDistrictsRepository(IGeoRepository<District> repository, IGeoRepository<DistrictSynonym> synonymRepository, IApplicationContext applicationContext)
            : base(repository, synonymRepository, applicationContext)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<District, District>()
                    .ForMember(d => d.Country, o => o.Ignore())
                    .ForMember(d => d.State, o => o.Ignore())
                    .ForMember(d => d.Province, o => o.Ignore())
                    .ForMember(d => d.Region, o => o.Ignore())
                    .ForMember(d => d.Municipalities, o => o.Ignore())
                    .ForMember(d => d.Counties, o => o.Ignore())
                    .ForMember(d => d.Cities, o => o.Ignore())
                    .ForMember(d => d.Synonyms, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore());

                c.CreateMap<DistrictSynonym, DistrictSynonym>()
                    .ForMember(d => d.District, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore());

                c.CreateMap<IDistrict, District>()
                    .ForMember(d => d.Country, o => o.Ignore())
                    .ForMember(d => d.State, o => o.Ignore())
                    .ForMember(d => d.Province, o => o.Ignore())
                    .ForMember(d => d.Region, o => o.Ignore())
                    .ForMember(d => d.Municipalities, o => o.Ignore())
                    .ForMember(d => d.Counties, o => o.Ignore())
                    .ForMember(d => d.Cities, o => o.Ignore())
                    .ForMember(d => d.Synonyms, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore())
                    .ForMember(d => d.ModifiedBy, o => o.Ignore())
                    .ForMember(d => d.ModifiedOn, o => o.Ignore());

                c.CreateMap<IDistrictSynonym, DistrictSynonym>()
                    .ForMember(d => d.District, o => o.Ignore())
                    .ForMember(d => d.DistrictId, o => o.ResolveUsing(x => x.ParentId))
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore())
                    .ForMember(d => d.ModifiedBy, o => o.Ignore())
                    .ForMember(d => d.ModifiedOn, o => o.Ignore());

                c.CreateMap<District, IDistrict>()
                    .ConstructUsing(m => new DistrictModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        AbbreviatedName = m.AbbreviatedName,
                        CountryId = m.CountryId,
                        ProvinceId = m.ProvinceId,
                        RegionId = m.RegionId,
                        StateId = m.StateId,
                        Synonyms = m.Synonyms
                            .Select(s => new DistrictSynonymModel
                            {
                                Id = s.Id,
                                LanguageCode = s.LanguageCode,
                                Name = s.Name,
                                ParentId = m.Id
                            })
                            .ToList<IDistrictSynonym>()
                    });

                c.CreateMap<DistrictSynonym, IDistrictSynonym>()
                    .ConstructUsing(s => new DistrictSynonymModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        LanguageCode = s.LanguageCode,
                        ParentId = s.DistrictId
                    });
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;

        protected override IQueryable<District> GetQuery(IDistrictsFilter filter)
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
                         (string.IsNullOrEmpty(filter.Municipality) || c.Municipalities.Any(s => s.Name.ToLower().Contains(filter.Municipality.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.County) || c.Counties.Any(s => s.Name.ToLower().Contains(filter.County.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.City) || c.Cities.Any(s => s.Name.ToLower().Contains(filter.City.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.Synonym) || c.Synonyms.Any(s => s.Name.ToLower().Contains(filter.Synonym.ToLower()))));
            }

            return query;
        }
    }
}
