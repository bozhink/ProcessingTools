namespace ProcessingTools.TestWebApiServer.Models
{
    using System;

    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString().GetHashCode();
            this.DateAdded = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
