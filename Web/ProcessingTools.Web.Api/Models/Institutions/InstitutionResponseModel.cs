namespace ProcessingTools.Web.Api.Models.Institutions
{
    using Mappings.Contracts;
    using Services.Data.Models.Contracts;

    public class InstitutionResponseModel : IMapFrom<IInstitutionServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}