namespace ProcessingTools.Web.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using AutoMapper.QueryableExtensions;
    using Mediatype.Services.Data.Contracts;
    using Models.MediatypeModels;

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
            var result = this.mediatypeService
                ?.GetMediatype(extension)
                ?.ProjectTo<MediatypeResponseModel>()
                .ToList();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}