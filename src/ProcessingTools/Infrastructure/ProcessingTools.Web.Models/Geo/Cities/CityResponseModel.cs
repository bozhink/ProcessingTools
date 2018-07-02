// <copyright file="CityResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.Cities
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// City response model
    /// </summary>
    public class CityResponseModel : INameableIntegerIdentifiable
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the city object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the city object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the corresponding country of the city object.
        /// </summary>
        public CountryResponseModel Country { get; set; }
    }
}
