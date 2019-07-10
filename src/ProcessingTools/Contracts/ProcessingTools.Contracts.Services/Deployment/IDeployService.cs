// <copyright file="IDeployService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Deployment
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Deploy service.
    /// </summary>
    public interface IDeployService
    {
        /// <summary>
        /// Deploy information of a specified document.
        /// </summary>
        /// <param name="articleId">Identifier of the article which is the document under deploy.</param>
        /// <param name="document">Document to be deployed.</param>
        /// <returns>Task of result.</returns>
        Task<object> DeployAsync(string articleId, IDocument document);
    }
}
