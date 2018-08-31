// <copyright file="ICoordinatePart.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Contracts.Geo.Coordinates
{
    using ProcessingTools.Common.Enumerations;

    /// <summary>
    /// Coordinate part.
    /// </summary>
    public interface ICoordinatePart
    {
        /// <summary>
        /// Gets or sets the coordinate part string.
        /// </summary>
        string CoordinatePartString { get; set; }

        /// <summary>
        /// Gets or sets the decimal value.
        /// </summary>
        double DecimalValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether coordinate part is present in the coordinate part string.
        /// </summary>
        bool PartIsPresent { get; set; }

        /// <summary>
        /// Gets or sets the coordinate type.
        /// </summary>
        CoordinatePartType Type { get; set; }

        /// <summary>
        /// Gets the string value of the coordinate part.
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Parse coordinate part string to value.
        /// </summary>
        void Parse();
    }
}
