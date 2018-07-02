// <copyright file="GeoJsonGeometryType.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Geo.GeoJson
{
    /// <summary>
    /// GeoJson geometry type.
    /// </summary>
    public enum GeoJsonGeometryType
    {
        /// <summary>
        /// Point
        /// </summary>
        Point,

        /// <summary>
        /// Multi point
        /// </summary>
        MultiPoint,

        /// <summary>
        /// Line string
        /// </summary>
        LineString,

        /// <summary>
        /// Multi line string
        /// </summary>
        MultiLineString,

        /// <summary>
        /// Polygon
        /// </summary>
        Polygon,

        /// <summary>
        /// Multi polygon
        /// </summary>
        MultiPolygon,

        /// <summary>
        /// Geometry collection
        /// </summary>
        GeometryCollection
    }
}
