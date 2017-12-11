// <copyright file="Address.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Journals
{
    using ProcessingTools.Contracts.Models.Services.Data.Journals;

    /// <summary>
    /// Address.
    /// </summary>
    public class Address : IAddress
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
