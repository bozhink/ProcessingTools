namespace ProcessingTools.Services.Data.Models
{
    using ProcessingTools.Services.Common.Models.Contracts;

    public class ProductServiceModel : INamedDataServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}