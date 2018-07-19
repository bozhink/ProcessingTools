// <copyright file="UtmCoordinatesConverter.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Geo
{
    using System;
    using System.Linq;
    using GeoAPI.CoordinateSystems;
    using GeoAPI.CoordinateSystems.Transformations;
    using ProjNet.CoordinateSystems;
    using ProjNet.CoordinateSystems.Transformations;

    /// <summary>
    /// See http://blogs.u2u.be/diederik/post/2010/01/01/Converting-Spatial-Coordinates-with-ProjNET.aspx
    /// </summary>
    public class UtmCoordinatesConverter
    {
        private readonly ICoordinateSystem gcsWGS84;
        private readonly ICoordinateTransformationFactory coordinateTransformationFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="UtmCoordinatesConverter"/> class.
        /// </summary>
        public UtmCoordinatesConverter()
        {
            this.gcsWGS84 = GeographicCoordinateSystem.WGS84;
            this.coordinateTransformationFactory = new CoordinateTransformationFactory();
        }

        /// <summary>
        /// Transforms decimal coordinates to UTM.
        /// </summary>
        /// <param name="latitude">Decimal latitude.</param>
        /// <param name="longitude">Decimal longitude.</param>
        /// <param name="utmZone">UTM Zone.</param>
        /// <returns>Pair of UTM Easting and UTM Northing.</returns>
        public double[] TransformDecimal2Utm(double latitude, double longitude, string utmZone)
        {
            var transformation = this.CreateTransformation(utmZone);
            var point = new[] { latitude, longitude };
            var result = transformation.Transform(point);
            return result;
        }

        /// <summary>
        /// Transforms UTM coordinates to decimal.
        /// </summary>
        /// <param name="utmEasting">UTM Easting.</param>
        /// <param name="utmNorthing">UTM Northing.</param>
        /// <param name="utmZone">UTM Zone.</param>
        /// <returns>Pair of decimal latitude and longitude.</returns>
        public double[] TransformUtm2Decimal(double utmEasting, double utmNorthing, string utmZone)
        {
            try
            {
                var transformation = this.CreateTransformation(utmZone).Inverse();
                var point = new[] { utmEasting, utmNorthing };
                var retult = transformation.Transform(point);
                return retult.Reverse().ToArray();
            }
            catch (NotImplementedException e)
            {
                var message = $"Transformation of coordinate UTM WGS84: {utmZone} {utmEasting} {utmNorthing} is not implemented";
                throw new NotImplementedException(message, e);
            }
        }

        private IMathTransform CreateTransformation(string utmZone)
        {
            bool isNorthHemisphere = utmZone[utmZone.Length - 1] >= 'N';
            var zone = int.Parse(utmZone.Substring(0, utmZone.Length - 1));

            IProjectedCoordinateSystem pcsUTM = ProjectedCoordinateSystem.WGS84_UTM(zone, isNorthHemisphere);
            var transformation = this.coordinateTransformationFactory.CreateFromCoordinateSystems(this.gcsWGS84, pcsUTM);
            return transformation.MathTransform;
        }
    }
}
