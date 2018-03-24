// <copyright file="AddressDataModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Journals
{
    using ProcessingTools.Models.Contracts.Journals;

    /// <summary>
    /// Address data model.
    /// </summary>
    public class AddressDataModel : IAddress
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string AddressString { get; set; }

        /// <inheritdoc/>
        public int? CityId { get; set; }

        /// <inheritdoc/>
        public int? CountryId { get; set; }
    }
}
