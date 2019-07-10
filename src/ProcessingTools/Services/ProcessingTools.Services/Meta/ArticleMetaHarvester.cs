// <copyright file="ArticleMetaHarvester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Meta;
using ProcessingTools.Contracts.Services.Models.Meta;

namespace ProcessingTools.Services.Meta
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Services.Models.Meta;

    /// <summary>
    /// Article meta harvester.
    /// </summary>
    public class ArticleMetaHarvester : IArticleMetaHarvester
    {
        /// <inheritdoc/>
        public Task<IArticle> HarvestAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Task.Run<IArticle>(() => new Article
            {
                Doi = context.SelectSingleNode(XPathStrings.ArticleIdOfTypeDoi)?.InnerText,
                Volume = context.SelectSingleNode(XPathStrings.ArticleMetaVolume)?.InnerText,
                Issue = context.SelectSingleNode(XPathStrings.ArticleMetaIssue)?.InnerText,
                FirstPage = context.SelectSingleNode(XPathStrings.ArticleMetaFirstPage)?.InnerText,
                LastPage = context.SelectSingleNode(XPathStrings.ArticleMetaLastPage)?.InnerText,
                Id = context.SelectSingleNode(XPathStrings.ArticleMetaElocationId)?.InnerText,
            });
        }
    }
}
