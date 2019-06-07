﻿namespace ProcessingTools.Web.Api.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Resources;
    using ProcessingTools.Services.Contracts.Resources;
    using ProcessingTools.Web.Core.Api.Abstractions;
    using ProcessingTools.Web.Models.Resources.Institutions;

    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionsController : GenericDataServiceController<IInstitutionsDataService, IInstitution, InstitutionRequestModel, InstitutionResponseModel, IFilter>
    {
        private readonly IMapper mapper;

        public InstitutionsController(IInstitutionsDataService service)
            : base(service)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IInstitution, InstitutionResponseModel>();
                c.CreateMap<InstitutionRequestModel, IInstitution>().ConvertUsing(i => i);
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;
    }
}
