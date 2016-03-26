﻿namespace ProcessingTools.Web.Api.Models.GeoNames
{
    using Contracts.Mapping;
    using Geo.Services.Data.Models.Contracts;

    public class GeoNameResponseModel : IMapFrom<IGeoNameServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}