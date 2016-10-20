namespace ProcessingTools.BaseLibrary.ZooBank
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;

    public class ZoobankXmlCloner : IDocumentCloner
    {
        private readonly IDocumentFactory documentFactory;
        private readonly ILogger logger;

        public ZoobankXmlCloner(IDocumentFactory documentFactory, ILogger logger)
        {
            if (documentFactory == null)
            {
                throw new ArgumentNullException(nameof(documentFactory));
            }

            this.logger = logger;
        }

        public Task<object> Clone(IDocument document, string content)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            var nlmDocument = this.documentFactory.Create(content);

            return Task.Run<object>(() =>
            {
                this.CloneTaxonomicActsLsid(document, nlmDocument);
                this.CloneArticleLsid(document, nlmDocument);
                this.CloneAuthorsLsid(document, nlmDocument);

                return true;
            });
        }

        private void CloneArticleLsid(IDocument targetDocument, IDocument sourceDocument)
        {
            this.logger?.Log("Reference:");
            try
            {
                var sourceArticleSelfUriList = sourceDocument.SelectNodes(XPathConstants.ArticleZooBankSelfUriXPath);
                int sourceArticleSelfUriListCount = sourceArticleSelfUriList.Count();

                var targetArticleSelfUriList = targetDocument.SelectNodes(XPathConstants.ArticleZooBankSelfUriXPath);
                int targetArticleSelfUriListCount = targetArticleSelfUriList.Count();

                if (sourceArticleSelfUriListCount == targetArticleSelfUriListCount)
                {
                    for (int i = 0; i < targetArticleSelfUriListCount; ++i)
                    {
                        targetArticleSelfUriList.ElementAt(i).InnerXml = sourceArticleSelfUriList.ElementAt(i).InnerXml;
                        this.logger?.Log(targetArticleSelfUriList.ElementAt(i).InnerXml);
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

        private void CloneAuthorsLsid(IDocument targetDocument, IDocument sourceDocument)
        {
            this.logger?.Log("Author(s):");
            try
            {
                var sourceContributorList = sourceDocument.SelectNodes(XPathConstants.ContributorZooBankUriXPath);
                int sourceContributorListCount = sourceContributorList.Count();

                var targetContributorList = targetDocument.SelectNodes(XPathConstants.ContributorZooBankUriXPath);
                int targetContributorListCount = targetContributorList.Count();

                if (sourceContributorListCount == targetContributorListCount)
                {
                    for (int i = 0; i < targetContributorListCount; ++i)
                    {
                        targetContributorList.ElementAt(i).InnerXml = sourceContributorList.ElementAt(i).InnerXml;
                        this.logger?.Log(targetContributorList.ElementAt(i).InnerXml);
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

        private void CloneTaxonomicActsLsid(IDocument targetDocument, IDocument sourceDocument)
        {
            this.logger?.Log("Taxonomic acts:");
            try
            {
                var sourceNomenclaturesList = sourceDocument.SelectNodes(XPathConstants.NomenclatureXPath);
                int sourceNomenclaturesListCount = sourceNomenclaturesList.Count();

                var targetNomenclaturesList = targetDocument.SelectNodes(XPathConstants.NomenclatureXPath);
                int targetNomenclaturesListCount = targetNomenclaturesList.Count();

                if (sourceNomenclaturesListCount == targetNomenclaturesListCount)
                {
                    for (int i = 0; i < targetNomenclaturesListCount; ++i)
                    {
                        var targetObjecIdList = targetNomenclaturesList.ElementAt(i).SelectNodes(XPathConstants.ZooBankObjectIdXPath);
                        int targetObjectIdListCount = targetObjecIdList.Count;

                        if (targetObjectIdListCount > 0)
                        {
                            var sourceObjecIdList = sourceNomenclaturesList.ElementAt(i).SelectNodes(XPathConstants.ZooBankObjectIdXPath);
                            int sourceObjectIdListCount = sourceObjecIdList.Count;

                            if (targetObjectIdListCount == sourceObjectIdListCount)
                            {
                                for (int j = 0; j < targetObjectIdListCount; ++j)
                                {
                                    targetObjecIdList[j].InnerXml = sourceObjecIdList[j].InnerXml;
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
