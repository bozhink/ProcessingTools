namespace ProcessingTools.Bio.Services.Data.Models
{
    using Contracts;

    public class TypeStatusResponseModel : ITypeStatus
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
