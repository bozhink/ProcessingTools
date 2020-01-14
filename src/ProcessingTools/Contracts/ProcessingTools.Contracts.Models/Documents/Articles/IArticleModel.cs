// <copyright file="IArticleModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Documents.Articles
{
    /// <summary>
    /// Article model.
    /// </summary>
    public interface IArticleModel : IArticleBaseModel, IStringIdentified, ICreated, IModified
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
