namespace ProcessingTools.Services.Data.Models
{
    using ProcessingTools.Services.Common.Models.Contracts;

    public class InstitutionServiceModel : INamedDataServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}