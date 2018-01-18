namespace ProcessingTools.Harvesters.Meta
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Harvesters.Meta;
    using ProcessingTools.Contracts.Models.Harvesters.Meta;
    using ProcessingTools.Harvesters.Models.Meta;

    public class ArticleMetaHarvester : IArticleMetaHarvester
    {
        public Task<IArticle> HarvestAsync(IDocument context)
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
