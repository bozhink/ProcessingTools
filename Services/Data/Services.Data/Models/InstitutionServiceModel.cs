namespace ProcessingTools.Services.Data.Models
{
    using Contracts.Models;

    public class InstitutionServiceModel : IInstitution
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
