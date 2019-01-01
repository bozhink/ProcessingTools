namespace ProcessingTools.Web.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Models.Contracts.Files.Mediatypes;
    using ProcessingTools.Services.Contracts.Files;
    using ProcessingTools.Web.Models.Resources.MediaTypes;

    [Route("api/[controller]")]
    [ApiController]
    public class MediatypesController : ControllerBase
    {
        private readonly IMediatypesResolver mediatypesResolver;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public MediatypesController(IMediatypesResolver mediatypesResolver, ILogger<MediatypesController> logger)
        {
            this.mediatypesResolver = mediatypesResolver ?? throw new ArgumentNullException(nameof(mediatypesResolver));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IMediatype, MediatypeResponseModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var result = await this.mediatypesResolver.ResolveMediatypeAsync(id).ConfigureAwait(false);
                if (result == null)
                {
                    return this.NotFound();
                }

                var data = result.Select(this.mapper.Map<IMediatype, MediatypeResponseModel>).ToArray();
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
