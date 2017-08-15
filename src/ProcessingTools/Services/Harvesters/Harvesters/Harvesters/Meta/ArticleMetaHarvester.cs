namespace ProcessingTools.Harvesters.Harvesters.Meta
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Harvesters.Meta;
    using Contracts.Models.Meta;
    using Models.Meta;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;

    public class ArticleMetaHarvester : IArticleMetaHarvester
    {
        public Task<IArticle> Harvest(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var article = new Article
            {
                Doi = context.SelectSingleNode(XPathStrings.ArticleIdOfTypeDoi)?.InnerText,
                Volume = context.SelectSingleNode(XPathStrings.ArticleMetaVolume)?.InnerText,
                Issue = context.SelectSingleNode(XPathStrings.ArticleMetaIssue)?.InnerText,
                FirstPage = context.SelectSingleNode(XPathStrings.ArticleMetaFirstPage)?.InnerText,
                LastPage = context.SelectSingleNode(XPathStrings.ArticleMetaLastPage)?.InnerText,
                Id = context.SelectSingleNode(XPathStrings.ArticleMetaElocationId)?.InnerText
            };

            return Task.FromResult<IArticle>(article);
        }
    }
}
