namespace ProcessingTools.Bio.Processors.Cloners
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts.Cloners;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;

    public class ZoobankXmlCloner : IZoobankXmlCloner
    {
        private readonly ILogger logger;

        public ZoobankXmlCloner(ILogger logger)
        {
            this.logger = logger;
        }

        public Task<object> Clone(IDocument target, IDocument source)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run<object>(() =>
            {
                this.CloneTaxonomicActsLsid(target, source);
                this.CloneArticleLsid(target, source);
                this.CloneAuthorsLsid(target, source);

                return true;
            });
        }

        private void CloneArticleLsid(IDocument target, IDocument source)
        {
            this.logger?.Log("Reference:");
            try
            {
                var sourceArticleSelfUriList = source.SelectNodes(XPathConstants.ArticleZooBankSelfUriXPath);
                int sourceArticleSelfUriListCount = sourceArticleSelfUriList.Count();

                var targetArticleSelfUriList = target.SelectNodes(XPathConstants.ArticleZooBankSelfUriXPath);
                int targetArticleSelfUriListCount = targetArticleSelfUriList.Count();

                if (sourceArticleSelfUriListCount == targetArticleSelfUriListCount)
                {
                    for (int i = 0; i < targetArticleSelfUriListCount; ++i)
                    {
                        var sourceSelfUriNode = sourceArticleSelfUriList.ElementAt(i);
                        var targetSelfUriNode = targetArticleSelfUriList.ElementAt(i);

                        targetSelfUriNode.InnerText = sourceSelfUriNode.InnerText.Trim();

                        this.logger?.Log(targetSelfUriNode.InnerText);
                    }

                    this.logger?.Log();
                }
                else
                {
                    this.logger?.Log(LogType.Warning, "Number of ZooBank self-uri tags in these files does not match.");
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }
        }

        private void CloneAuthorsLsid(IDocument target, IDocument source)
        {
            this.logger?.Log("Author(s):");
            try
            {
                var sourceContributorUriList = source.SelectNodes(XPathConstants.ContributorZooBankUriXPath);
                int sourceContributorUriListCount = sourceContributorUriList.Count();

                var targetContributorUriList = target.SelectNodes(XPathConstants.ContributorZooBankUriXPath);
                int targetContributorUriListCount = targetContributorUriList.Count();

                if (sourceContributorUriListCount == targetContributorUriListCount)
                {
                    for (int i = 0; i < targetContributorUriListCount; ++i)
                    {
                        var sourceContributorUriNode = sourceContributorUriList.ElementAt(i);
                        var targetContributorUriNode = targetContributorUriList.ElementAt(i);

                        targetContributorUriNode.InnerText = sourceContributorUriNode.InnerText.Trim();

                        this.logger?.Log(targetContributorUriNode.InnerText);
                    }

                    this.logger?.Log();
                }
                else
                {
                    this.logger?.Log(LogType.Warning, "Number of ZooBank uri tags in these files does not match.");
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }
        }

        private void CloneTaxonomicActsLsid(IDocument target, IDocument source)
        {
            this.logger?.Log("Taxonomic acts:");
            try
            {
                var sourceNomenclaturesList = source.SelectNodes(XPathConstants.NomenclatureXPath);
                int sourceNomenclaturesListCount = sourceNomenclaturesList.Count();

                var targetNomenclaturesList = target.SelectNodes(XPathConstants.NomenclatureXPath);
                int targetNomenclaturesListCount = targetNomenclaturesList.Count();

                if (sourceNomenclaturesListCount == targetNomenclaturesListCount)
                {
                    for (int i = 0; i < targetNomenclaturesListCount; ++i)
                    {
                        var targetNomenclatureNode = targetNomenclaturesList.ElementAt(i);

                        var targetObjecIdList = targetNomenclatureNode.SelectNodes(XPathConstants.ZooBankObjectIdXPath);
                        int targetObjectIdListCount = targetObjecIdList.Count;

                        if (targetObjectIdListCount > 0)
                        {
                            var sourceNomenclatureNode = sourceNomenclaturesList.ElementAt(i);

                            var sourceObjecIdList = sourceNomenclatureNode.SelectNodes(XPathConstants.ZooBankObjectIdXPath);
                            int sourceObjectIdListCount = sourceObjecIdList.Count;

                            if (targetObjectIdListCount == sourceObjectIdListCount)
                            {
                                for (int j = 0; j < targetObjectIdListCount; ++j)
                                {
                                    targetObjecIdList[j].InnerText = sourceObjecIdList[j].InnerText.Trim();

                                    this.logger?.Log(targetObjecIdList[j].InnerXml);
                                }
                            }
                            else
                            {
                                this.logger?.Log(LogType.Warning, "Number of ZooBank object-id tags does not match.");
                            }
                        }
                    }

                    this.logger?.Log();
                }
                else
                {
                    this.logger?.Log(LogType.Warning, "Number of nomenclatures tags in these files does not match.");
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }
        }
    }
}
