// <copyright file="ParseController.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Api.Geography.Areas.Coordinates.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Web.Models.Geography.Coordinates;

    /// <summary>
    /// Parse coordinates controller.
    /// </summary>
    [Route("api/v1/coordinates/parse")]
    [ApiController]
    public class ParseController : ControllerBase
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public ParseController(ILogger<ParseController> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Parse list of coordinates provided as single string.
        /// </summary>
        /// <param name="coordinates">Parse coordinates string request model.</param>
        /// <returns>List of parsed coordinates.</returns>
        /// <response code="200">Returns parsed coordinates.</response>
        /// <response code="400">If no valid coordinates string is provided.</response>
        /// <response code="500">If something unexpected happened. See log for details.</response>
        [HttpPost(Order = 0)]
        [ProducesResponseType(200, Type = typeof(IList<CoordinateResponseModel>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint general exception")]
        public IActionResult ParseCoordinatesString([FromBody] string coordinates)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    // TODO
                    return this.Ok();
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, string.Empty);
                    return this.StatusCode((int)HttpStatusCode.InternalServerError);
                }
            }

            return this.BadRequest(this.ModelState);
        }

        /// <summary>
        /// Parse list of coordinate strings.
        /// </summary>
        /// <param name="coordinates">Parse list of coordinate strings request model.</param>
        /// <returns>List of parsed coordinates.</returns>
        /// <response code="200">Returns parsed coordinates.</response>
        /// <response code="400">If no valid coordinates string is provided.</response>
        /// <response code="500">If something unexpected happened. See log for details.</response>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(IList<CoordinateResponseModel>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint general exception")]
        public IActionResult ParseCoordinateStrings([FromBody] string[] coordinates)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    // TODO
                    return this.Ok();
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, string.Empty);
                    return this.StatusCode((int)HttpStatusCode.InternalServerError);
                }
            }

            return this.BadRequest(this.ModelState);
        }
    }
}
