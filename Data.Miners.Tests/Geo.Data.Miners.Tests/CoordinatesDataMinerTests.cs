namespace ProcessingTools.Geo.Data.Miners.Tests
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CoordinatesDataMinerTests
    {
        [TestMethod]
        public void CoordinatesDataMiner_WithDefaultConstructor_ShouldReturnValidObject()
        {
            var miner = new CoordinatesDataMiner();
            Assert.IsNotNull(miner, "Miner should not be null.");
        }

        [TestMethod]
        public void CoordinatesDataMiner_WithDecimalDegDirectionCoordiantes_ShouldWork()
        {
            string content = "24°03.008N, 105°43.147E";
            var miner = new CoordinatesDataMiner();
            Assert.IsNotNull(miner, "Miner should not be null.");

            var result = miner.Mine(content).Result.ToList();
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(1, result.Count, "Number of items in result should be 1.");
            Assert.AreEqual(content, result[0], "Content should match.");
        }
    }
}