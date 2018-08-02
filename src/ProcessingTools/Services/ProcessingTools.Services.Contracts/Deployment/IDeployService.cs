// <copyright file="IDeployService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Deployment
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

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
