﻿// <copyright file="EntityContinentsRepository.cs" company="ProcessingTools">
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

    public class EntityContinentsRepository : AbstractGeoSynonymisableRepository<Continent, IContinent, IContinentsFilter, ContinentSynonym, IContinentSynonym, IContinentSynonymsFilter>, IContinentsRepository
    {
        private readonly IMapper mapper;

        public EntityContinentsRepository(IGeoRepository<Continent> repository, IGeoRepository<ContinentSynonym> synonymRepository, IApplicationContext applicationContext)
            : base(repository, synonymRepository, applicationContext)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<Continent, Continent>()
                    .ForMember(d => d.Countries, o => o.Ignore())
                    .ForMember(d => d.Synonyms, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore());

                c.CreateMap<ContinentSynonym, ContinentSynonym>()
                    .ForMember(d => d.Continent, o => o.Ignore())
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
                    .ForMember(d => d.ContinentId, o => o.MapFrom(x => x.ParentId))
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedOn, o => o.Ignore())
                    .ForMember(d => d.ModifiedBy, o => o.Ignore())
                    .ForMember(d => d.ModifiedOn, o => o.Ignore());

                c.CreateMap<Continent, IContinent>()
                    .ConstructUsing(m => new ContinentModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        AbbreviatedName = m.AbbreviatedName,
                        ////Synonyms = m.Synonyms
                        ////    .Select(s => new ContinentSynonymModel
                        ////    {
                        ////        Id = s.Id,
                        ////        Name = s.Name,
                        ////        LanguageCode = s.LanguageCode,
                        ////        ParentId = m.Id,
                        ////    })
                        ////    .ToList<IContinentSynonym>(),
                    });

                c.CreateMap<ContinentSynonym, IContinentSynonym>()
                    .ConstructUsing(s => new ContinentSynonymModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        LanguageCode = s.LanguageCode,
                        ParentId = s.ContinentId,
                    });
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        protected override IMapper Mapper => this.mapper;

        /// <inheritdoc/>
        protected override IQueryable<Continent> GetQuery(IContinentsFilter filter)
        {
            {
                var query = this.Repository.Queryable();

                if (filter != null)
                {
                    query = query.Where(
                        c =>
                             (!filter.Id.HasValue || c.Id == filter.Id) &&
                             (string.IsNullOrEmpty(filter.Name) || c.Name.ToLower().Contains(filter.Name.ToLower())) &&
                             (string.IsNullOrEmpty(filter.Country) || c.Countries.Any(s => s.Name.ToLower().Contains(filter.Country.ToLower()))) &&
                             (string.IsNullOrEmpty(filter.Synonym) || c.Synonyms.Any(s => s.Name.ToLower().Contains(filter.Synonym.ToLower()))));
                }

                return query.AsNoTracking()
                    .Include(c => c.Synonyms);
            }
        }
    }
}
