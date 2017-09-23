namespace ProcessingTools.Services.Data.Models
{
    using ProcessingTools.Models.Contracts.Resources;

    public class ProductServiceModel : IProduct
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
