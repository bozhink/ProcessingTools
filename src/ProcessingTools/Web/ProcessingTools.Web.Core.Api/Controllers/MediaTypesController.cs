// <copyright file="MediaTypesController.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Web.Services.Files;
    using ProcessingTools.Web.Models.Resources.MediaTypes;

    /// <summary>
    /// Mediatypes controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MediatypesController : ControllerBase
    {
        private readonly IMediatypesApiService service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediatypesController"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="IMediatypesApiService"/>.</param>
        /// <param name="logger">Logger.</param>
        public MediatypesController(IMediatypesApiService service, ILogger<MediatypesController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Resolves mediatype by file name.
        /// </summary>
        /// <param name="id">File name.</param>
        /// <returns>Response model if resolved correctly.</returns>
        /// <response code="200">Returns resolved items.</response>
        /// <response code="400">If no valid file name is provided.</response>
        /// <response code="404">If no resolution is found.</response>
        /// <response code="500">If something unexpected happened. See log for details.</response>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IList<MediatypeResponseModel>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return this.BadRequest();
            }

            try
            {
                IList<MediatypeResponseModel> result = await this.service.ResolveMediatypeAsync(id).ConfigureAwait(false);

                if (result is null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, string.Empty);
                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
