namespace ProcessingTools.Web.Api.Models.TypeStatuses
{
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Contracts.Models;

    public class TypeStatusResponseModel : IMapFrom<TypeStatusServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
