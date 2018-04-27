// <copyright file="FloatObjectInsertParseStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Layout.Styles.Floats
{
    using ProcessingTools.Enumerations.Nlm;

    /// <summary>
    /// Float object insert parse style model.
    /// </summary>
    public class FloatObjectInsertParseStyleModel : ProcessingTools.Services.Models.Contracts.Layout.Styles.Floats.IFloatObjectInsertParseStyleModel
    {
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
