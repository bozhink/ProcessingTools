namespace ProcessingTools.Services.Data.Models
{
    using Contracts;

    public class InstitutionResponseModel : IInstitution
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}