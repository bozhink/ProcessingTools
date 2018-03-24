// <copyright file="IArticleMetaHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Contracts.Meta
{
    using ProcessingTools.Harvesters.Models.Contracts.Meta;

    /// <summary>
    /// Article meta harvester.
    /// </summary>
    public interface IArticleMetaHarvester : IDocumentHarvester<IArticle>
    {
    }
}
