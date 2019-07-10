﻿// <copyright file="ReferenceUpdateParseStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Layout.Styles.References;

namespace ProcessingTools.Services.Models.Layout.Styles.References
{
    /// <summary>
    /// Reference update parse style model.
    /// </summary>
    public class ReferenceUpdateParseStyleModel : IReferenceUpdateParseStyleModel
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
