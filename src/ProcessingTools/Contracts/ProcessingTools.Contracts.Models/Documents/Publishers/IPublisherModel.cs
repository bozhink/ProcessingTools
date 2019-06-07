// <copyright file="IPublisherModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Documents.Publishers
{
    /// <summary>
    /// Publisher model.
    /// </summary>
    public interface IPublisherModel : IPublisherBaseModel, IStringIdentifiable, ICreated, IModified
    {
    }
}
