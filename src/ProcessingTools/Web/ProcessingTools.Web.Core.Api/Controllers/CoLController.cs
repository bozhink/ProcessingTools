// <copyright file="CoLController.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Web.Core.Api.Abstractions;

    /// <summary>
    /// CoL.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CoLController : AbstractTaxonClassificationResolverController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoLController"/> class.
        /// </summary>
        /// <param name="resolver">Instance of <see cref="ICatalogueOfLifeTaxonClassificationResolver"/>.</param>
        /// <param name="logger">Logger.</param>
        protected CoLController(ICatalogueOfLifeTaxonClassificationResolver resolver, ILogger<CoLController> logger)
            : base(resolver, logger)
        {
        }
    }
}
