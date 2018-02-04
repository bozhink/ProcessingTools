// <copyright file="IXmlPresenter.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Services.Data.Documents;

    /// <summary>
    /// XML Presenter.
    /// </summary>
    public interface IXmlPresenter
    {
        /// <summary>
        /// Gets HTML.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="articleId">Article ID for the file.</param>
        /// <param name="documentId">Document ID for the file.</param>
        /// <returns>HTML of the file as string.</returns>
        Task<string> GetHtmlAsync(object userId, object articleId, object documentId);

        /// <summary>
        /// Gets XML.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="articleId">Article ID for the file.</param>
        /// <param name="documentId">Document ID for the file.</param>
        /// <returns>XML of the file as string.</returns>
        Task<string> GetXmlAsync(object userId, object articleId, object documentId);

        /// <summary>
        /// Saves document content provides as HTML string.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="articleId">Article ID for the file.</param>
        /// <param name="document"><see cref="IDocument"/> to be updated.</param>
        /// <param name="content">HTML content as string.</param>
        /// <returns>Task of result.</returns>
        Task<object> SaveHtmlAsync(object userId, object articleId, IDocument document, string content);

        /// <summary>
        /// Saves document content provides as XML string.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="articleId">Article ID for the file.</param>
        /// <param name="document"><see cref="IDocument"/> to be updated.</param>
        /// <param name="content">XML content as string.</param>
        /// <returns>Task of result.</returns>
        Task<object> SaveXmlAsync(object userId, object articleId, IDocument document, string content);
    }
}
