namespace ProcessingTools.Web.Api.Models.TypeStatuses
{
    using Bio.Services.Data.Models.Contracts;
    using Mappings.Contracts;

    public class TypeStatusResponseModel : IMapFrom<ITypeStatusServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
