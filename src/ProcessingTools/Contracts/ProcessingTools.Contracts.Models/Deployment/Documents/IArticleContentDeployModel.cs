// <copyright file="IArticleContentDeployModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Deployment.Documents
{
    /// <summary>
    /// Article content deploy model.
    /// </summary>
    public interface IArticleContentDeployModel : IDeployModel
    {
        /// <summary>
        /// Gets the value of content as text.
        /// </summary>
        string ContentText { get; }

        /// <summary>
        /// Gets the value of the content as XML.
        /// </summary>
        string ContentXml { get; }
    }
}
