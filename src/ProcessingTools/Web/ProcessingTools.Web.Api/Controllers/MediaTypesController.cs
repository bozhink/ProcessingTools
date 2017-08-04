namespace ProcessingTools.Web.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using ProcessingTools.Services.Data.Contracts.Mediatypes;
    using ProcessingTools.Web.Api.Models.MediaTypes;

    public class MediaTypesController : ApiController
    {
        private readonly IMediatypesResolver mediatypesResolver;

        public MediaTypesController(IMediatypesResolver mediatypesResolver)
        {
            this.mediatypesResolver = mediatypesResolver ?? throw new ArgumentNullException(nameof(mediatypesResolver));
        }

        public async Task<IHttpActionResult> Get(string id)
        {
            var result = (await this.mediatypesResolver.ResolveMediatype(id))?.Select(AutoMapperConfig.Mapper.Map<MediaTypeResponseModel>).ToList();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}
