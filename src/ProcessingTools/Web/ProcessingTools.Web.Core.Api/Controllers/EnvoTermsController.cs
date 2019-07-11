// <copyright file="EnvoTermsController.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Models.Bio;
    using ProcessingTools.Contracts.Services.Bio.Environments;
    using ProcessingTools.Web.Models.Bio.EnvoTerms;

    [Route("api/[controller]")]
    [ApiController]
    public class EnvoTermsController : ControllerBase
    {
        private readonly IEnvoTermsDataService service;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public EnvoTermsController(IEnvoTermsDataService service, ILogger<EnvoTermsController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IEnvoTerm, EnvoTermResponseModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        [HttpGet]
        public async Task<IActionResult> Get(int skip = PaginationConstants.DefaultSkip, int take = PaginationConstants.DefaultTake)
        {
            try
            {
                var result = await this.service.GetAsync(skip, take).ConfigureAwait(false);
                if (result == null)
                {
                    return this.NotFound();
                }

                var data = result.Select(this.mapper.Map<IEnvoTerm, EnvoTermResponseModel>).ToArray();
                return this.Ok(data);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, string.Empty);
                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
