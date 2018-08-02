// <copyright file="IDeployDataModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Contracts.Deployment
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Deploy data model.
    /// </summary>
    public interface IDeployDataModel : IIdentifiedDataModel, ICreated
    {
        /// <summary>
        /// Gets the identifier of the source article.
        /// </summary>
        string ArticleId { get; }
    }
}
