﻿// <copyright file="FloatObjectUpdateParseStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Layout.Styles.Floats
{
    using ProcessingTools.Common.Enumerations.Nlm;

    /// <summary>
    /// Float object update parse style model.
    /// </summary>
    public class FloatObjectUpdateParseStyleModel : ProcessingTools.Services.Models.Contracts.Layout.Styles.Floats.IFloatObjectUpdateParseStyleModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        public ReferenceType FloatReferenceType { get; set; }

        /// <inheritdoc/>
        public string Script { get; set; }

        /// <inheritdoc/>
        public string FloatObjectXPath { get; set; }
    }
}
