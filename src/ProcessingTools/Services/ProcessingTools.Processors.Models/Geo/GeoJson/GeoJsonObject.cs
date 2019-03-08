﻿// <copyright file="GeoJsonObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Geo.GeoJson
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// <para>GeoJSON always consists of a single object.</para>
    /// <para>This object represents a geometry, feature, or collection of features.</para>
    /// </summary>
    /// <typeparam name="TEnum">Type of the enumeration</typeparam>
    [JsonObject]
    public abstract class GeoJsonObject<TEnum>
        where TEnum : struct, IComparable, IConvertible, IFormattable
    {
        private string type;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoJsonObject{TEnum}"/> class.
        /// </summary>
        protected GeoJsonObject()
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new InvalidOperationException(nameof(TEnum));
            }
        }

        /// <summary>
        /// Gets or sets the GeoJSON type.
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
        /// Gets or sets "crs" value of the member.
        /// <para>A GeoJSON object may have an optional "crs" member, the value of which must be a coordinate reference system object.</para>
        /// </summary>
        [JsonProperty(propertyName: "crs", NullValueHandling = NullValueHandling.Ignore)]
        public GeoJsonCoordinateReferenceSystem Crs { get; set; }

        /// <summary>
        /// Gets or sets the "bbox" value of the member.
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
                throw new ArgumentNullException(nameof(type));
            }

            string inputType = type.ToLowerInvariant();
            string thisType = Enum.GetNames(enumType).FirstOrDefault(t => t.ToLowerInvariant() == inputType);

            if (string.IsNullOrEmpty(thisType))
            {
                throw new ArgumentException(string.Empty, nameof(type));
            }

            return thisType;
        }
    }
}