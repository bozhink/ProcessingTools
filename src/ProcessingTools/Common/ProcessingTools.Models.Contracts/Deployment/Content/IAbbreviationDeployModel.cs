// <copyright file="IAbbreviationDeployModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Deployment.Content
{
    /// <summary>
    /// Abbreviation deploy model.
    /// </summary>
    public interface IAbbreviationDeployModel : IDeployModel
    {
        /// <summary>
        /// Gets the value of the content type.
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Gets the value of the abbreviation as text.
        /// </summary>
        string AbbreviationText { get; }

        /// <summary>
        /// Gets the value of the abbreviation as XML.
        /// </summary>
        string AbbreviationXml { get; }

        /// <summary>
        /// Gets the value of the description as text.
        /// </summary>
        string DescriptionText { get; }

        /// <summary>
        /// Gets the value of the description as XML.
        /// </summary>
        string DescriptionXml { get; }
    }
}
