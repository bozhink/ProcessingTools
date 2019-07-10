﻿// <copyright file="IReferenceTemplateItem.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Contracts.References
{
    /// <summary>
    /// Reference template item.
    /// </summary>
    public interface IReferenceTemplateItem
    {
        /// <summary>
        /// Gets reference id.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets reference authors.
        /// </summary>
        string Authors { get; }

        /// <summary>
        /// Gets reference year.
        /// </summary>
        string Year { get; }
    }
}
