namespace ProcessingTools.Web.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using MediaType.Services.Data.Contracts;
    using Models.MediaTypes;

    public class MediaTypeController : ApiController
    {
        private IMediaTypeDataService mediatypeDataService;

        public MediaTypeController(IMediaTypeDataService mediatypeService)
        {
            this.mediatypeDataService = mediatypeService;
        }

        public IHttpActionResult Get(string id)
        {
            var result = this.mediatypeDataService?.GetMediaType(id)?.Select(AutoMapperConfig.Mapper.Map<MediaTypeResponseModel>).ToList();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}