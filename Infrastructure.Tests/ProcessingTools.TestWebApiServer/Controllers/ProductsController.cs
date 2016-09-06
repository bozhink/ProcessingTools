namespace ProcessingTools.TestWebApiServer.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using ProcessingTools.TestWebApiServer.Models;

    /// <summary>
    /// See http://www.asp.net/web-api/overview/older-versions/self-host-a-web-api
    /// and http://www.asp.net/web-api/overview/testing-and-debugging/unit-testing-with-aspnet-web-api
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
                new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
            };
        }

        public ProductsController()
        {
        }

        [HttpPost]
        public async Task<IHttpActionResult> Add(Product product)
        {
            if (product == null)
            {
                return await Task.FromResult(this.BadRequest());
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
                Price = product.Price
            };

            Products.Add(entity);
            return await Task.FromResult(this.Created(entity.Id.ToString(), entity));
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAll()
        {
            Products.Clear();
            return await Task.FromResult(this.Ok());
        }

        // /api/products
        public async Task<IHttpActionResult> GetAllProducts()
        {
            return await Task.FromResult(this.Ok(Products));
        }

        // /api/products/id
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            var product = Products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return await Task.FromResult(this.NotFound());
            }

            return await Task.FromResult(this.Ok(product));
        }

        // /api/products/?category=category
        public async Task<IHttpActionResult> GetProductsByCategory(string category)
        {
            var result = Products.Where(p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));

            return await Task.FromResult(this.Ok(result));
        }
    }
}
