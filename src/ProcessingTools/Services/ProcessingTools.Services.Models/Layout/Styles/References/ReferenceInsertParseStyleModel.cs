// <copyright file="ReferenceInsertParseStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Layout.Styles.References
{
    /// <summary>
    /// Reference insert parse style model.
    /// </summary>
    public class ReferenceInsertParseStyleModel : ProcessingTools.Services.Models.Contracts.Layout.Styles.References.IReferenceInsertParseStyleModel
    {
        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        public string Script { get; set; }

        /// <inheritdoc/>
        public string ReferenceXPath { get; set; }
    }
}
