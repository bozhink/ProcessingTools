// <copyright file="AphiaController.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Web.Core.Api.Abstractions;

    [Route("api/[controller]")]
    [ApiController]
    public class AphiaController : AbstractTaxonClassificationResolverController
    {
        protected AphiaController(IAphiaTaxonClassificationResolver resolver, ILogger<AphiaController> logger)
            : base(resolver, logger)
        {
        }
    }
}
