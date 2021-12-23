// <copyright file="IArticleDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Documents.Articles
{
    using ProcessingTools.Contracts.Models.Documents.Articles;

    /// <summary>
    /// Article data transfer object (DTO).
    /// </summary>
    public interface IArticleDataTransferObject : IDataTransferObject, IArticleModel
    {
    }
}
