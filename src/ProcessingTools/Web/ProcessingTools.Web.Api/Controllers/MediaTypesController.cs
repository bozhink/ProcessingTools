namespace ProcessingTools.Web.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Models.Contracts.Files.Mediatypes;
    using ProcessingTools.Services.Contracts.Files;
    using ProcessingTools.Web.Models.Resources.MediaTypes;

    public class MediaTypesController : ApiController
    {
        private readonly IMediatypesResolver mediatypesResolver;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public MediaTypesController(IMediatypesResolver mediatypesResolver, ILogger<MediaTypesController> logger)
        {
            this.mediatypesResolver = mediatypesResolver ?? throw new ArgumentNullException(nameof(mediatypesResolver));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IMediatype, MediaTypeResponseModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var result = await this.mediatypesResolver.ResolveMediatypeAsync(id).ConfigureAwait(false);
                if (result == null)
                {
                    return this.NotFound();
                }

                var data = result.Select(this.mapper.Map<IMediatype, MediaTypeResponseModel>).ToArray();
                return this.Ok(data);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, string.Empty);
                return this.InternalServerError();
            }
        }
    }
}
