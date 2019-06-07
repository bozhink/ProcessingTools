// <copyright file="IArticleContributorDeployModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Deployment.Documents
{
    /// <summary>
    /// Article contributor deploy model.
    /// </summary>
    public interface IArticleContributorDeployModel : IDeployModel, IPerson
    {
        /// <summary>
        /// Gets the role of the contributor, e.g. author, editor, etc.
        /// </summary>
        string Role { get; }

        // TODO: add affiliations
    }
}
