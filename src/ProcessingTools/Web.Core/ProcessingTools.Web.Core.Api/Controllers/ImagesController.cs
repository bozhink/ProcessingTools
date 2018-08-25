// <copyright file="ImagesController.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Core.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Web.Services.Contracts.Images;

    /// <summary>
    /// Images controller.
    /// </summary>
    [Route("api/image")]
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
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null)
            {
                return this.UnprocessableEntity();
            }

            try
            {
                var result = await this.service.UploadImageAsync(file).ConfigureAwait(false);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Upload image");

                return new EmptyResult();
            }
        }
    }
}
