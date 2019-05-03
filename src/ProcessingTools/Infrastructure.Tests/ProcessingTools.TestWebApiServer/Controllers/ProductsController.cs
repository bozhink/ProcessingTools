namespace ProcessingTools.TestWebApiServer.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using ProcessingTools.TestWebApiServer.Models;

    /// <summary>
    /// See [http://www.asp.net/web-api/overview/older-versions/self-host-a-web-api]
    /// and [http://www.asp.net/web-api/overview/testing-and-debugging/unit-testing-with-aspnet-web-api].
    /// </summary>
    public class ProductsController : ApiController
    {
        private static readonly ICollection<Product> Products;

        static ProductsController()
        {
            Products = new List<Product>
            {
                new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1M },
                new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
                new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M },
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        public ProductsController()
        {
        }

        /// <summary>
        /// Add new product to the list.
        /// </summary>
        /// <param name="product">Product item to be added.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public IHttpActionResult Add(Product product)
        {
            if (product == null)
            {
                return this.BadRequest();
            }

            int id = 1;
            if (Products.Count > 0)
            {
                id += Products.Max(p => p.Id);
            }

            var entity = new Product
            {
                Id = id,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price,
            };

            Products.Add(entity);
            return this.Created(entity.Id.ToString(), entity);
        }

        /// <summary>
        /// Delete all products.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpDelete]
        public IHttpActionResult DeleteAll()
        {
            Products.Clear();
            return this.Ok();
        }

        /// <summary>
        /// Gets all the products.
        /// </summary>
        /// <returns>Action result.</returns>
        public IHttpActionResult GetAllProducts()
        {
            return this.Ok(Products);
        }

        /// <summary>
        /// Gets a product specified by ID.
        /// </summary>
        /// <param name="id">ID of the product.</param>
        /// <returns>Action result.</returns>
        public IHttpActionResult GetProduct(int id)
        {
            var product = Products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return this.NotFound();
            }

            return this.Ok(product);
        }

        /// <summary>
        /// Gets products by category.
        /// </summary>
        /// <param name="category">Category of the products.</param>
        /// <returns>Action result.</returns>
        public IHttpActionResult GetProductsByCategory(string category)
        {
            var result = Products.Where(p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));

            return this.Ok(result);
        }
    }
}
