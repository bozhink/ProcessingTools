// <copyright file="GeoJsonType.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Geo.GeoJson
{
    /// <summary>
    /// GeoJson type.
    /// </summary>
    public enum GeoJsonType
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
        GeometryCollection,

        /// <summary>
        /// Feature
        /// </summary>
        Feature,

        /// <summary>
        /// Feature collection
        /// </summary>
        FeatureCollection
    }
}
