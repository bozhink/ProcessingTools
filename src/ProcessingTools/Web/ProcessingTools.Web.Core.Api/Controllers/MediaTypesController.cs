// <copyright file="MediaTypesController.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Api.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Web.Services.Files;
    using ProcessingTools.Web.Models.Resources.MediaTypes;

    /// <summary>
    /// Mediatypes controller.
    /// </summary>
    [Route("api/[controller]")]
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
        /// <returns><see cref="MediatypeResponseModel"/> if resolved correctly.</returns>
        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var result = await this.service.ResolveMediatypeAsync(id).ConfigureAwait(false);
                if (result == null)
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
