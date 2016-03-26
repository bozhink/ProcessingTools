namespace ProcessingTools.Services.Data.Models
{
    using Contracts;

    public class InstitutionServiceModel : IInstitutionServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}