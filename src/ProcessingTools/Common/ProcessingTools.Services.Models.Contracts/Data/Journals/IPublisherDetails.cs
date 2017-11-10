// <copyright file="IPublisherDetails.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Contracts.Data.Journals
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Publisher details.
    /// </summary>
    public interface IPublisherDetails : IPublisher, IAddressable, ICreated, IModified, IDetailedModel
    {
    }
}
