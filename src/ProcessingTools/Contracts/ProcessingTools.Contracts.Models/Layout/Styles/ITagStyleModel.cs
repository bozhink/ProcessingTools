// <copyright file="ITagStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Layout.Styles
{
    /// <summary>
    /// Tag style model.
    /// </summary>
    public interface ITagStyleModel : IStyleModel
    {
        /// <summary>
        /// Gets the XPath to select target nodes to process.
        /// </summary>
        string TargetXPath { get; }
    }
}
