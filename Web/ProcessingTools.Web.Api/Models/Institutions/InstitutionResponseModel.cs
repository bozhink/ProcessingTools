namespace ProcessingTools.Web.Api.Models.Institutions
{
    using Mappings.Contracts;
    using Services.Data.Models;

    public class InstitutionResponseModel : IMapFrom<InstitutionServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}