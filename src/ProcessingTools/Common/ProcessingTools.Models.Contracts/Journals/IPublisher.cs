// <copyright file="IPublisher.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Journals
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Publisher.
    /// </summary>
    public interface IPublisher : IAddressable, IAbbreviatedNameableStringIdentifiable, IModelWithUserInformation
    {
    }
}
