// <copyright file="IAddress.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Journals.Models
{
    using ProcessingTools.Models.Contracts;

    public interface IAddress : IStringIdentifiable, ProcessingTools.Models.Contracts.IAddressable, IDataModel
    {
        int? CityId { get; }

        int? CountryId { get; }
    }
}
