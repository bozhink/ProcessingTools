// <copyright file="IArticlesDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Documents
{
    using ProcessingTools.Services.Models.Contracts.Documents.Articles;

    /// <summary>
    /// Articles data service.
    /// </summary>
    public interface IArticlesDataService : IDataService<IArticleModel, IArticleDetailsModel, IArticleInsertModel, IArticleUpdateModel>
    {
    }
}
