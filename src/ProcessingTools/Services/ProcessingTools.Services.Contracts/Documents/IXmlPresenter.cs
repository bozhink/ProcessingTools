// <copyright file="IXmlPresenter.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Documents
{
    using System.Threading.Tasks;

    /// <summary>
    /// XML presenter.
    /// </summary>
    public interface IXmlPresenter
    {
        /// <summary>
        /// Gets content of a specified document as HTML.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>HTML representation of the content of the document.</returns>
        Task<string> GetHtmlAsync(object id, string articleId);

        /// <summary>
        /// Gets content of a specified document as XML.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>XML representation of the content of the document.</returns>
        Task<string> GetXmlAsync(object id, string articleId);

        /// <summary>
        /// Sets HTML content of a specified document.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="content">HTML representation of the content of the document.</param>
        /// <returns>Resultant object.</returns>
        Task<bool> SetHtmlAsync(object id, string articleId, string content);

        /// <summary>
        /// Sets XML content of a specified document.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="content">XML representation of the content of the document.</param>
        /// <returns>Resultant object.</returns>
        Task<bool> SetXmlAsync(object id, string articleId, string content);
    }
}
