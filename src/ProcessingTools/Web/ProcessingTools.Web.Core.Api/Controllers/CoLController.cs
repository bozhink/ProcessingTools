// <copyright file="CoLController.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Contracts.Web.Services.Bio.Taxonomy;
    using ProcessingTools.Web.Core.Api.Abstractions;

    /// <summary>
    /// Taxon classification with CoL service API controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CoLController : TaxonClassificationResolverController<ICatalogueOfLifeTaxonClassificationResolver>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoLController"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="ITaxonClassificationResolverApiService{ICatalogueOfLifeTaxonClassificationResolver}"/>.</param>
        /// <param name="logger">Logger.</param>
        public CoLController(ITaxonClassificationResolverApiService<ICatalogueOfLifeTaxonClassificationResolver> service, ILogger<CoLController> logger)
            : base(service, logger)
        {
        }
    }
}
