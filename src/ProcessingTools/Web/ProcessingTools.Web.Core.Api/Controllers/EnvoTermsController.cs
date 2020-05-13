// <copyright file="EnvoTermsController.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
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
    using ProcessingTools.Contracts.Models.Bio.Environments;
    using ProcessingTools.Contracts.Services.Bio.Environments;
    using ProcessingTools.Web.Models.Bio.Environments;

    /// <summary>
    /// ENVO terms controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EnvoTermsController : ControllerBase
    {
        private readonly IEnvoTermsDataService service;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvoTermsController"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="IEnvoTermsDataService"/>.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public EnvoTermsController(IEnvoTermsDataService service, ILogger<EnvoTermsController> logger, IMapper mapper)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get list of ENVO terms.
        /// </summary>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>List of  ENVO terms.</returns>
        [HttpGet]
        public async Task<IActionResult> Get(int skip = PaginationConstants.DefaultSkip, int take = PaginationConstants.DefaultTake)
        {
            try
            {
                var result = await this.service.GetAsync(skip, take).ConfigureAwait(false);
                if (result is null)
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
