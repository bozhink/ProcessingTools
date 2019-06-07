﻿namespace ProcessingTools.Data.Entity.Geo
{
    using System.Linq;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Data.Models.Entity.Geo;

    public class EntityCitiesRepository : AbstractGeoSynonymisableRepository<City, ICity, ICitiesFilter, CitySynonym, ICitySynonym, ICitySynonymsFilter>, ICitiesRepository
    {
        private readonly IMapper mapper;

        public EntityCitiesRepository(IGeoRepository<City> repository, IGeoRepository<CitySynonym> synonymRepository, IApplicationContext applicationContext)
            : base(repository, synonymRepository, applicationContext)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<City, City>()
                    .ForMember(d => d.Country, o => o.Ignore())
                    .ForMember(d => d.County, o => o.Ignore())
                    .ForMember(d => d.District, o => o.Ignore())
                    .ForMember(d => d.Municipality, o => o.Ignore())
                    .ForMember(d => d.Province, o => o.Ignore())
                    .ForMember(d => d.Region, o => o.Ignore())
                    .ForMember(d => d.State, o => o.Ignore())
                    .ForMember(d => d.PostCodes, o => o.Ignore())
                    .ForMember(d => d.Synonyms, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore());

                c.CreateMap<CitySynonym, CitySynonym>()
                    .ForMember(d => d.City, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore());

                c.CreateMap<ICity, City>()
                    .ForMember(d => d.Country, o => o.Ignore())
                    .ForMember(d => d.County, o => o.Ignore())
                    .ForMember(d => d.District, o => o.Ignore())
                    .ForMember(d => d.Municipality, o => o.Ignore())
                    .ForMember(d => d.Province, o => o.Ignore())
                    .ForMember(d => d.Region, o => o.Ignore())
                    .ForMember(d => d.State, o => o.Ignore())
                    .ForMember(d => d.PostCodes, o => o.Ignore())
                    .ForMember(d => d.Synonyms, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore())
                    .ForMember(d => d.ModifiedBy, o => o.Ignore())
                    .ForMember(d => d.ModifiedOn, o => o.Ignore());

                c.CreateMap<ICitySynonym, CitySynonym>()
                    .ForMember(d => d.City, o => o.Ignore())
                    .ForMember(d => d.CityId, o => o.MapFrom(x => x.ParentId))
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore())
                    .ForMember(d => d.ModifiedBy, o => o.Ignore())
                    .ForMember(d => d.ModifiedOn, o => o.Ignore());

                c.CreateMap<City, ICity>()
                    .ConstructUsing(m => new CityModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        AbbreviatedName = m.AbbreviatedName,
                        CountryId = m.CountryId,
                        CountyId = m.CountyId,
                        DistrictId = m.DistrictId,
                        MunicipalityId = m.MunicipalityId,
                        ProvinceId = m.ProvinceId,
                        RegionId = m.RegionId,
                        StateId = m.StateId,
                        Country = new CountryModel
                        {
                            Id = m.Country.Id,
                            Name = m.Country.Name,
                            CallingCode = m.Country.CallingCode,
                            Iso639xCode = m.Country.Iso639xCode,
                            LanguageCode = m.Country.LanguageCode,
                        },
                        PostCodes = m.PostCodes
                            .Select(p => new PostCodeModel
                            {
                                Id = p.Id,
                                Code = p.Code,
                                Type = p.Type,
                                CityId = m.Id,
                            })
                            .ToList<IPostCode>(),
                        Synonyms = m.Synonyms
                            .Select(s => new CitySynonymModel
                            {
                                Id = s.Id,
                                LanguageCode = s.LanguageCode,
                                Name = s.Name,
                                ParentId = m.Id,
                            })
                            .ToList<ICitySynonym>(),
                    });

                c.CreateMap<CitySynonym, ICitySynonym>()
                    .ConstructUsing(s => new CitySynonymModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        LanguageCode = s.LanguageCode,
                        ParentId = s.CityId,
                    });
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        protected override IMapper Mapper => this.mapper;

        /// <inheritdoc/>
        protected override IQueryable<City> GetQuery(ICitiesFilter filter)
        {
            var query = this.Repository.Queryable();

            if (filter != null)
            {
                query = query.Where(
                    c =>
                         (!filter.Id.HasValue || c.Id == filter.Id) &&
                         (string.IsNullOrEmpty(filter.Name) || c.Name.ToLower().Contains(filter.Name.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Country) || c.Country.Name.ToLower().Contains(filter.Country.ToLower())) &&
                         (string.IsNullOrEmpty(filter.County) || c.County.Name.ToLower().Contains(filter.County.ToLower())) &&
                         (string.IsNullOrEmpty(filter.District) || c.District.Name.ToLower().Contains(filter.District.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Municipality) || c.Municipality.Name.ToLower().Contains(filter.Municipality.ToLower())) &&
                         (string.IsNullOrEmpty(filter.PostCode) || c.PostCodes.Any(p => p.Code.ToLower().Contains(filter.PostCode.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.Province) || c.Province.Name.ToLower().Contains(filter.Province.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Region) || c.Region.Name.ToLower().Contains(filter.Region.ToLower())) &&
                         (string.IsNullOrEmpty(filter.State) || c.State.Name.ToLower().Contains(filter.State.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Synonym) || c.Synonyms.Any(s => s.Name.ToLower().Contains(filter.Synonym.ToLower()))));
            }

            return query.AsNoTracking()
                .Include(e => e.Country)
                .Include(e => e.PostCodes)
                .Include(e => e.Synonyms);
        }
    }
}
