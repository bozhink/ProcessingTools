namespace ProcessingTools.Net.Tests.Integration.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Owin.Hosting;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using ProcessingTools.Extensions;
    using ProcessingTools.Net.Tests.Models;
    using ProcessingTools.Services.Net;
    using ProcessingTools.TestWebApiServer;

    /// <summary>
    /// <see cref="HttpRequester"/> integration tests.
    /// </summary>
    [TestFixture]
    public class HttpRequesterIntegrationTests
    {
        private const string BaseAddress = "http://localhost:5324/";

        private IDisposable server;

        /// <summary>
        /// Set-up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.server = WebApp.Start<Startup>(BaseAddress);
        }

        /// <summary>
        /// Tear-down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.server.Dispose();
        }

        /// <summary>
        /// <see cref="HttpRequester"/> get JSON as string with valid parameters should work.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="checkString">Expected resultant string.</param>
        /// <returns>Task.</returns>
        [TestCase("/api/products/1", @"{""Id"":1,")]
        [TestCase("/api/products/2", @"{""Id"":2,")]
        [TestCase("/api/products/3", @"{""Id"":3,")]
        [TestCase("/api/products", @"[{""Id"":1,")]
        [Timeout(1000)]
        public async Task HttpRequester_GetJsonAsString_WithValidParameters_ShouldWork(string url, string checkString)
        {
            // Arrange
            var requestUri = UriExtensions.Append(BaseAddress, url);
            var requester = new HttpRequester();

            // Act
            var content = await requester.GetStringAsync(requestUri, "application/json").ConfigureAwait(false);

            // Assert
            Assert.IsTrue(content.Contains(checkString));
        }

        /// <summary>
        /// <see cref="HttpRequester"/> get deserialized JSON with valid parameters should work.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="id">Expected value of the ID.</param>
        /// <param name="name">Expected value of the name.</param>
        /// <param name="category">Expected value of the category.</param>
        /// <param name="price">Expected value of the price.</param>
        /// <returns>Task.</returns>
        [TestCase("/api/products/1", 1, "Tomato Soup", "Groceries", 1)]
        [TestCase("/api/products/2", 2, "Yo-yo", "Toys", 3.75)]
        [TestCase("/api/products/3", 3, "Hammer", "Hardware", 16.99)]
        [Timeout(5000)]
        public async Task HttpRequester_GetDeserializedJson_WithValidParameters_ShouldWork(string url, int id, string name, string category, decimal price)
        {
            // Arrange
            var requestUri = UriExtensions.Append(BaseAddress, url);
            var requester = new HttpRequester();

            // Act
            var responseObject = await requester.GetJsonToObjectAsync<Product>(requestUri).ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(responseObject);
            Assert.AreEqual(id, responseObject.Id);
            Assert.AreEqual(name, responseObject.Name);
            Assert.AreEqual(category, responseObject.Category);
            Assert.AreEqual(price, responseObject.Price);
        }

        /// <summary>
        /// <see cref="HttpRequester"/> get deserialized JSON array with valid parameters should work.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="numberOfItems">Expected number of items.</param>
        /// <returns>Task.</returns>
        [TestCase("/api/products", 3)]
        [Timeout(1000)]
        public async Task HttpRequester_GetDeserializedJsonArray_WithValidParameters_ShouldWork(string url, int numberOfItems)
        {
            // Arrange
            var requestUri = UriExtensions.Append(BaseAddress, url);
            var requester = new HttpRequester();

            // Act
            var responseObject = await requester.GetJsonToObjectAsync<Product[]>(requestUri).ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(responseObject);
            Assert.AreEqual(numberOfItems, responseObject.Length);

            for (int i = 0; i < numberOfItems; ++i)
            {
                int expected = i + 1;
                Assert.AreEqual(expected, responseObject[i].Id, $"Id should be {expected}.");
            }
        }

        /// <summary>
        /// <see cref="HttpRequester"/> get XML as string with valid parameters should work.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="checkString">Expected value.</param>
        /// <returns>Task.</returns>
        [TestCase("/api/products/1", @"<Id>1</Id>")]
        [TestCase("/api/products/2", @"<Id>2</Id>")]
        [TestCase("/api/products/3", @"<Id>3</Id>")]
        [TestCase("/api/products", @"</Product><Product")]
        [Timeout(1000)]
        public async Task HttpRequester_GetXmlAsString_WithValidParameters_ShouldWork(string url, string checkString)
        {
            // Arrange
            var requestUri = UriExtensions.Append(BaseAddress, url);
            var requester = new HttpRequester();

            // Act
            var content = await requester.GetStringAsync(requestUri, "application/xml").ConfigureAwait(false);

            // Assert
            Assert.IsTrue(content.Contains(checkString));
        }

        /// <summary>
        /// <see cref="HttpRequester"/> get deserialized XML with valid parameters should work.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="id">Expected value of the ID.</param>
        /// <param name="name">Expected value of the name.</param>
        /// <param name="category">Expected value of the category.</param>
        /// <param name="price">Expected value of the price.</param>
        /// <returns>Task.</returns>
        [TestCase("/api/products/1", 1, "Tomato Soup", "Groceries", 1)]
        [TestCase("/api/products/2", 2, "Yo-yo", "Toys", 3.75)]
        [TestCase("/api/products/3", 3, "Hammer", "Hardware", 16.99)]
        [Timeout(1000)]
        public async Task HttpRequester_GetDeserializedXml_WithValidParameters_ShouldWork(string url, int id, string name, string category, decimal price)
        {
            // Arrange
            var requestUri = UriExtensions.Append(BaseAddress, url);
            var requester = new HttpRequester();

            // Act
            var responseObject = await requester.GetXmlToObjectAsync<Product>(requestUri).ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(responseObject);
            Assert.AreEqual(id, responseObject.Id);
            Assert.AreEqual(name, responseObject.Name);
            Assert.AreEqual(category, responseObject.Category);
            Assert.AreEqual(price, responseObject.Price);
        }

        /// <summary>
        /// <see cref="HttpRequester"/> get deserialized XML array with valid parameters should work.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="numberOfItems">Expected number of items.</param>
        /// <returns>Task.</returns>
        [TestCase("/api/products", 3)]
        [Timeout(1000)]
        public async Task HttpRequester_GetDeserializedXmlArray_WithValidParameters_ShouldWork(string url, int numberOfItems)
        {
            // Arrange
            var requestUri = UriExtensions.Append(BaseAddress, url);
            var requester = new HttpRequester();

            // Act
            var responseObject = await requester.GetXmlToObjectAsync<ArrayOfProduct>(requestUri).ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(responseObject);
            Assert.AreEqual(numberOfItems, responseObject.Products.Length);

            for (int i = 0; i < numberOfItems; ++i)
            {
                int expected = i + 1;
                Assert.AreEqual(expected, responseObject.Products[i].Id, $"Id should be {expected}.");
            }
        }

        /// <summary>
        /// <see cref="HttpRequester"/> post dictionary with valid parameters should work.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="name">Expected value of the name.</param>
        /// <param name="category">Expected value of the category.</param>
        /// <param name="price">Expected value of the price.</param>
        /// <returns>Task.</returns>
        [TestCase("/api/products/add", "Tomato Soup - 1", "Groceries", 1)]
        [TestCase("/api/products/add", "Yo-yo - 1", "Toys", 3.75)]
        [TestCase("/api/products/add", "Hammer - 1", "Hardware", 16.99)]
        [Timeout(1000)]
        public async Task HttpRequester_PostDictionary_WithValidParameters_ShouldWork(string url, string name, string category, decimal price)
        {
            // Arrange
            var requestUri = UriExtensions.Append(BaseAddress, url);
            var requester = new HttpRequester();
            var values = new Dictionary<string, string>
            {
                { "name", name },
                { "category", category },
                { "price", price.ToString(CultureInfo.InvariantCulture) },
            };

            // Act
            var response = await requester.PostToStringAsync(requestUri, values, Encoding.UTF8).ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Contains(name));
            Assert.IsTrue(response.Contains(category));
            Assert.IsTrue(response.Contains(price.ToString(CultureInfo.InvariantCulture)));
        }

        /// <summary>
        /// <see cref="HttpRequester"/> post string with valid parameters should work.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="name">Expected value of the name.</param>
        /// <param name="category">Expected value of the category.</param>
        /// <param name="price">Expected value of the price.</param>
        /// <returns>Task.</returns>
        [TestCase("/api/products/add", "Tomato Soup - 1", "Groceries", 1)]
        [TestCase("/api/products/add", "Yo-yo - 1", "Toys", 3.75)]
        [TestCase("/api/products/add", "Hammer - 1", "Hardware", 16.99)]
        [Timeout(1000)]
        public async Task HttpRequester_PostString_WithValidParameters_ShouldWork(string url, string name, string category, decimal price)
        {
            // Arrange
            var product = new Product
            {
                Name = name,
                Category = category,
                Price = price,
            };

            string content = JsonConvert.SerializeObject(product);

            var requestUri = UriExtensions.Append(BaseAddress, url);
            var requester = new HttpRequester();

            // Act
            var response = await requester.PostAsync(requestUri, content, "application/json", Encoding.UTF8).ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Contains(name));
            Assert.IsTrue(response.Contains(category));
            Assert.IsTrue(response.Contains(price.ToString(CultureInfo.InvariantCulture)));

            TestContext.WriteLine(content);
            TestContext.WriteLine(response);
        }

        /// <summary>
        /// <see cref="HttpRequester"/> post and deserialize dictionary as XML with valid parameters should work.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="name">Expected value of the name.</param>
        /// <param name="category">Expected value of the category.</param>
        /// <param name="price">Expected value of the price.</param>
        /// <returns>Task.</returns>
        [TestCase("/api/products/add", "Tomato Soup - 1", "Groceries", 1)]
        [TestCase("/api/products/add", "Yo-yo - 1", "Toys", 3.75)]
        [TestCase("/api/products/add", "Hammer - 1", "Hardware", 16.99)]
        [Timeout(1000)]
        public async Task HttpRequester_PostAndDeserializeDictionaryAsXml_WithValidParameters_ShouldWork(string url, string name, string category, decimal price)
        {
            // Arrange
            var requestUri = UriExtensions.Append(BaseAddress, url);
            var requester = new HttpRequester();
            var values = new Dictionary<string, string>
            {
                { "name", name },
                { "category", category },
                { "price", price.ToString(CultureInfo.InvariantCulture) },
            };

            // Act
            var responseObject = await requester.PostToXmlToObjectAsync<Product>(requestUri, values, Encoding.UTF8).ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(responseObject);
            Assert.AreEqual(name, responseObject.Name);
            Assert.AreEqual(category, responseObject.Category);
            Assert.AreEqual(price, responseObject.Price);
        }
    }
}
