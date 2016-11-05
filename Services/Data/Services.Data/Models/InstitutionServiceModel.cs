namespace ProcessingTools.Services.Data.Models
{
    using ProcessingTools.Contracts;

    public class InstitutionServiceModel : INameableIntegerIdentifiable
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
