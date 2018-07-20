// <copyright file="LatitudeLongitudeUtmConverterIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Geo.Tests.Integration.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// <see cref="LatitudeLongitudeUtmConverter"/> integration tests.
    /// </summary>
    [TestFixture(Author = "Bozhin Karaivanov", Category = "Integration", TestOf = typeof(LatitudeLongitudeUtmConverter))]
    public class LatitudeLongitudeUtmConverterIntegrationTests
    {
        /// <summary>
        /// <see cref="LatitudeLongitudeUtmConverter"/> Convert to UTM should work.
        /// </summary>
        /// <param name="zoneNumber">Expected value for the zone number.</param>
        /// <param name="zoneLetter">Expected value for the zone letter.</param>
        /// <param name="utmEasting">>Expected value for the UTM Easting.</param>
        /// <param name="utmNorthing">Expected value for the UTM Northing.</param>
        /// <param name="latitude">Value for the latitude.</param>
        /// <param name="longitude">Value for the longitude</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(LatitudeLongitudeUtmConverter), Description = "LatitudeLongitudeUtmConverter.Convert to UTM should work.")]
        [TestCase(33, "T", 455000, 4683000, 42.297928, 14.454084)]
        [TestCase(33, "T", 674582, 4498003, 40.614422, 17.063799)]
        [TestCase(55, "G", 595500, 5371700, -41.800816, 148.149549)]
        public void LatitudeLongitudeUtmConverter_ConvertToUtm_ShouldWork(int zoneNumber, string zoneLetter, double utmEasting, double utmNorthing, double latitude, double longitude)
        {
            // Arrange
            double epsilon = 1;
            LatitudeLongitudeUtmConverter converter = new LatitudeLongitudeUtmConverter(null);

            // Act
            var result = converter.Convert(latitude, longitude);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(zoneNumber, result.ZoneNumber, "ZoneNumber should match.");
            Assert.AreEqual(zoneLetter, result.ZoneLetter, "ZoneLetter should match.");
            Assert.AreEqual(utmNorthing, result.Northing, epsilon, "Northing should match.");
            Assert.AreEqual(utmEasting, result.Easting, epsilon, "Easting should match.");
        }

        /// <summary>
        /// <see cref="LatitudeLongitudeUtmConverter"/> Convert to decimal should work.
        /// </summary>
        /// <param name="zoneNumber">Zone number.</param>
        /// <param name="zoneLetter">Zone letter.</param>
        /// <param name="utmEasting">>UTM Easting.</param>
        /// <param name="utmNorthing">UTM Northing.</param>
        /// <param name="latitude">Expected value for the latitude.</param>
        /// <param name="longitude">Expected value for the longitude</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(LatitudeLongitudeUtmConverter), Description = "LatitudeLongitudeUtmConverter.Convert to decimal should work.")]
        [TestCase(33, "T", 455000, 4683000, 42.297928, 14.454084)]
        [TestCase(33, "T", 674582, 4498003, 40.614422, 17.063799)]
        [TestCase(55, "G", 595500, 5371700, -41.800816, 148.149549)]
        public void LatitudeLongitudeUtmConverter_ConvertToDecimal_ShouldWork(int zoneNumber, string zoneLetter, double utmEasting, double utmNorthing, double latitude, double longitude)
        {
            // Arrange
            double epsilon = 1e-6;
            LatitudeLongitudeUtmConverter converter = new LatitudeLongitudeUtmConverter();

            // Act
            var result = converter.Convert(utmEasting: utmEasting, utmNorthing: utmNorthing, utmZoneNumber: zoneNumber, utmZoneLetter: zoneLetter);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(longitude, result.Longitude, epsilon, "Longitude should match.");
            Assert.AreEqual(latitude, result.Latitude, epsilon, "Latitude should match.");
        }
    }
}
