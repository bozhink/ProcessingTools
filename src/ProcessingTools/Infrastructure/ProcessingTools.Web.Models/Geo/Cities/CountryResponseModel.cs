// <copyright file="CountryResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.Cities
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Country response model
    /// </summary>
    public class CountryResponseModel : INameableIntegerIdentifiable
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the country object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the country object.
        /// </summary>
        public string Name { get; set; }
    }
}
