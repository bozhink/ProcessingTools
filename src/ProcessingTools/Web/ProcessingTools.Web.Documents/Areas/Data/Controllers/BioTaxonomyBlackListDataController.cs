// <copyright file="BioTaxonomyBlackListDataController.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Data.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Web.Services.Bio.Taxonomy;
    using ProcessingTools.Web.Models.Bio.Taxonomy.BlackList;

    /// <summary>
    /// Bio-taxonomy blacklist data controller.
    /// </summary>
    [Route("api/v1/data/bio/taxonomy/blacklist/[action]")]
    [ApiController]
    [Authorize]
    public class BioTaxonomyBlackListDataController : ControllerBase
    {
        private readonly IBlackListWebService service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BioTaxonomyBlackListDataController"/> class.
        /// </summary>
        /// <param name="service">Web service.</param>
        /// <param name="logger">Logger.</param>
        public BioTaxonomyBlackListDataController(IBlackListWebService service, ILogger<BioTaxonomyBlackListDataController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Inserts items.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns>Task of result.</returns>
        [HttpPost]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Insert([FromBody] BlackListItemsRequestModel model)
        {
            if (model is null)
            {
                return this.BadRequest();
            }

            try
            {
                var response = await this.service.InsertAsync(model).ConfigureAwait(false);

                if (response is null)
                {
                    return this.StatusCode((int)HttpStatusCode.UnprocessableEntity);
                }

                return this.Ok();
            }
            catch (ArgumentException ex)
            {
                this.logger.LogError(ex, string.Empty);
                return this.BadRequest();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, string.Empty);
                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Search by search string.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns>Task of result.</returns>
        [HttpPost]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Search([FromBody] BlackListSearchRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.SearchString))
            {
                return this.BadRequest();
            }

            try
            {
                var response = await this.service.SearchAsync(model).ConfigureAwait(false);

                if (response is null)
                {
                    this.NotFound();
                }

                return this.Ok(response);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogError(ex, string.Empty);
                return this.BadRequest();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, string.Empty);
                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
