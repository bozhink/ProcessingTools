// <copyright file="IArticleMetaHarvester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Meta
{
    using ProcessingTools.Services.Models.Contracts.Meta;

    /// <summary>
    /// Article meta harvester.
    /// </summary>
    public interface IArticleMetaHarvester : IDocumentHarvester<IArticle>
    {
    }
}
