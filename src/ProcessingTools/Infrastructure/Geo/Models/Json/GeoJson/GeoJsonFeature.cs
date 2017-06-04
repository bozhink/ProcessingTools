namespace ProcessingTools.Geo.Models.Json.GeoJson
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    /// <summary>
    /// <para>A GeoJSON object with the type "Feature" is a feature object.</para>
    /// </summary>
    [JsonObject]
    public class GeoJsonFeature : GeoJsonObject<GeoJsonFeatureType>
    {
        /// <summary>
        /// <para>If a feature has a commonly used identifier, that identifier should
        /// be included as a member of the feature object with the name "id".</para>
        /// </summary>
        [JsonProperty(propertyName: "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        /// <summary>
        /// <para>The value of the geometry member is a geometry object a JSON null value.</para>
        /// </summary>
        [Required]
        [JsonRequired]
        [JsonProperty(propertyName: "geometry")]
        public GeoJsonGeometry Geometry { get; set; }

        /// <summary>
        /// <para>The value of the properties member is an object (any JSON object or a JSON null value).</para>
        /// </summary>
        [Required]
        [JsonRequired]
        [JsonProperty(propertyName: "properties")]
        public object Properties { get; set; }
    }
}
