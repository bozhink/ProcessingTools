﻿// <copyright file="IPublisher.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Journals.Models
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Publisher.
    /// </summary>
    public interface IPublisher : IAddressable, IAbbreviatedNameableStringIdentifiable, IModelWithUserInformation
    {
    }
}
