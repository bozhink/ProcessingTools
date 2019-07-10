// <copyright file="BioTaxonomyTaxonRanksDataController.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
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
    using ProcessingTools.Web.Models.Bio.Taxonomy.TaxonRanks;

    /// <summary>
    /// Bio-taxonomy taxon-ranks data controller.
    /// </summary>
    [Route("api/v1/data/bio/taxonomy/taxonranks/[action]")]
    [ApiController]
    [Authorize]
    public class BioTaxonomyTaxonRanksDataController : ControllerBase
    {
        private readonly ITaxonRanksWebService service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BioTaxonomyTaxonRanksDataController"/> class.
        /// </summary>
        /// <param name="service">Web service.</param>
        /// <param name="logger">Logger.</param>
        public BioTaxonomyTaxonRanksDataController(ITaxonRanksWebService service, ILogger<BioTaxonomyTaxonRanksDataController> logger)
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
        public async Task<IActionResult> Insert(TaxonRanksRequestModel model)
        {
            if (model == null)
            {
                return this.BadRequest();
            }

            try
            {
                var response = await this.service.InsertAsync(model).ConfigureAwait(false);

                if (response == null)
                {
                    return this.StatusCode((int)HttpStatusCode.UnprocessableEntity);
                }

                return this.Ok();
            }
            catch (ArgumentException ex)
            {
                this.logger.LogError(ex, "Insert Bad Request");
                return this.BadRequest();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Insert Internal Server Error");
                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Search by search string.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns>Task of result.</returns>
        [HttpPost]
        public async Task<IActionResult> Search(SearchRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.SearchString))
            {
                return this.BadRequest();
            }

            try
            {
                var response = await this.service.SearchAsync(model).ConfigureAwait(false);

                if (response == null)
                {
                    this.NotFound();
                }

                return this.Ok(response);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogError(ex, "Search Bad Request");
                return this.BadRequest();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Search Internal Server Error");
                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
