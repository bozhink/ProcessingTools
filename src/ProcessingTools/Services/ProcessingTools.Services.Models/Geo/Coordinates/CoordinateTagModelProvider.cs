// <copyright file="CoordinateTagModelProvider.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Geo.Coordinates
{
    using System;
    using System.Xml;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Contracts.Services.Models.Geo.Coordinates;

    /// <summary>
    /// Coordinate tag model provider.
    /// </summary>
    public class CoordinateTagModelProvider : ICoordinateTagModelProvider
    {
        /// <inheritdoc/>
        public Func<XmlDocument, XmlElement> TagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.GeoCoordinate);

            return tagModel;
        };
    }
}
