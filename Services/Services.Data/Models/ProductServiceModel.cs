namespace ProcessingTools.Services.Data.Models
{
    using Contracts;

    public class ProductServiceModel : IProductServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}