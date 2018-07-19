// <copyright file="UtmCoordinatesConverterIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Geo.Tests.Integration.Tests
{
    using NUnit.Framework;
    using ProcessingTools.Geo;

    /// <summary>
    /// <see cref="UtmCoordinatesConverter "/> Integration Tests.
    /// </summary>
    [TestFixture(Author = "Bozhin Karaivanov", Category = "Integration", TestOf = typeof(UtmCoordinatesConverter))]
    public class UtmCoordinatesConverterIntegrationTests
    {
        /// <summary>
        /// <see cref="UtmCoordinatesConverter"/> TransformUtm2Decimal should work.
        /// </summary>
        /// <param name="utmZone">UTM Zone.</param>
        /// <param name="utmEasting">>UTM Easting.</param>
        /// <param name="utmNorthing">UTM Northing.</param>
        /// <param name="latitude">Expected value for latitude.</param>
        /// <param name="longitude">Expected value for longitude</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(UtmCoordinatesConverter), Description = "UtmCoordinatesConverter.TransformUtm2Decimal should work.")]
        [TestCase("33T", 455000, 4683000, 42.297928, 14.454084)]
        [TestCase("33T", 449800, 6745820, 40.614422, 17.063799)]
        [TestCase("33T", 674582, 4498003, 40.614422, 17.063799)]
        [TestCase("33T", 674582, 4498003, 40.614422, 17.063799)]
        [TestCase("55G", 595500, 5371700, -41.800816, 148.149549)]
        public void UtmCoordinatesConverter_TransformUtm2Decimal_ShouldWork(string utmZone, double utmEasting, double utmNorthing, double latitude, double longitude)
        {
            // Arrange
            UtmCoordinatesConverter converter = new UtmCoordinatesConverter();

            // Act
            double[] coordinates = converter.TransformUtm2Decimal(utmEasting, utmNorthing, utmZone);

            // Assert
            Assert.AreEqual(2, coordinates.Length, "Coordinate pair should contain two elements.");
            Assert.AreEqual(latitude, coordinates[0], "Latitude should match");
            Assert.AreEqual(longitude, coordinates[1], "Longitude should match");
        }
    }
}
