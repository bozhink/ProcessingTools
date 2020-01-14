// <copyright file="ParseController.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Api.Geography.Areas.Coordinates.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Web.Services.Geo.Coordinates;
    using ProcessingTools.Web.Models.Geography.Coordinates;

    /// <summary>
    /// Parse coordinates controller.
    /// </summary>
    [Route("api/v1/coordinates/parse")]
    [ApiController]
    public class ParseController : ControllerBase
    {
        private readonly ICoordinatesCalculatorApiService service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseController"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="ICoordinatesCalculatorApiService"/>.</param>
        /// <param name="logger">Logger.</param>
        public ParseController(ICoordinatesCalculatorApiService service, ILogger<ParseController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Parse list of coordinates provided as single string.
        /// </summary>
        /// <param name="coordinates">List of coordinates as string.</param>
        /// <returns>List of parsed coordinates.</returns>
        /// <response code="200">Returns parsed coordinates.</response>
        /// <response code="400">If no valid coordinates string is provided.</response>
        /// <response code="500">If something unexpected happened. See log for details.</response>
        [HttpPost]
        [Route("string")]
        [ProducesResponseType(200, Type = typeof(IList<CoordinateResponseModel>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint general exception")]
        public async Task<IActionResult> ParseCoordinatesAsync([FromBody] string coordinates)
        {
            if (string.IsNullOrWhiteSpace(coordinates))
            {
                return this.BadRequest();
            }

            try
            {
                var data = await this.service.ParseCoordinatesAsync(coordinates).ConfigureAwait(false);

                return this.Ok(data);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, string.Empty);
                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Parse list of coordinate strings.
        /// </summary>
        /// <param name="coordinates">Coordinates as list of strings.</param>
        /// <returns>List of parsed coordinates.</returns>
        /// <response code="200">Returns parsed coordinates.</response>
        /// <response code="400">If no valid coordinates string is provided.</response>
        /// <response code="500">If something unexpected happened. See log for details.</response>
        [HttpPost]
        [Route("list")]
        [ProducesResponseType(200, Type = typeof(IList<CoordinateResponseModel>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint general exception")]
        public async Task<IActionResult> ParseCoordinatesAsync([FromBody] string[] coordinates)
        {
            if (coordinates is null)
            {
                return this.BadRequest();
            }

            try
            {
                var data = await this.service.ParseCoordinatesAsync(coordinates).ConfigureAwait(false);

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
