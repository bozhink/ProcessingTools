namespace ProcessingTools.Services.Data.Models
{
    using Contracts.Models;

    public class ProductServiceModel : IProduct
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
