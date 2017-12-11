// <copyright file="IPublisherDetails.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Services.Data.Journals
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Publisher details.
    /// </summary>
    public interface IPublisherDetails : IPublisher, IAddressable, ICreated, IModified, IDetailedModel
    {
    }
}
