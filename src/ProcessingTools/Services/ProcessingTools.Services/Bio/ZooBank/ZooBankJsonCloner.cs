// <copyright file="ZooBankJsonCloner.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.ZooBank
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.ZooBank.Json;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Common.Constants.Uri;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Bio.ZooBank;

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
            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run<object>(() =>
            {
                ProcessArticleLsid(target, source);
                this.ProcessTaxonomicActsLsid(target, source);

                return true;
            });
        }

        private static string GetNomenclatureTaxonObjectIdXPath(NomenclaturalAct nomenclaturalAct)
        {
            const string XPathFormat = ".//tp:taxon-treatment/tp:nomenclature/tn[string(../tp:taxon-status)='{0}']{1}/object-id[@content-type='zoobank']";
            switch (nomenclaturalAct.RankGroup.ToUpperInvariant())
            {
                case "FAMILY":
                    return string.Format(CultureInfo.InvariantCulture, XPathFormat, "fam. n.", $"[tn-part[@type='family']='{nomenclaturalAct.NameString}']");

                case "GENUS":
                    return string.Format(CultureInfo.InvariantCulture, XPathFormat, "gen. n.", $"[tn-part[@type='genus']='{nomenclaturalAct.NameString}']");

                case "SPECIES":
                    return string.Format(CultureInfo.InvariantCulture, XPathFormat, "sp. n.", $"[tn-part[@type='genus']='{nomenclaturalAct.Parentname}'][tn-part[@type='species']='{nomenclaturalAct.NameString}']");

                default:
                    return null;
            }
        }

        private static void ProcessArticleLsid(IDocument target, ZooBankRegistration source)
        {
            string articleLsid = UrlConstants.ZooBankPrefix + source.ReferenceUuid.Trim();
            var selfUriNode = target.SelectSingleNode(XPathStrings.ArticleZooBankSelfUri);
            if (selfUriNode is null)
            {
                throw new InvalidDocumentException("article-meta/self-uri/@content-type='zoobank' is missing.");
            }

            selfUriNode.InnerText = articleLsid;
        }

        private static void ResolveEmptyParentNames(ZooBankRegistration zoobankRegistration, NomenclaturalAct nomenclaturalAct)
        {
            if (string.IsNullOrEmpty(nomenclaturalAct.Parentname) && !string.IsNullOrEmpty(nomenclaturalAct.ParentUsageUuid))
            {
                foreach (NomenclaturalAct n in zoobankRegistration.NomenclaturalActs)
                {
                    if (string.Compare(nomenclaturalAct.ParentUsageUuid, n.TnuUuid, StringComparison.InvariantCulture) == 0)
                    {
                        nomenclaturalAct.Parentname = n.NameString;
                        break;
                    }
                }
            }
        }

        private void ProcessTaxonomicActsLsid(IDocument target, ZooBankRegistration source)
        {
            int numberOfNomenclaturalActs = source.NomenclaturalActs.Count;
            int numberOfNewNomenclaturalActs = 0;

            foreach (var nomenclaturalAct in source.NomenclaturalActs)
            {
                ResolveEmptyParentNames(source, nomenclaturalAct);

                this.logger.LogDebug($"\n\n{nomenclaturalAct.Parentname}{(string.IsNullOrEmpty(nomenclaturalAct.Parentname) ? string.Empty : " ")}{nomenclaturalAct.NameString} {nomenclaturalAct.TnuUuid}");

                string xpath = GetNomenclatureTaxonObjectIdXPath(nomenclaturalAct);
                if (xpath != null)
                {
                    var objectIdNode = target.SelectSingleNode(xpath);
                    if (objectIdNode != null)
                    {
                        objectIdNode.InnerText = UrlConstants.ZooBankPrefix + nomenclaturalAct.TnuUuid.Trim();
                        numberOfNewNomenclaturalActs++;

                        this.logger.LogDebug($"{nomenclaturalAct.Parentname}{(string.IsNullOrEmpty(nomenclaturalAct.Parentname) ? string.Empty : " ")}{nomenclaturalAct.NameString} {nomenclaturalAct.TnuUuid}");
                    }
                }
            }

            this.logger.LogDebug($"\n\n\nNumber of nomenclatural acts = {numberOfNomenclaturalActs}.\nNumber of new nomenclatural acts = {numberOfNewNomenclaturalActs}.\n\n\n");
        }
    }
}
