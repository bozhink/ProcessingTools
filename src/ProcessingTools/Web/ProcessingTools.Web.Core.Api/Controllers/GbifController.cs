// <copyright file="GbifController.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Bio.Taxonomy.Api.Contracts;
    using ProcessingTools.Bio.Taxonomy.External.GbifApiV09.Contracts;
    using ProcessingTools.Web.Core.Api.Abstractions;

    /// <summary>
    /// Taxon classification with GBIF service API controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GbifController : TaxonClassificationResolverController<IGbifTaxonClassificationResolver>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GbifController"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="ITaxonClassificationResolverApiService{IGbifTaxonClassificationResolver}"/>.</param>
        /// <param name="logger">Logger.</param>
        public GbifController(ITaxonClassificationResolverApiService<IGbifTaxonClassificationResolver> service, ILogger<GbifController> logger)
            : base(service, logger)
        {
        }
    }
}
