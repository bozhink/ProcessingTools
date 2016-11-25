namespace ProcessingTools.Geo
{
    using System;
    using System.Linq;
    using Contracts;
    using GeoAPI.CoordinateSystems;
    using GeoAPI.CoordinateSystems.Transformations;
    using ProjNet.CoordinateSystems;
    using ProjNet.CoordinateSystems.Transformations;

    /// <summary>
    /// See http://blogs.u2u.be/diederik/post/2010/01/01/Converting-Spatial-Coordinates-with-ProjNET.aspx
    /// </summary>
    public class UtmCoordianesTransformer : IUtmCoordianesTransformer
    {
        private readonly ICoordinateSystem gcsWGS84;
        private readonly ICoordinateTransformationFactory coordinateTransformationFactory;

        public UtmCoordianesTransformer()
        {
            this.gcsWGS84 = GeographicCoordinateSystem.WGS84;
            this.coordinateTransformationFactory = new CoordinateTransformationFactory();
        }

        public double[] TransformDecimal2Utm(double latitude, double longitude, string utmZone)
        {
            var transformation = this.CreateTransformation(utmZone);
            var point = new double[] { latitude, longitude };
            var result = transformation.Transform(point);
            return result;
        }

        public double[] TransformUtm2Decimal(double utmEasting, double utmNorthing, string utmZone)
        {
            try
            {
                var transformation = this.CreateTransformation(utmZone).Inverse();
                var point = new double[] { utmEasting, utmNorthing };
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
