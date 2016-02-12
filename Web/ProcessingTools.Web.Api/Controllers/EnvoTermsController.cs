namespace ProcessingTools.Web.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using Models.EnvoTerms;

    using ProcessingTools.Bio.Environments.Services.Data.Contracts;
    using ProcessingTools.Common.Constants;

    public class EnvoTermsController : ApiController
    {
        private IEnvoTermsDataService service;

        public EnvoTermsController(IEnvoTermsDataService service)
        {
            this.service = service;
        }

        public IHttpActionResult GetEnvoTerms(string skip = "0", string take = DefaultPagingConstants.DefaultTakeString)
        {
            int skipItemsCount;
            if (!int.TryParse(skip, out skipItemsCount))
            {
                return this.BadRequest(Messages.InvalidValueForSkipQueryParameterMessage);
            }

            int takeItemsCount;
            if (!int.TryParse(take, out takeItemsCount))
            {
                return this.BadRequest(Messages.InvalidValueForTakeQueryParameterMessage);
            }

            var result = this.service?.Get(skipItemsCount, takeItemsCount)
                .Select(AutoMapperConfig.Mapper.Map<EnvoTermResponseModel>)
                .ToList();

            if (result == null)
            {
                return this.InternalServerError();
            }

            return this.Ok(result);
        }
    }
}