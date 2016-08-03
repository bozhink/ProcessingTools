namespace ProcessingTools.Data.Miners.Tests.RegressionTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Fakes;

    [TestClass]
    public class ProductsDataMinerRegressionTests
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
        public void ProductsDataMiner_WithNullServiceInConstructor_ShouldThrow()
        {
            var miner = new ProductsDataMiner(null);
        }

        [TestMethod]
        public void ProductsDataMiner_WithValidServiceInConstructor_ShouldReturnValidMiner()
        {
            var miner = new ProductsDataMiner(service);

            Assert.IsNotNull(miner, "Miner should not be null.");
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void ProductsDataMiner_MineEmptyContent_ShouldThrow()
        {
            string content = " ";

            var miner = new ProductsDataMiner(service);
            var data = miner.Mine(content).Result;
        }

        [TestMethod]
        public void ProductsDataMiner_MineEmptyContent_ShouldThrowWithInnerArgumentNullException()
        {
            string content = "  ";

            var miner = new ProductsDataMiner(service);
            try
            {
                var data = miner.Mine(content).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(1, e.InnerExceptions.Count, "Number of inner exceptions should be 1.");
                Assert.AreEqual(
                    typeof(ArgumentNullException).FullName,
                    e.InnerExceptions.FirstOrDefault().GetType().FullName,
                    "Inner exception should be ArgumentNullException");
            }
        }

        [TestMethod]
        public void ProductsDataMiner_MineContentWithNoMatchingItems_ShouldReturnEmptyResult()
        {
            string content = " no matching content here ";

            var miner = new ProductsDataMiner(service);
            var data = miner.Mine(content).Result.ToList();

            Assert.AreEqual(0, data.Count, "Data should be empty.");
        }

        [TestMethod]
        public void ProductsDataMiner_MineContentWithMatchingFirstItem_ShouldHaveCorrectNumberOfDataItems()
        {
            string item = dataItems.FirstOrDefault();
            int numberOfMatchingDataItems = dataItems.Where(i => item.Contains(i)).ToList().Count;

            string content = item;

            var miner = new ProductsDataMiner(service);

            var data = miner.Mine(content).Result.ToList();

            Assert.AreEqual(numberOfMatchingDataItems, data.Count, $"Data lenght should be {numberOfMatchingDataItems}");

            Assert.IsTrue(data.Contains(item), "Resultant data should contain input item.");
        }

        [TestMethod]
        public void ProductsDataMiner_MineContentWithMatchingLastItem_ShouldHaveCorrectNumberOfDataItems()
        {
            string item = dataItems.LastOrDefault();
            int numberOfMatchingDataItems = dataItems.Where(i => item.Contains(i)).ToList().Count;

            string content = item;

            var miner = new ProductsDataMiner(service);

            var data = miner.Mine(content).Result.ToList();

            Assert.AreEqual(numberOfMatchingDataItems, data.Count, $"Data lenght should be {numberOfMatchingDataItems}");

            Assert.IsTrue(data.Contains(item), "Resultant data should contain input item.");
        }
    }
}