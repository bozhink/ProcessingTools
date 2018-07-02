﻿// <copyright file="IPublisherModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Publishers
{
    /// <summary>
    /// Publisher model.
    /// </summary>
    public interface IPublisherModel : IPublisherBaseModel, IStringIdentifiable, ICreated, IModified
    {
    }
}
