namespace ProcessingTools.Net.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.Owin.Hosting;

    using Models;

    using Newtonsoft.Json;
    using NUnit.Framework;

    using ProcessingTools.Net;
    using ProcessingTools.TestWebApiServer;

    [TestFixture]
    public class NetConnectorIntegrationTests
    {
        private const string BaseAddress = "http://localhost:5324/";

        private IDisposable server;

        [SetUp]
        public void SetUp()
        {
            this.server = WebApp.Start<Startup>(BaseAddress);
        }

        [TearDown]
        public void TearDown()
        {
            this.server.Dispose();
        }

        [TestCase("/api/products/1", @"{""Id"":1,")]
        [TestCase("/api/products/2", @"{""Id"":2,")]
        [TestCase("/api/products/3", @"{""Id"":3,")]
        [TestCase("/api/products", @"[{""Id"":1,")]
        [Timeout(1000)]
        public void NetConnector_GetJsonAsString_WithValidParameters_ShouldWork(string url, string checkString)
        {
            var connector = new NetConnector(BaseAddress);
            var content = connector.Get(url, "application/json").Result;
            Assert.IsTrue(content.Contains(checkString), "Content of the response should contain {0}", checkString);
        }

        [TestCase("/api/products/1", 1, "Tomato Soup", "Groceries", 1)]
        [TestCase("/api/products/2", 2, "Yo-yo", "Toys", 3.75)]
        [TestCase("/api/products/3", 3, "Hammer", "Hardware", 16.99)]
        [Timeout(1000)]
        public void NetConnector_GetDeserializedJson_WithValidParameters_ShouldWork(string url, int id, string name, string category, decimal price)
        {
            var connector = new NetConnector(BaseAddress);
            var responseObject = connector.GetAndDeserializeDataContractJson<Product>(url).Result;
            Assert.IsNotNull(responseObject, "Response object should not be null.");
            Assert.AreEqual(id, responseObject.Id, "Id should match.");
            Assert.AreEqual(name, responseObject.Name, "Name should match.");
            Assert.AreEqual(category, responseObject.Category, "Category should match.");
            Assert.AreEqual(price, responseObject.Price, "Price should match.");
        }

        [TestCase("/api/products", 3)]
        [Timeout(1000)]
        public void NetConnector_GetDeserializedJsonArray_WithValidParameters_ShouldWork(string url, int numberOfItems)
        {
            var connector = new NetConnector(BaseAddress);
            var responseObject = connector.GetAndDeserializeDataContractJson<Product[]>(url).Result;
            Assert.IsNotNull(responseObject, "Response object should not be null.");
            Assert.AreEqual(numberOfItems, responseObject.Length, "Number of items should match.");

            for (int i = 0; i < numberOfItems; ++i)
            {
                Assert.AreEqual(i + 1, responseObject[i].Id, "Id should be {0}.", i + 1);
            }
        }

        [TestCase("/api/products/1", @"<Id>1</Id>")]
        [TestCase("/api/products/2", @"<Id>2</Id>")]
        [TestCase("/api/products/3", @"<Id>3</Id>")]
        [TestCase("/api/products", @"</Product><Product")]
        [Timeout(1000)]
        public void NetConnector_GetXmlAsString_WithValidParameters_ShouldWork(string url, string checkString)
        {
            var connector = new NetConnector(BaseAddress);
            var content = connector.Get(url, "application/xml").Result;
            Assert.IsTrue(content.Contains(checkString), "Content of the response should contain {0}", checkString);
        }

        [TestCase("/api/products/1", 1, "Tomato Soup", "Groceries", 1)]
        [TestCase("/api/products/2", 2, "Yo-yo", "Toys", 3.75)]
        [TestCase("/api/products/3", 3, "Hammer", "Hardware", 16.99)]
        [Timeout(1000)]
        public void NetConnector_GetDeserializedXml_WithValidParameters_ShouldWork(string url, int id, string name, string category, decimal price)
        {
            var connector = new NetConnector(BaseAddress);
            var responseObject = connector.GetAndDeserializeXml<Product>(url).Result;
            Assert.IsNotNull(responseObject, "Response object should not be null.");
            Assert.AreEqual(id, responseObject.Id, "Id should match.");
            Assert.AreEqual(name, responseObject.Name, "Name should match.");
            Assert.AreEqual(category, responseObject.Category, "Category should match.");
            Assert.AreEqual(price, responseObject.Price, "Price should match.");
        }

        [TestCase("/api/products", 3)]
        [Timeout(1000)]
        public void NetConnector_GetDeserializedXmlArray_WithValidParameters_ShouldWork(string url, int numberOfItems)
        {
            var connector = new NetConnector(BaseAddress);

            var responseObject = connector.GetAndDeserializeXml<ArrayOfProduct>(url).Result;
            Assert.IsNotNull(responseObject, "Response object should not be null.");
            Assert.AreEqual(numberOfItems, responseObject.Products.Length, "Number of items should match.");

            for (int i = 0; i < numberOfItems; ++i)
            {
                Assert.AreEqual(i + 1, responseObject.Products[i].Id, "Id should be {0}.", i + 1);
            }
        }

        [TestCase("/api/products/add", "Tomato Soup - 1", "Groceries", 1)]
        [TestCase("/api/products/add", "Yo-yo - 1", "Toys", 3.75)]
        [TestCase("/api/products/add", "Hammer - 1", "Hardware", 16.99)]
        [Timeout(1000)]
        public void NetConnector_PostDictionary_WithValidParameters_ShouldWork(string url, string name, string category, decimal price)
        {
            var connector = new NetConnector(BaseAddress);

            var response = connector.Post(
                url,
                new Dictionary<string, string>
                {
                    { "name", name },
                    { "category", category },
                    { "price", price.ToString() }
                },
                Encoding.UTF8).Result;

            Assert.IsNotNull(response, "Response should not be null.");
            Assert.IsTrue(response.Contains(name), "Response should contain the name.");
            Assert.IsTrue(response.Contains(category), "Response should contain the category.");
            Assert.IsTrue(response.Contains(price.ToString()), "Response should contain the price.");
        }

        [TestCase("/api/products/add", "Tomato Soup - 1", "Groceries", 1)]
        [TestCase("/api/products/add", "Yo-yo - 1", "Toys", 3.75)]
        [TestCase("/api/products/add", "Hammer - 1", "Hardware", 16.99)]
        [Timeout(1000)]
        public void NetConnector_PostString_WithValidParameters_ShouldWork(string url, string name, string category, decimal price)
        {
            var product = new Product
            {
                Name = name,
                Category = category,
                Price = price
            };

            string content = JsonConvert.SerializeObject(product);

            var connector = new NetConnector(BaseAddress);
            var response = connector.Post(url, content, "application/json", Encoding.UTF8).Result;

            Assert.IsNotNull(response, "Response should not be null.");
            Assert.IsTrue(response.Contains(name), "Response should contain the name.");
            Assert.IsTrue(response.Contains(category), "Response should contain the category.");
            Assert.IsTrue(response.Contains(price.ToString()), "Response should contain the price.");

            Console.WriteLine(content);
            Console.WriteLine(response);
        }

        [TestCase("/api/products/add", "Tomato Soup - 1", "Groceries", 1)]
        [TestCase("/api/products/add", "Yo-yo - 1", "Toys", 3.75)]
        [TestCase("/api/products/add", "Hammer - 1", "Hardware", 16.99)]
        [Timeout(1000)]
        public void NetConnector_PostAndDeserializeDictionaryAsXml_WithValidParameters_ShouldWork(string url, string name, string category, decimal price)
        {
            var connector = new NetConnector(BaseAddress);

            var responseObject = connector.PostAndDeserializeXml<Product>(
                url,
                new Dictionary<string, string>
                {
                    { "name", name },
                    { "category", category },
                    { "price", price.ToString() }
                },
                Encoding.UTF8).Result;

            Assert.IsNotNull(responseObject, "Response should not be null.");
            Assert.AreEqual(name, responseObject.Name, "Name should match.");
            Assert.AreEqual(category, responseObject.Category, "Category should match.");
            Assert.AreEqual(price, responseObject.Price, "Price should match.");
        }
    }
}
