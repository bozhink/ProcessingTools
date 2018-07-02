// <copyright file="ITagStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Layout.Styles
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
