namespace ProcessingTools.Geo.Models.Json.GeoJson
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    /// <summary>
    /// <para>A GeoJSON object with the type "FeatureCollection" is a feature collection object.</para>
    /// </summary>
    [JsonObject]
    public class GeoJsonFeatureCollection : GeoJsonObject<GeoJsonFeatureCollectionType>
    {
        /// <summary>
        /// <para>The value corresponding to "features" is an array.
        /// Each element in the array is a feature object as defined above.</para>
        /// </summary>
        [Required]
        [JsonRequired]
        [JsonProperty(propertyName: "features")]
        public IEnumerable<GeoJsonFeature> Features { get; set; }
    }
}
