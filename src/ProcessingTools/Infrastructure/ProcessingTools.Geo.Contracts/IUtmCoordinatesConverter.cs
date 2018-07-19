// <copyright file="IUtmCoordinatesConverter.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Geo.Contracts
{
    /// <summary>
    /// UTM Coordinates Converter
    /// </summary>
    public interface IUtmCoordinatesConverter
    {
        /// <summary>
        /// Transforms decimal coordinates to UTM.
        /// </summary>
        /// <param name="latitude">Decimal latitude.</param>
        /// <param name="longitude">Decimal longitude.</param>
        /// <param name="utmZone">UTM Zone.</param>
        /// <returns>Pair of UTM Easting and UTM Northing.</returns>
        double[] TransformDecimal2Utm(double latitude, double longitude, string utmZone);

        /// <summary>
        /// Transforms UTM coordinates to decimal.
        /// </summary>
        /// <param name="utmEasting">UTM Easting.</param>
        /// <param name="utmNorthing">UTM Northing.</param>
        /// <param name="utmZone">UTM Zone.</param>
        /// <returns>Pair of decimal latitude and longitude.</returns>
        double[] TransformUtm2Decimal(double utmEasting, double utmNorthing, string utmZone);
    }
}
