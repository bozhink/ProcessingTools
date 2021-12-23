// <copyright file="CoordinatesDataMinerTests.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Tests.Integration.Tests.Geo.Coordinates
{
    using System.Linq;
    using NUnit.Framework;
    using ProcessingTools.Services.Geo;

    /// <summary>
    /// <see cref="CoordinatesDataMiner"/> tests.
    /// </summary>
    [TestFixture]
    public class CoordinatesDataMinerTests
    {
        /// <summary>
        /// <see cref="CoordinatesDataMiner"/> with default constructor should return valid object.
        /// </summary>
        [Test]
        public void CoordinatesDataMiner_WithDefaultConstructor_ShouldReturnValidObject()
        {
            var miner = new CoordinatesDataMiner();
            Assert.IsNotNull(miner, "Miner should not be null.");
        }

        /// <summary>
        /// <see cref="CoordinatesDataMiner"/> with decimal deg direction coordinates should work.
        /// </summary>
        [Test]
        public void CoordinatesDataMiner_WithDecimalDegDirectionCoordinates_ShouldWork()
        {
            string content = "24°03.008N, 105°43.147E";
            var miner = new CoordinatesDataMiner();
            Assert.IsNotNull(miner, "Miner should not be null.");

            var result = miner.MineAsync(content).Result.ToList();
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(1, result.Count, "Number of items in result should be 1.");
            Assert.AreEqual(content, result[0], "Content should match.");
        }
    }
}
