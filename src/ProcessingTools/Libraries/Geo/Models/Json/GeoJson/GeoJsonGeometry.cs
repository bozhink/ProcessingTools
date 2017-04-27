namespace ProcessingTools.Geo.Models.Json.GeoJson
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// <para>A geometry is a GeoJSON object where the type member's value is one of the following strings:
    /// "Point",
    /// "MultiPoint",
    /// "LineString",
    /// "MultiLineString",
    /// "Polygon",
    /// "MultiPolygon", or
    /// "GeometryCollection".</para>
    /// <para>A GeoJSON geometry object of any type other than "GeometryCollection"
    /// must have a member with the name "coordinates".
    /// The value of the coordinates member is always an array.
    /// The structure for the elements in this array is determined by the type of geometry.</para>
    /// </summary>
    [JsonObject]
    public class GeoJsonGeometry : GeoJsonObject<GeoJsonGeometryType>
    {
        /// <summary>
        /// <para>For type "Point", the "coordinates" member must be a single position.</para>
        /// <para>For type "MultiPoint", the "coordinates" member must be an array of positions.</para>
        /// <para>For type "LineString", the "coordinates" member must be an array of two or more positions.</para>
        /// <para>A LinearRing is closed LineString with 4 or more positions.
        /// The first and last positions are equivalent (they represent equivalent points).
        /// Though a LinearRing is not explicitly represented as a GeoJSON geometry type,
        /// it is referred to in the Polygon geometry type definition.</para>
        /// <para>For type "MultiLineString", the "coordinates" member must be an array of LineString coordinate arrays.</para>
        /// <para>For type "Polygon", the "coordinates" member must be an array of LinearRing coordinate arrays.
        /// For Polygons with multiple rings, the first must be the exterior ring and any others must be interior rings or holes.</para>
        /// <para>For type "MultiPolygon", the "coordinates" member must be an array of Polygon coordinate arrays.</para>
        /// </summary>
        [JsonProperty(propertyName: "coordinates", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<object> Coordinates { get; set; }

        /// <summary>
        /// <para>A GeoJSON object with type "GeometryCollection" is a geometry object which represents a collection of geometry objects.</para>
        /// <para>A geometry collection must have a member with the name "geometries".
        /// The value corresponding to "geometries" is an array.
        /// Each element in this array is a GeoJSON geometry object.</para>
        /// </summary>
        [JsonProperty(propertyName: "geometries", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<GeoJsonGeometry> Geometries { get; set; }
    }
}