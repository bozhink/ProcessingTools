// <copyright file="AphiaController.cs" company="ProcessingTools">
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
    /// Taxon classification with APHIA service API controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AphiaController : TaxonClassificationResolverController<IAphiaTaxonClassificationResolver>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AphiaController"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="ITaxonClassificationResolverApiService{IAphiaTaxonClassificationResolver}"/>.</param>
        /// <param name="logger">Logger.</param>
        public AphiaController(ITaxonClassificationResolverApiService<IAphiaTaxonClassificationResolver> service, ILogger<AphiaController> logger)
            : base(service, logger)
        {
        }
    }
}
