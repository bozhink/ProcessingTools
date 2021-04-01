﻿// <copyright file="IPublisherModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Documents.Publishers
{
    /// <summary>
    /// Publisher model.
    /// </summary>
    public interface IPublisherModel : IPublisherBaseModel, IStringIdentified, ICreated, IModified
    {
    }
}
