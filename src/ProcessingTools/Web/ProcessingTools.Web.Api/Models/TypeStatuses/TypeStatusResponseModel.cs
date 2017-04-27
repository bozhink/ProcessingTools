namespace ProcessingTools.Web.Api.Models.TypeStatuses
{
    using Bio.Services.Data.Models;
    using Mappings.Contracts;

    public class TypeStatusResponseModel : IMapFrom<TypeStatusServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
