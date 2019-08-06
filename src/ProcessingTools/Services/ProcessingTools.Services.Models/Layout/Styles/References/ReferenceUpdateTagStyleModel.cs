﻿// <copyright file="ReferenceUpdateTagStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Layout.Styles.References
{
    using ProcessingTools.Contracts.Services.Models.Layout.Styles.References;

    /// <summary>
    /// Reference update tag style model.
    /// </summary>
    public class ReferenceUpdateTagStyleModel : IReferenceUpdateTagStyleModel
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

        /// <inheritdoc/>
        public string TargetXPath { get; set; }
    }
}