// <copyright file="IArticleModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Articles
{
    /// <summary>
    /// Article model.
    /// </summary>
    public interface IArticleModel : IArticleBaseModel, IStringIdentifiable, ICreated, IModified
    {
        /// <summary>
        /// Gets or sets a value indicating whether article is finalized.
        /// </summary>
        bool IsFinalized { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether article is deployed.
        /// </summary>
        bool IsDeployed { get; set; }
    }
}
