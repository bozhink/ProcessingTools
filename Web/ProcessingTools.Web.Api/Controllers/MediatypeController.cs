namespace ProcessingTools.Web.Api.Controllers
{
    using Mediatype.Services.Data.Contracts;
    using Mediatype.Services.Data;
    using System.Web.Http;
    using Data.Common.Repositories;
    using Mediatype.Data.Models;
    using Mediatype.Data;

    public class MediatypeController : ApiController
    {
        private IMediatypeService mediatypeService;

        public MediatypeController(IMediatypeService mediatypeService)
        {
            this.mediatypeService = mediatypeService;
        }

        [Route("api/Mediatype/{extension}")]
        public IHttpActionResult Get(string extension)
        {
            var result = this.mediatypeService.GetMediatype(extension);

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}