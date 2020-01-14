﻿// <copyright file="EntityProvincesRepository.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Geo
{
    using System.Linq;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Data.Models.Entity.Geo;

    public class EntityProvincesRepository : AbstractGeoSynonymisableRepository<Province, IProvince, IProvincesFilter, ProvinceSynonym, IProvinceSynonym, IProvinceSynonymsFilter>, IProvincesRepository
    {
        private readonly IMapper mapper;

        public EntityProvincesRepository(IGeoRepository<Province> repository, IGeoRepository<ProvinceSynonym> synonymRepository, IApplicationContext applicationContext)
            : base(repository, synonymRepository, applicationContext)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<Province, Province>()
                    .ForMember(d => d.Country, o => o.Ignore())
                    .ForMember(d => d.State, o => o.Ignore())
                    .ForMember(d => d.Regions, o => o.Ignore())
                    .ForMember(d => d.Districts, o => o.Ignore())
                    .ForMember(d => d.Municipalities, o => o.Ignore())
                    .ForMember(d => d.Counties, o => o.Ignore())
                    .ForMember(d => d.Cities, o => o.Ignore())
                    .ForMember(d => d.Synonyms, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore());

                c.CreateMap<ProvinceSynonym, ProvinceSynonym>()
                    .ForMember(d => d.Province, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore());

                c.CreateMap<IProvince, Province>()
                    .ForMember(d => d.Country, o => o.Ignore())
                    .ForMember(d => d.State, o => o.Ignore())
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

                c.CreateMap<IProvinceSynonym, ProvinceSynonym>()
                    .ForMember(d => d.Province, o => o.Ignore())
                    .ForMember(d => d.ProvinceId, o => o.MapFrom(x => x.ParentId))
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore())
                    .ForMember(d => d.ModifiedBy, o => o.Ignore())
                    .ForMember(d => d.ModifiedOn, o => o.Ignore());

                c.CreateMap<Province, IProvince>()
                    .ConstructUsing(m => new ProvinceModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        AbbreviatedName = m.AbbreviatedName,
                        CountryId = m.CountryId,
                        StateId = m.StateId,
                        Synonyms = m.Synonyms
                            .Select(s => new ProvinceSynonymModel
                            {
                                Id = s.Id,
                                LanguageCode = s.LanguageCode,
                                Name = s.Name,
                                ParentId = m.Id,
                            })
                            .ToList<IProvinceSynonym>(),
                    });

                c.CreateMap<ProvinceSynonym, IProvinceSynonym>()
                    .ConstructUsing(s => new ProvinceSynonymModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        LanguageCode = s.LanguageCode,
                        ParentId = s.ProvinceId,
                    });
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        protected override IMapper Mapper => this.mapper;

        /// <inheritdoc/>
        protected override IQueryable<Province> GetQuery(IProvincesFilter filter)
        {
            var query = this.Repository.Queryable();

            if (filter != null)
            {
                query = query.Where(
                    c =>
                         (!filter.Id.HasValue || c.Id == filter.Id) &&
                         (string.IsNullOrEmpty(filter.Name) || c.Name.ToLower().Contains(filter.Name.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Country) || c.Country.Name.ToLower().Contains(filter.Country.ToLower())) &&
                         (string.IsNullOrEmpty(filter.State) || c.State.Name.ToLower().Contains(filter.State.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Region) || c.Regions.Any(s => s.Name.ToLower().Contains(filter.Region.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.District) || c.Districts.Any(s => s.Name.ToLower().Contains(filter.District.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.Municipality) || c.Municipalities.Any(s => s.Name.ToLower().Contains(filter.Municipality.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.County) || c.Counties.Any(s => s.Name.ToLower().Contains(filter.County.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.City) || c.Cities.Any(s => s.Name.ToLower().Contains(filter.City.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.Synonym) || c.Synonyms.Any(s => s.Name.ToLower().Contains(filter.Synonym.ToLower()))));
            }

            return query.AsNoTracking()
                .Include(e => e.Country)
                .Include(e => e.Synonyms);
        }
    }
}
