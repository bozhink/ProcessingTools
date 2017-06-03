namespace ProcessingTools.Web.Api.Models.Institutions
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Services.Data.Models;

    public class InstitutionResponseModel : IMapFrom<InstitutionServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
