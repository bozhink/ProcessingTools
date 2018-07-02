// <copyright file="IPublisherDetails.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Services.Data.Journals
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Publisher details.
    /// </summary>
    public interface IPublisherDetails : IPublisher, IAddressable, ICreated, IModified, IDetailedModel
    {
    }
}
