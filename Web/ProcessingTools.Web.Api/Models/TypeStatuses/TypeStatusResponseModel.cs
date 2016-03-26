namespace ProcessingTools.Web.Api.Models.TypeStatuses
{
    using Bio.Services.Data.Models.Contracts;
    using ProcessingTools.Contracts.Mapping;

    public class TypeStatusResponseModel : IMapFrom<ITypeStatusServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
