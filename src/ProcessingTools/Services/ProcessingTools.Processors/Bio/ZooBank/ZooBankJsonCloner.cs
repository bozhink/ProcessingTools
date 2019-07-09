﻿// <copyright file="ZooBankJsonCloner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Bio.ZooBank
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.ZooBank.Json;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Common.Constants.Uri;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Processors.Contracts.Bio.ZooBank;

    /// <summary>
    /// ZooBank JSON cloner.
    /// </summary>
    public class ZooBankJsonCloner : IZooBankJsonCloner
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZooBankJsonCloner"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public ZooBankJsonCloner(ILogger<ZooBankJsonCloner> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public Task<object> CloneAsync(IDocument target, ZooBankRegistration source)
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
                this.ProcessArticleLsid(target, source);
                this.ProcessTaxonomicActsLsid(target, source);

                return true;
            });
        }

        private string GetNomenclatureTaxonObjectIdXPath(NomenclaturalAct nomenclaturalAct)
        {
            const string XPathFormat = ".//tp:taxon-treatment/tp:nomenclature/tn[string(../tp:taxon-status)='{0}']{1}/object-id[@content-type='zoobank']";
            switch (nomenclaturalAct.RankGroup.ToLowerInvariant())
            {
                case "family":
                    return string.Format(XPathFormat, "fam. n.", $"[tn-part[@type='family']='{nomenclaturalAct.NameString}']");

                case "genus":
                    return string.Format(XPathFormat, "gen. n.", $"[tn-part[@type='genus']='{nomenclaturalAct.NameString}']");

                case "species":
                    return string.Format(XPathFormat, "sp. n.", $"[tn-part[@type='genus']='{nomenclaturalAct.Parentname}'][tn-part[@type='species']='{nomenclaturalAct.NameString}']");

                default:
                    return null;
            }
        }

        private void ProcessArticleLsid(IDocument target, ZooBankRegistration source)
        {
            string articleLsid = UrlConstants.ZooBankPrefix + source.ReferenceUuid.Trim();
            var selfUriNode = target.SelectSingleNode(XPathStrings.ArticleZooBankSelfUri);
            if (selfUriNode == null)
            {
                throw new InvalidDocumentException("article-meta/self-uri/@content-type='zoobank' is missing.");
            }

            selfUriNode.InnerText = articleLsid;
        }

        private void ProcessTaxonomicActsLsid(IDocument target, ZooBankRegistration source)
        {
            int numberOfNomenclaturalActs = source.NomenclaturalActs.Count;
            int numberOfNewNomenclaturalActs = 0;

            foreach (var nomenclaturalAct in source.NomenclaturalActs)
            {
                this.ResolveEmptyParentNames(source, nomenclaturalAct);

                this.logger.LogDebug(
                    "\n\n{0}{1}{2} {3}",
                    nomenclaturalAct.Parentname,
                    string.IsNullOrEmpty(nomenclaturalAct.Parentname) ? string.Empty : " ",
                    nomenclaturalAct.NameString,
                    nomenclaturalAct.TnuUuid);

                string xpath = this.GetNomenclatureTaxonObjectIdXPath(nomenclaturalAct);
                if (xpath != null)
                {
                    var objectIdNode = target.SelectSingleNode(xpath);
                    if (objectIdNode != null)
                    {
                        objectIdNode.InnerText = UrlConstants.ZooBankPrefix + nomenclaturalAct.TnuUuid.Trim();
                        numberOfNewNomenclaturalActs++;

                        this.logger.LogDebug(
                            "{0}{1}{2} {3}",
                            nomenclaturalAct.Parentname,
                            string.IsNullOrEmpty(nomenclaturalAct.Parentname) ? string.Empty : " ",
                            nomenclaturalAct.NameString,
                            nomenclaturalAct.TnuUuid);
                    }
                }
            }

            this.logger.LogDebug(
                "\n\n\nNumber of nomenclatural acts = {0}.\nNumber of new nomenclatural acts = {1}.\n\n\n",
                numberOfNomenclaturalActs,
                numberOfNewNomenclaturalActs);
        }

        private void ResolveEmptyParentNames(ZooBankRegistration zoobankRegistration, NomenclaturalAct nomenclaturalAct)
        {
            if (nomenclaturalAct.Parentname == string.Empty && nomenclaturalAct.ParentUsageUuid != string.Empty)
            {
                foreach (NomenclaturalAct n in zoobankRegistration.NomenclaturalActs)
                {
                    if (string.Compare(nomenclaturalAct.ParentUsageUuid, n.TnuUuid) == 0)
                    {
                        nomenclaturalAct.Parentname = n.NameString;
                        break;
                    }
                }
            }
        }
    }
}
