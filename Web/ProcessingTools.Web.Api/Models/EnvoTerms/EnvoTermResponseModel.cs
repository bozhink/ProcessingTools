namespace ProcessingTools.Web.Api.Models.EnvoTerms
{
    using Bio.Environments.Services.Data.Models.Contracts;
    using Contracts.Mapping;

    public class EnvoTermResponseModel : IMapFrom<IEnvoTermServiceModel>
    {
        public string EntityId { get; set; }

        public string EnvoId { get; set; }

        public string Content { get; set; }
    }
}