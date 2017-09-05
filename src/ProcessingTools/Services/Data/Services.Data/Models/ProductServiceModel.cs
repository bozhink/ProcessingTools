namespace ProcessingTools.Services.Data.Models
{
    using ProcessingTools.Contracts.Models.Resources;

    public class ProductServiceModel : IProduct
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
