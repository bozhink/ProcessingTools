namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Data.Geo.Filters;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;
    using ProcessingTools.Geo.Data.Entity.Contracts;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Geo.Services.Data.Entity.Abstractions;
    using ProcessingTools.Geo.Services.Data.Entity.Contracts.Services;

    public class EntityPostCodesDataService : AbstractGeoDataService<PostCode, IPostCode, IPostCodesFilter>, IEntityPostCodesDataService
    {
        public EntityPostCodesDataService(IGeoRepository<PostCode> repository, IEnvironment environment)
            : base(repository, environment)
        {
        }

        protected override Func<PostCode, IPostCode> MapEntityToModel => m => new ProcessingTools.Geo.Services.Data.Entity.Models.PostCode
        {
            Id = m.Id,
            CityId = m.CityId,
            Code = m.Code,
            Type = m.Type
        };

        protected override Func<IPostCode, PostCode> MapModelToEntity => m => new PostCode
        {
            Id = m.Id,
            CityId = m.CityId,
            Code = m.Code,
            Type = m.Type
        };

        protected override IQueryable<PostCode> GetQuery(IPostCodesFilter filter)
        {
            var query = this.Repository.Queryable()
                .Include(e => e.City);

            if (filter != null)
            {
                query = query.Where(
                    c => (!filter.Id.HasValue || c.Id == filter.Id) &&
                         (string.IsNullOrEmpty(filter.Country) || c.City.Country.Name.ToLower().Contains(filter.Country.ToLower())) &&
                         (string.IsNullOrEmpty(filter.County) || c.City.County.Name.ToLower().Contains(filter.County.ToLower())) &&
                         (string.IsNullOrEmpty(filter.District) || c.City.District.Name.ToLower().Contains(filter.District.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Municipality) || c.City.Municipality.Name.ToLower().Contains(filter.Municipality.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Province) || c.City.Province.Name.ToLower().Contains(filter.Province.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Region) || c.City.Region.Name.ToLower().Contains(filter.Region.ToLower())) &&
                         (string.IsNullOrEmpty(filter.State) || c.City.State.Name.ToLower().Contains(filter.State.ToLower())));
            }

            return query;
        }
    }
}
