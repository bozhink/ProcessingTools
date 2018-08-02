// <copyright file="IArticleAbstractDeployModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Deployment.Documents
{
    /// <summary>
    /// Article abstract deploy model.
    /// </summary>
    public interface IArticleAbstractDeployModel : IDeployModel
    {
        /// <summary>
        /// Gets the language code.
        /// </summary>
        string LanguageCode { get; }

        /// <summary>
        /// Gets the value of the title as text.
        /// </summary>
        string TitleText { get; }

        /// <summary>
        /// Gets the value of the title as XML.
        /// </summary>
        string TitleXml { get; }

        /// <summary>
        /// Gets the value of the content as text.
        /// </summary>
        string ContentText { get; }

        /// <summary>
        /// Gets the value of the content as XML.
        /// </summary>
        string ContentXml { get; }
    }
}
