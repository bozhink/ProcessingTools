// <copyright file="IReferenceBaseTagStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Layout.Styles.References
{
    /// <summary>
    /// Reference base tag style model.
    /// </summary>
    public interface IReferenceBaseTagStyleModel : IReferenceStyleModel, ITagStyleModel
    {
        /// <summary>
        /// Gets the script content.
        /// </summary>
        string Script { get; }
    }
}
