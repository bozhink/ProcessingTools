// <copyright file="GbifController.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Web.Core.Api.Abstractions;

    /// <summary>
    /// GBIF.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GbifController : AbstractTaxonClassificationResolverController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GbifController"/> class.
        /// </summary>
        /// <param name="resolver">Instance of <see cref="IGbifTaxonClassificationResolver"/>.</param>
        /// <param name="logger">Logger.</param>
        protected GbifController(IGbifTaxonClassificationResolver resolver, ILogger<GbifController> logger)
            : base(resolver, logger)
        {
        }
    }
}
