// <copyright file="JatsArticleMetaHarvester.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Meta
{
    using ProcessingTools.Harvesters.Abstractions;
    using ProcessingTools.Harvesters.Contracts.Meta;
    using ProcessingTools.Harvesters.Models.Contracts.Meta;
    using ProcessingTools.Harvesters.Models.Meta;

    /// <summary>
    /// JATS Article meta harvester.
    /// </summary>
    public class JatsArticleMetaHarvester : XPathHarvester<IArticleMetaModel, JatsArticleMetaModel>, IJatsArticleMetaHarvester
    {
    }
}
