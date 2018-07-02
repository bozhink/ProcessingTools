// <copyright file="GeoJsonCoordinateReferenceSystemProperties.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Geo.GeoJson
{
    using Newtonsoft.Json;

    /// <summary>
    /// <para>A CRS object may indicate a coordinate reference system by name.
    /// In this case, the value of its "type" member must be the string "name".
    /// The value of its "properties" member must be an object containing a "name" member.</para>
    /// <para>A CRS object may link to CRS parameters on the Web.
    /// In this case, the value of its "type" member must be the string "link",
    /// and the value of its "properties" member must be a Link object.</para>
    /// </summary>
    [JsonObject]
    public class GeoJsonCoordinateReferenceSystemProperties
    {
        /// <summary>
        /// Gets or sets the name.
        /// <para>A CRS object may indicate a coordinate reference system by name.
        /// In this case, the value of its "type" member must be the string "name".
        /// The value of its "properties" member must be an object containing a "name" member.
        /// The value of that "name" member must be a string identifying a coordinate reference system.
        /// OGC CRS URNs such as "urn:ogc:def:crs:OGC:1.3:CRS84" shall be preferred over
        /// legacy identifiers such as "EPSG:4326".</para>
        /// <code>"crs": {
        ///   "type": "name",
        ///   "properties": {
        ///     "name": "urn:ogc:def:crs:OGC:1.3:CRS84"
        ///   }
        /// }</code>
        /// </summary>
        [JsonProperty(propertyName: "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the href.
        /// <para>A link object has one required member: "href", and one optional member: "type".</para>
        /// <para>The value of the required "href" member must be a dereferenceable URI.</para>
        /// <code>"crs": {
        ///   "type": "link",
        ///   "properties": {
        ///     "href": "http://example.com/crs/42",
        ///     "type": "proj4"
        ///   }
        /// }</code>
        /// </summary>
        [JsonProperty(propertyName: "href", NullValueHandling = NullValueHandling.Ignore)]
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// <para>A link object has one required member: "href", and one optional member: "type".</para>
        /// <para>The value of the optional "type" member must be a string that hints at the format used
        /// to represent CRS parameters at the provided URI.
        /// Suggested values are: "proj4", "ogcwkt", "esriwkt", but others can be used.</para>
        /// <code>"crs": {
        ///   "type": "link",
        ///   "properties": {
        ///     "href": "http://example.com/crs/42",
        ///     "type": "proj4"
        ///   }
        /// }</code>
        /// </summary>
        [JsonProperty(propertyName: "type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }
}
