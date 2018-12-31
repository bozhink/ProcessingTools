﻿// <copyright file="ReferenceTemplateItem.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.References
{
    using ProcessingTools.Processors.Models.Contracts.References;

    /// <summary>
    /// Reference template item.
    /// </summary>
    public class ReferenceTemplateItem : IReferenceTemplateItem
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Year { get; set; }

        /// <inheritdoc/>
        public string Authors { get; set; }
    }
}
