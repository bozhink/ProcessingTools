namespace ProcessingTools.Web.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using AutoMapper.QueryableExtensions;
    using MediaType.Services.Data.Contracts;
    using Models.MediaTypeModels;

    public class MediaTypeController : ApiController
    {
        private IMediaTypeDataService mediatypeDataService;

        public MediaTypeController(IMediaTypeDataService mediatypeService)
        {
            this.mediatypeDataService = mediatypeService;
        }

        [Route("api/mediatype/{extension}")]
        public IHttpActionResult Get(string extension)
        {
            var result = this.mediatypeDataService?.GetMediaType(extension)?.ProjectTo<MediaTypeResponseModel>().ToList();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}