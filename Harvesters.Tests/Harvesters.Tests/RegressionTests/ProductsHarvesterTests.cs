namespace ProcessingTools.Harvesters.Tests.RegressionTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Fakes;

    [TestClass]
    public class ProductsHarvesterTests
    {
        private static FakeProductsDataService service;
        private static HashSet<string> dataItems;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            service = new FakeProductsDataService();
            dataItems = new HashSet<string>(service.Items
                .Select(i => i.Name));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProductsHarvester_WithNullServiceInConstructor_ShouldThrow()
        {
            var harvester = new ProductsHarvester(null);
        }

        [TestMethod]
        public void ProductsHarvester_WithValidServiceInConstructor_ShouldReturnValidHarvester()
        {
            var harvester = new ProductsHarvester(service);

            Assert.IsNotNull(harvester, "Harvester should not be null.");

            Assert.IsNotNull(harvester.Data.ToList(), "Data should not be null.");
        }

        [TestMethod]
        public void ProductsHarvester_WithNonRunnedHarvestMethod_ShouldHaveEmptyData()
        {
            var harvester = new ProductsHarvester(service);

            Assert.AreEqual(0, harvester.Data.ToList().Count, "Data should be empty.");
        }

        [TestMethod]
        public void ProductsHarvester_HarvestContentWithNoMatchingItems_ShouldHaveEmptyData()
        {
            string content = " ";

            var harvester = new ProductsHarvester(service);
            harvester.Harvest(content);

            Assert.AreEqual(0, harvester.Data.ToList().Count, "Data should be empty.");
        }

        [TestMethod]
        public void ProductsHarvester_HarvestContentWithMatchingFirstItem_ShouldHaveCorrectNumberOfDataItems()
        {
            string item = dataItems.FirstOrDefault();
            int numberOfMatchingDataItems = dataItems.Where(i => item.Contains(i)).ToList().Count;

            string content = item;

            var harvester = new ProductsHarvester(service);
            harvester.Harvest(content);

            var data = harvester.Data.ToList();

            Assert.AreEqual(numberOfMatchingDataItems, data.Count, $"Data lenght should be {numberOfMatchingDataItems}");

            Assert.IsTrue(data.Contains(item), "Resultant data should contain input item.");
        }

        [TestMethod]
        public void ProductsHarvester_HarvestContentWithMatchingLastItem_ShouldHaveCorrectNumberOfDataItems()
        {
            string item = dataItems.LastOrDefault();
            int numberOfMatchingDataItems = dataItems.Where(i => item.Contains(i)).ToList().Count;

            string content = item;

            var harvester = new ProductsHarvester(service);
            harvester.Harvest(content);

            var data = harvester.Data.ToList();

            Assert.AreEqual(numberOfMatchingDataItems, data.Count, $"Data lenght should be {numberOfMatchingDataItems}");

            Assert.IsTrue(data.Contains(item), "Resultant data should contain input item.");
        }
    }
}