// <copyright file="JatsArticleMetaHarvester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Meta
{
    using ProcessingTools.Services.Abstractions;
    using ProcessingTools.Services.Contracts.Meta;
    using ProcessingTools.Services.Models.Contracts.Meta;
    using ProcessingTools.Services.Models.Meta;

    /// <summary>
    /// JATS Article meta harvester.
    /// </summary>
    public class JatsArticleMetaHarvester : XPathHarvester<IArticleMetaModel, JatsArticleMetaModel>, IJatsArticleMetaHarvester
    {
    }
}
