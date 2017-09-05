// <copyright file="GeoNameResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.GeoNames
{
    /// <summary>
    /// Represents response model for the geo names API.
    /// </summary>
    public class GeoNameResponseModel
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the geo name object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the geo name object.
        /// </summary>
        public string Name { get; set; }
    }
}
