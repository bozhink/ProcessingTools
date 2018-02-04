// <copyright file="IPublisherModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Contracts.Documents.Publishers
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Publisher model.
    /// </summary>
    public interface IPublisherModel : IPublisherBaseModel, IStringIdentifiable, ICreated, IModified
    {
    }
}
