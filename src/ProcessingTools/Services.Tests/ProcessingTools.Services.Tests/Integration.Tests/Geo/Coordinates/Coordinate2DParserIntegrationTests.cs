﻿// <copyright file="Coordinate2DParserIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Tests.Integration.Tests.Geo.Coordinates
{
    using System;
    using NUnit.Framework;
    using ProcessingTools.Services.Geo.Coordinates;
    using ProcessingTools.Services.Models.Geo.Coordinates;

    /// <summary>
    /// <see cref="Coordinate2DParser"/> integration tests.
    /// </summary>
    [TestFixture(Author = "Bozhin Karaivanov", Category = "Integration", TestOf = typeof(Coordinate2DParser))]
    public class Coordinate2DParserIntegrationTests
    {
        private Func<Coordinate2DParser> Coordinate2DParserFactory => () => new Coordinate2DParser(new UtmCoordinatesTransformer(new UtmCoordinatesConverter()));

        /// <summary>
        /// <see cref="Coordinate2DParser"/> with spherical coordinate pair should work.
        /// </summary>
        /// <param name="decimalCoordinateString">Decimal coordinate string.</param>
        /// <param name="latitudeValue">Expected value for latitude.</param>
        /// <param name="longitudeValue">Expected value for longitude.</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(Coordinate2DParser), Description = "Coordinate2DParser with spherical coordinate pair should work.")]
        [TestCase(@"S13°07'247"", E30°19'345""", "-13.120783", "30.322417")]
        [TestCase(@"29.63527EN, 82.37111EW", "29.635270", "-82.371110")]
        [TestCase(@"N33.50.13, E107.48.52", "33.836944", "107.814444")]
        [TestCase(@"N33.50.613, E107.48.524", "33.843550", "107.808733")]
        [TestCase(@"S39°34 283, W71°29 908", "-39.571383", "-71.498467")]
        [TestCase(@"S39°34'283""W 71°29'908""", "-39.571383", "-71.498467")]
        [TestCase(@"20. 58139°S, 164.76444°E", "-20.581390", "164.764440")]
        [TestCase(@"22.14158°’S, 166.67993 °E", "-22.141580", "166.679930")]
        [TestCase(@"22.14158°’S, 164 °7.6444'E", "-22.141580", "164.127407")]
        [TestCase(@" 29.5423°, -86.1926° ", "29.542300", "-86.192600")]
        [TestCase(@"-31:34:55; 159:5:9", "-31.581944", "159.085833")]
        [TestCase(@"34.47325°, 132.10362°", "34.473250", "132.103620")]
        [TestCase(@"S21°59'01, W64°12'30", "-21.983611", "-64.208333")]
        [TestCase(@"8.77522 N, -70.80349 E", "8.775220", "70.803490")]
        [TestCase(@"-3.08732°N, -79.71493°W", "3.087320", "-79.714930")]
        [TestCase(@"03°14.78S, 72°54.61W", "-3.246333", "-72.910167")]
        [TestCase(@"03°15’S 72°54’W", "-3.250000", "-72.900000")]
        [TestCase(@"20°20.1N 74°33.6W", "20.335000", "-74.560000")]
        [TestCase(@"37°08'09.4""N, 8°23'04.2""W", "37.135944", "-8.384500")]
        [TestCase(@"08º48’23’’S, 115º56’24’’E", "-8.806389", "115.940000")]
        [TestCase(@"04.2948°, -066.2889°", "4.294800", "-66.288900")]
        [TestCase(@"04.7503°, -066.3549°", "4.750300", "-66.354900")]
        [TestCase(@"05.4286°, -066.1362°", "5.428600", "-66.136200")]
        [TestCase(@"05.3868°, -066.1159°", "5.386800", "-66.115900")]
        [TestCase(@"Lon. 151. E. Lat. 3. S.", "-3.000000", "151.000000")]
        [TestCase(@"16°03'--""S, 130°26'--""E", "-16.050000", "130.433333")]
        [TestCase(@"N38.50 W 120.22", "38.500000", "-120.220000")]
        [TestCase(@"18.592277, -70.492133", "18.592277", "-70.492133")]
        [TestCase(@"17°54'10.7""N, 71°14'13.7""W", "17.902972", "-71.237139")]
        public void Coordinate2DParser_WithSphericalCoordinatePair_ShouldWork(string decimalCoordinateString, string latitudeValue, string longitudeValue)
        {
            // Arrange
            var latitude = new CoordinatePart();
            var longitude = new CoordinatePart();
            var coordinateType = string.Empty;
            var parser = this.Coordinate2DParserFactory.Invoke();

            // Act
            parser.ParseCoordinateString(decimalCoordinateString, coordinateType, latitude, longitude);

            // Assert
            Assert.AreEqual(latitudeValue, latitude.Value);
            Assert.AreEqual(longitudeValue, longitude.Value);
        }

        /// <summary>
        /// <see cref="Coordinate2DParser"/> wit UTM coordinate should work.
        /// </summary>
        /// <param name="utmCoordinateString">UTM coordinate string.</param>
        /// <param name="latitudeValue">Expected value for latitude.</param>
        /// <param name="longitudeValue">Expected value for longitude.</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(Coordinate2DParser), Description = "Coordinate2DParser with UTM coordinate should work.")]
        [TestCase(@"33T 455.4683", "42.297928", "14.454084")]
        [TestCase(@"UTM WGS84: 33T 455.4683", "42.297928", "14.454084")]
        [TestCase(@" UTM WGS84 33T 455.4683", "42.297928", "14.454084")]
        [TestCase(@"33T 4498003N; 674582E", "40.614422", "17.063799")]
        [TestCase(@"UTM ED50: 33T 4498003N; 674582E", "40.614422", "17.063799")]
        [TestCase(@" UTM ED50 33T 4498003N; 674582E", "40.614422", "17.063799")]
        [TestCase(@"33T 674582E; 4498003N", "40.614422", "17.063799")]
        [TestCase(@"UTM ED50: 33T 674582E; 4498003N", "40.614422", "17.063799")]
        [TestCase(@"55G 595500 5371700", "-41.800816", "148.149549")]
        [TestCase(@" UTM XYZ123 55G 5955 53717", "-41.800816", "148.149549")]
        public void Coordinate2DParser_WitUTMCoordinate_ShouldWork(string utmCoordinateString, string latitudeValue, string longitudeValue)
        {
            // Arrange
            var latitude = new CoordinatePart();
            var longitude = new CoordinatePart();
            var coordinateType = string.Empty;
            var parser = this.Coordinate2DParserFactory.Invoke();

            // Act
            parser.ParseCoordinateString(utmCoordinateString, coordinateType, latitude, longitude);

            // Assert
            Assert.AreEqual(latitudeValue, latitude.Value);
            Assert.AreEqual(longitudeValue, longitude.Value);
        }
    }
}
