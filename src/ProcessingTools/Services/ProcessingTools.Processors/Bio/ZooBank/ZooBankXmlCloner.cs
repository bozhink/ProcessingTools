// <copyright file="ZooBankXmlCloner.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Bio.ZooBank
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts.Bio.ZooBank;

    /// <summary>
    /// ZooBank XML cloner.
    /// </summary>
    public class ZooBankXmlCloner : IZooBankXmlCloner
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZooBankXmlCloner"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public ZooBankXmlCloner(ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public Task<object> CloneAsync(IDocument target, IDocument source)
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
            this.logger?.Log(message: "Reference:");
            try
            {
                var sourceArticleSelfUriList = source.SelectNodes(XPathStrings.ArticleZooBankSelfUri);
                int sourceArticleSelfUriListCount = sourceArticleSelfUriList.Count();

                var targetArticleSelfUriList = target.SelectNodes(XPathStrings.ArticleZooBankSelfUri);
                int targetArticleSelfUriListCount = targetArticleSelfUriList.Count();

                if (sourceArticleSelfUriListCount == targetArticleSelfUriListCount)
                {
                    for (int i = 0; i < targetArticleSelfUriListCount; ++i)
                    {
                        var sourceSelfUriNode = sourceArticleSelfUriList.ElementAt(i);
                        var targetSelfUriNode = targetArticleSelfUriList.ElementAt(i);

                        targetSelfUriNode.InnerText = sourceSelfUriNode.InnerText.Trim();

                        this.logger?.Log(message: targetSelfUriNode.InnerText);
                    }

                    this.logger?.Log();
                }
                else
                {
                    this.logger?.Log(type: LogType.Warning, message: "Number of ZooBank self-uri tags in these files does not match.");
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(exception: e, message: string.Empty);
            }
        }

        private void CloneAuthorsLsid(IDocument target, IDocument source)
        {
            this.logger?.Log(message: "Author(s):");
            try
            {
                var sourceContributorUriList = source.SelectNodes(XPathStrings.ContributorZooBankUri);
                int sourceContributorUriListCount = sourceContributorUriList.Count();

                var targetContributorUriList = target.SelectNodes(XPathStrings.ContributorZooBankUri);
                int targetContributorUriListCount = targetContributorUriList.Count();

                if (sourceContributorUriListCount == targetContributorUriListCount)
                {
                    for (int i = 0; i < targetContributorUriListCount; ++i)
                    {
                        var sourceContributorUriNode = sourceContributorUriList.ElementAt(i);
                        var targetContributorUriNode = targetContributorUriList.ElementAt(i);

                        targetContributorUriNode.InnerText = sourceContributorUriNode.InnerText.Trim();

                        this.logger?.Log(message: targetContributorUriNode.InnerText);
                    }

                    this.logger?.Log();
                }
                else
                {
                    this.logger?.Log(type: LogType.Warning, message: "Number of ZooBank uri tags in these files does not match.");
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(exception: e, message: string.Empty);
            }
        }

        private void CloneTaxonomicActsLsid(IDocument target, IDocument source)
        {
            this.logger?.Log(message: "Taxonomic acts:");
            try
            {
                var sourceNomenclaturesList = source.SelectNodes(XPathStrings.TaxonTreatmentNomenclature);
                int sourceNomenclaturesListCount = sourceNomenclaturesList.Count();

                var targetNomenclaturesList = target.SelectNodes(XPathStrings.TaxonTreatmentNomenclature);
                int targetNomenclaturesListCount = targetNomenclaturesList.Count();

                if (sourceNomenclaturesListCount == targetNomenclaturesListCount)
                {
                    for (int i = 0; i < targetNomenclaturesListCount; ++i)
                    {
                        var targetNomenclatureNode = targetNomenclaturesList.ElementAt(i);

                        var targetObjecIdList = targetNomenclatureNode.SelectNodes(XPathStrings.ObjectIdOfTypeZooBank);
                        int targetObjectIdListCount = targetObjecIdList.Count;

                        if (targetObjectIdListCount > 0)
                        {
                            var sourceNomenclatureNode = sourceNomenclaturesList.ElementAt(i);

                            var sourceObjecIdList = sourceNomenclatureNode.SelectNodes(XPathStrings.ObjectIdOfTypeZooBank);
                            int sourceObjectIdListCount = sourceObjecIdList.Count;

                            if (targetObjectIdListCount == sourceObjectIdListCount)
                            {
                                for (int j = 0; j < targetObjectIdListCount; ++j)
                                {
                                    targetObjecIdList[j].InnerText = sourceObjecIdList[j].InnerText.Trim();

                                    this.logger?.Log(message: targetObjecIdList[j].InnerXml);
                                }
                            }
                            else
                            {
                                this.logger?.Log(type: LogType.Warning, message: "Number of ZooBank object-id tags does not match.");
                            }
                        }
                    }

                    this.logger?.Log();
                }
                else
                {
                    this.logger?.Log(type: LogType.Warning, message: "Number of nomenclatures tags in these files does not match.");
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(exception: e, message: string.Empty);
            }
        }
    }
}
