namespace ProcessingTools.Services.Data.Models
{
    using ProcessingTools.Contracts.Models.Resources;

    public class InstitutionServiceModel : IInstitution
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
