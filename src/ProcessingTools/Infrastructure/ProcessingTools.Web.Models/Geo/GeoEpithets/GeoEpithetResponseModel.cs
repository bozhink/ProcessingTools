// <copyright file="GeoEpithetResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.GeoEpithets
{
    /// <summary>
    /// Represents response model for the geo epithets API.
    /// </summary>
    public class GeoEpithetResponseModel
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the geo epithet object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the geo epithet object.
        /// </summary>
        public string Name { get; set; }
    }
}
