// <copyright file="IDeployModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Deployment
{
    /// <summary>
    /// Deploy model.
    /// </summary>
    public interface IDeployModel
    {
        /// <summary>
        /// Gets the identifier of the source article.
        /// </summary>
        string ArticleId { get; }
    }
}
