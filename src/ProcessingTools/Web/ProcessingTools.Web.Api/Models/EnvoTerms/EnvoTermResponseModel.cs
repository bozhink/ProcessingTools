namespace ProcessingTools.Web.Api.Models.EnvoTerms
{
    using ProcessingTools.Bio.Environments.Services.Data.Models;
    using ProcessingTools.Contracts.Models;

    public class EnvoTermResponseModel : IMapFrom<EnvoTermServiceModel>
    {
        public string EntityId { get; set; }

        public string EnvoId { get; set; }

        public string Content { get; set; }
    }
}
