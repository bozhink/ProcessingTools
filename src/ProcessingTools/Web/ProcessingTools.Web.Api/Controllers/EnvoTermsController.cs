namespace ProcessingTools.Web.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using ProcessingTools.Bio.Environments.Services.Data.Contracts;
    using ProcessingTools.Constants;
    using ProcessingTools.Web.Api.Models.EnvoTerms;

    public class EnvoTermsController : ApiController
    {
        private readonly IEnvoTermsDataService service;

        public EnvoTermsController(IEnvoTermsDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IHttpActionResult> GetEnvoTerms(int skip = PagingConstants.DefaultSkip, int take = PagingConstants.DefaultTake)
        {
            try
            {
                var data = await this.service.Get(skip, take);
                if (data == null)
                {
                    return this.NotFound();
                }

                var result = data.Select(AutoMapperConfig.Mapper.Map<EnvoTermResponseModel>).ToList();

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.ToString());
            }
        }
    }
}
