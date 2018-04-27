// <copyright file="IReferenceBaseParseStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Layout.Styles.References
{
    /// <summary>
    /// Reference base parse style model.
    /// </summary>
    public interface IReferenceBaseParseStyleModel : IReferenceStyleModel, IParseStyleModel
    {
        /// <summary>
        /// Gets the script content.
        /// </summary>
        string Script { get; }
    }
}
