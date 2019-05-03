namespace ProcessingTools.TestWebApiServer.Models
{
    using System;

    /// <summary>
    /// Product model.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product()
        {
            this.Id = Guid.NewGuid().ToString().GetHashCode();
            this.DateAdded = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the ID of the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the data and time of addition of the product.
        /// </summary>
        public DateTime DateAdded { get; set; }
    }
}
