namespace ProcessingTools.Geo.Models.Json.GeoJson
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// <para>GeoJSON always consists of a single object.
    /// This object represents a geometry, feature, or collection of features.</para>
    /// </summary>
    [JsonObject]
    public abstract class GeoJsonObject<TEnum>
        where TEnum : struct, IComparable, IConvertible, IFormattable
    {
        private string type;

        public GeoJsonObject()
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException(GeoJsonMessages.InvalidEnumTypeExceptionMessage);
            }
        }

        /// <summary>
        /// <para>The GeoJSON object must have a member with the name "type".
        /// This member's value is a string that determines the type of the GeoJSON object.</para>
        /// <para>The value of the type member must be one of:
        /// "Point",
        /// "MultiPoint",
        /// "LineString",
        /// "MultiLineString",
        /// "Polygon",
        /// "MultiPolygon",
        /// "GeometryCollection",
        /// "Feature", or
        /// "FeatureCollection".
        /// The case of the type member values must be as shown here.</para>
        /// </summary>
        [Required]
        [JsonRequired]
        [JsonProperty(propertyName: "type")]
        public string Type
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = this.ValidateGeoJsonType(value, typeof(TEnum));
            }
        }

        /// <summary>
        /// <para>A GeoJSON object may have an optional "crs" member, the value of which must be a coordinate reference system object.</para>
        /// </summary>
        [JsonProperty(propertyName: "crs", NullValueHandling = NullValueHandling.Ignore)]
        public GeoJsonCoordinateReferenceSystem Crs { get; set; }

        /// <summary>
        /// <para>A GeoJSON object may have a "bbox" member, the value of which must be a bounding box array.</para>
        /// <para>To include information on the coordinate range for geometries, features, or feature collections,
        /// a GeoJSON object may have a member named "bbox". The value of the bbox member must be a 2*n array
        /// where n is the number of dimensions represented in the contained geometries, with the lowest values
        /// for all axes followed by the highest values.
        /// The axes order of a bbox follows the axes order of geometries.
        /// In addition, the coordinate reference system for the bbox is assumed to match the
        /// coordinate reference system of the GeoJSON object of which it is a member.</para>
        /// </summary>
        [JsonProperty(propertyName: "bbox", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<double> Bbox { get; set; }

        private string ValidateGeoJsonType(string type, Type enumType)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException(GeoJsonMessages.NullTypeExceptionMessage);
            }

            string inputType = type.ToLower();
            string thisType = Enum.GetNames(enumType)
                .FirstOrDefault(t => t.ToLower() == inputType);

            if (string.IsNullOrEmpty(thisType))
            {
                throw new ArgumentException($"{GeoJsonMessages.InvalidTypeExceptionMessage} {type}.");
            }

            return thisType;
        }
    }
}