// <copyright file="ImagesController.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Core.Api.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Web.Services.Images;

    /// <summary>
    /// Images controller.
    /// </summary>
    [Route("api/v1/image")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageWriterWebService service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagesController"/> class.
        /// </summary>
        /// <param name="service">Images service.</param>
        /// <param name="logger">Logger.</param>
        public ImagesController(IImageWriterWebService service, ILogger<ImagesController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Uploads an image to the server.
        /// </summary>
        /// <param name="file">File to be uploaded.</param>
        /// <returns>Action result.</returns>
        /// <response code="200">Resultant message of the operation.</response>
        /// <response code="422">If the provided file is not valid.</response>
        /// <response code="500">If something unexpected happened. See log for details.</response>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file is null)
            {
                return this.UnprocessableEntity();
            }

            try
            {
                string result = await this.service.UploadImageAsync(file).ConfigureAwait(false);
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
