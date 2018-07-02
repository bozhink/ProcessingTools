// <copyright file="GeoJsonCoordinateReferenceSystem.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Geo.GeoJson
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    /// <summary>
    /// <para>The coordinate reference system (CRS) of a GeoJSON object is determined by its "crs" member
    /// (referred to as the CRS object below). If an object has no crs member, then its parent or
    /// grandparent object's crs member may be acquired. If no crs member can be so acquired,
    /// the default CRS shall apply to the GeoJSON object.</para>
    /// <para>* The default CRS is a geographic coordinate reference system, using the WGS84 datum,
    /// and with longitude and latitude units of decimal degrees.</para>
    /// <para>* The value of a member named "crs" must be a JSON object or JSON null.
    /// If the value of CRS is null, no CRS can be assumed.</para>
    /// <para>* The crs member should be on the top-level GeoJSON object in a hierarchy
    /// (in feature collection, feature, geometry order) and should not be repeated or
    /// overridden on children or grandchildren of the object.</para>
    /// <para>* A non-null CRS object has two mandatory members: "type" and "properties".</para>
    /// <para>* CRS shall not change coordinate ordering.</para>
    /// <para>A CRS object may indicate a coordinate reference system by name.
    /// In this case, the value of its "type" member must be the string "name".
    /// The value of its "properties" member must be an object containing a "name" member.</para>
    /// <para>A CRS object may link to CRS parameters on the Web.
    /// In this case, the value of its "type" member must be the string "link",
    /// and the value of its "properties" member must be a Link object.</para>
    /// </summary>
    [JsonObject]
    public class GeoJsonCoordinateReferenceSystem
    {
        /// <summary>
        /// Gets or sets the type.
        /// The value of the type member must be a string, indicating the type of CRS object.
        /// </summary>
        [Required]
        [JsonRequired]
        [JsonProperty(propertyName: "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// The value of the properties member must be an object.
        /// </summary>
        [Required]
        [JsonRequired]
        [JsonProperty(propertyName: "properties")]
        public GeoJsonCoordinateReferenceSystemProperties Properties { get; set; }
    }
}
