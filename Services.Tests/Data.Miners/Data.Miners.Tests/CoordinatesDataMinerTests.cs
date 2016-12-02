namespace ProcessingTools.Data.Miners.Tests
{
    using System.Linq;
    using NUnit.Framework;
    using ProcessingTools.Data.Miners;

    [TestFixture]
    public class CoordinatesDataMinerTests
    {
        [Test]
        public void CoordinatesDataMiner_WithDefaultConstructor_ShouldReturnValidObject()
        {
            var miner = new CoordinatesDataMiner();
            Assert.IsNotNull(miner, "Miner should not be null.");
        }

        [Test]
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
