namespace ProcessingTools.Web.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Mediatypes.Services.Data.Contracts;
    using Models.MediaTypes;

    public class MediaTypeController : ApiController
    {
        private IMediaTypeDataService mediatypeDataService;

        public MediaTypeController(IMediaTypeDataService mediatypeService)
        {
            if (mediatypeService == null)
            {
                throw new ArgumentNullException(nameof(mediatypeService));
            }

            this.mediatypeDataService = mediatypeService;
        }

        public async Task<IHttpActionResult> Get(string id)
        {
            var result = (await this.mediatypeDataService.GetMediaType(id))?.Select(AutoMapperConfig.Mapper.Map<MediaTypeResponseModel>).ToList();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}