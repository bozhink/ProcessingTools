// <copyright file="IJatsArticleMetaHarvester.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Meta
{
    using ProcessingTools.Contracts.Models.Meta;

    /// <summary>
    /// JATS Article meta harvester.
    /// </summary>
    public interface IJatsArticleMetaHarvester : IXmlHarvester<IArticleMetaModel>
    {
    }
}
