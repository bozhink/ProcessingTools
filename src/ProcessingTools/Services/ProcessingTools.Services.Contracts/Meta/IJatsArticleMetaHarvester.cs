// <copyright file="IJatsArticleMetaHarvester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Meta;

namespace ProcessingTools.Contracts.Services.Meta
{
    /// <summary>
    /// JATS Article meta harvester.
    /// </summary>
    public interface IJatsArticleMetaHarvester : IXmlHarvester<IArticleMetaModel>
    {
    }
}
