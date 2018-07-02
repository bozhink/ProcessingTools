// <copyright file="ReferenceUpdateParseStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Layout.Styles.References
{
    /// <summary>
    /// Reference update parse style model.
    /// </summary>
    public class ReferenceUpdateParseStyleModel : ProcessingTools.Services.Models.Contracts.Layout.Styles.References.IReferenceUpdateParseStyleModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

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
