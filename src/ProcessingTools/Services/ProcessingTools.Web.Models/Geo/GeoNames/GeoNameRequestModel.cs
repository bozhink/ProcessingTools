// <copyright file="GeoNameRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.GeoNames
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Geo name request model.
    /// </summary>
    public class GeoNameRequestModel : IGeoName
    {
        /// <summary>
        /// Gets or sets the geo name ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the geo name.
        /// </summary>
        public string Name { get; set; }
    }
}
