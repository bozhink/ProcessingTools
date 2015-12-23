namespace ProcessingTools.Services.Data.Models
{
    using Contracts;

    public class ProductResponseModel : IProduct
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}