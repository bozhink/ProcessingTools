// <copyright file="IJatsArticleMetaHarvester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Meta
{
    using ProcessingTools.Services.Models.Contracts.Meta;

    /// <summary>
    /// JATS Article meta harvester.
    /// </summary>
    public interface IJatsArticleMetaHarvester : IXmlHarvester<IArticleMetaModel>
    {
    }
}
