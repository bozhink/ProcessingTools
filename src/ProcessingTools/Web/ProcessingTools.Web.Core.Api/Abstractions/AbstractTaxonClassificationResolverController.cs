namespace ProcessingTools.Web.Core.Api.Abstractions
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Web.Models.Bio.Taxonomy;

    public abstract class AbstractTaxonClassificationResolverController : ControllerBase
    {
        private readonly ITaxonClassificationResolver resolver;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        protected AbstractTaxonClassificationResolverController(ITaxonClassificationResolver resolver, ILogger logger)
        {
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<ITaxonClassification, TaxonClassificationResponseModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var result = await this.resolver.ResolveAsync(new[] { id }).ConfigureAwait(false);
                if (result == null)
                {
                    return this.NotFound();
                }

                var data = result.Select(this.mapper.Map<ITaxonClassification, TaxonClassificationResponseModel>).ToList();
                return this.Ok(data);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, string.Empty);
                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
