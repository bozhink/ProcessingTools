// <copyright file="IArticleMetaHarvester.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Meta
{
    using ProcessingTools.Contracts.Models.Meta;

    /// <summary>
    /// Article meta harvester.
    /// </summary>
    public interface IArticleMetaHarvester : IDocumentHarvester<IArticle>
    {
    }
}
