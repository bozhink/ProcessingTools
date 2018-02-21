// <copyright file="IUtmCoordinatesTransformer.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Geo.Coordinates
{
    /// <summary>
    /// UTM coordinates transformer.
    /// </summary>
    public interface IUtmCoordinatesTransformer
    {
        /// <summary>
        /// Transform decimal coordinates to UTM.
        /// </summary>
        /// <param name="latitude">Latitude.</param>
        /// <param name="longitude">Longitude.</param>
        /// <param name="utmZone">UTM zone.</param>
        /// <returns>UTM coordinates.</returns>
        double[] TransformDecimal2Utm(double latitude, double longitude, string utmZone);

        /// <summary>
        /// Transform UTM coordinates to decimal.
        /// </summary>
        /// <param name="utmEasting">UTM easting</param>
        /// <param name="utmNorthing">UTM northing.</param>
        /// <param name="utmZone">UTM zone.</param>
        /// <returns>Decimal coordinates.</returns>
        double[] TransformUtm2Decimal(double utmEasting, double utmNorthing, string utmZone);
    }
}
