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
        public Task<IArticle> Harvest(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var article = new Article
            {
                Doi = document.SelectSingleNode(XPathStrings.ArticleIdOfTypeDoi)?.InnerText,
                Volume = document.SelectSingleNode(XPathStrings.ArticleMetaVolume)?.InnerText,
                Issue = document.SelectSingleNode(XPathStrings.ArticleMetaIssue)?.InnerText,
                FirstPage = document.SelectSingleNode(XPathStrings.ArticleMetaFirstPage)?.InnerText,
                LastPage = document.SelectSingleNode(XPathStrings.ArticleMetaLastPage)?.InnerText,
                Id = document.SelectSingleNode(XPathStrings.ArticleMetaElocationId)?.InnerText
            };

            return Task.FromResult<IArticle>(article);
        }
    }
}
