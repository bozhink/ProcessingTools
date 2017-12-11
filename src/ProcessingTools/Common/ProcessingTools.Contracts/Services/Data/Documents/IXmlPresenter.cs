// <copyright file="IXmlPresenter.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Services.Data.Documents;

    /// <summary>
    /// XML Presenter.
    /// </summary>
    public interface IXmlPresenter
    {
        Task<string> GetHtmlAsync(object userId, object articleId, object documentId);

        Task<string> GetXmlAsync(object userId, object articleId, object documentId);

        Task<object> SaveHtmlAsync(object userId, object articleId, IDocument document, string content);

        Task<object> SaveXmlAsync(object userId, object articleId, IDocument document, string content);
    }
}
