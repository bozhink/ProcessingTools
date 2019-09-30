// <copyright file="TaxonClassificationResolverController{TResolver}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Core.Api.Abstractions
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Contracts.Web.Services.Bio.Taxonomy;

    /// <summary>
    /// Generic taxon classification resolver API controller.
    /// </summary>
    /// <typeparam name="TResolver">Type of the classification resolver.</typeparam>
    public abstract class TaxonClassificationResolverController<TResolver> : ControllerBase
        where TResolver : class, ITaxonClassificationResolver
    {
        private readonly ITaxonClassificationResolverApiService<TResolver> service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonClassificationResolverController{TResolver}"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="ITaxonClassificationResolverApiService"/>.</param>
        /// <param name="logger">Logger.</param>
        protected TaxonClassificationResolverController(ITaxonClassificationResolverApiService<TResolver> service, ILogger logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get classification for specified taxon name.
        /// </summary>
        /// <param name="id">Taxon name to be resolved.</param>
        /// <returns>List of found classification data.</returns>
        [HttpGet("classification/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return this.BadRequest();
            }

            try
            {
                var result = await this.service.ResolveAsync(new[] { id }).ConfigureAwait(false);

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

        /// <summary>
        /// Get classification for specified taxon name.
        /// </summary>
        /// <param name="name">Taxon name to be resolved.</param>
        /// <returns>List of found classification data.</returns>
        [HttpGet("classification")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return this.BadRequest();
            }

            try
            {
                var result = await this.service.ResolveAsync(new[] { name }).ConfigureAwait(false);

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

        /// <summary>
        /// Get classification for specified taxon names.
        /// </summary>
        /// <param name="names">Taxon names to be resolved.</param>
        /// <returns>List of found classification data.</returns>
        [HttpPost("classification")]
        public async Task<IActionResult> GetByNames([FromBody] string[] names)
        {
            if (names is null || names.Length < 1)
            {
                return this.BadRequest();
            }

            try
            {
                var result = await this.service.ResolveAsync(names).ConfigureAwait(false);

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
