// <copyright file="IReferenceStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Layout.Styles.References
{
    /// <summary>
    /// Reference style model.
    /// </summary>
    public interface IReferenceStyleModel : IStyleModel
    {
        /// <summary>
        /// Gets the XPath for selection of the XML objects which represent the reference.
        /// </summary>
        string ReferenceXPath { get; }
    }
}
