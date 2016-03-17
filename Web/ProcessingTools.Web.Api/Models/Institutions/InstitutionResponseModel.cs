namespace ProcessingTools.Web.Api.Models.Institutions
{
    using Contracts.Mapping;
    using Services.Data.Models.Contracts;

    public class InstitutionResponseModel : IMapFrom<IInstitution>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}