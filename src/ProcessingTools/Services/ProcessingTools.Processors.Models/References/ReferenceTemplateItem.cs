// <copyright file="ReferenceTemplateItem.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.References
{
    using ProcessingTools.Models.Contracts.Processors.References;

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
