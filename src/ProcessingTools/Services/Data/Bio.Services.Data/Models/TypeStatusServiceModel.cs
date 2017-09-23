namespace ProcessingTools.Bio.Services.Data.Models
{
    using ProcessingTools.Models.Contracts.Bio;

    public class TypeStatusServiceModel : ITypeStatus
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
