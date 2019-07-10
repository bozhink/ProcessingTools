﻿// <copyright file="HigherTaxaParserWithDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Bio.Taxonomy;

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Extensions;
    using ProcessingTools.Services.Models.Bio.Taxonomy;

    /// <summary>
    /// Generic higher taxa parser with data service.
    /// </summary>
    /// <typeparam name="TService">Type of the data service.</typeparam>
    /// <typeparam name="T">Type of the data service model.</typeparam>
    public class HigherTaxaParserWithDataService<TService, T> : IHigherTaxaParserWithDataService<TService, T>
        where TService : class, ITaxonRankResolver
        where T : ITaxonRank
    {
        private readonly TService service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HigherTaxaParserWithDataService{TService, T}"/> class.
        /// </summary>
        /// <param name="service">Taxon rank data service.</param>
        /// <param name="logger">Logger.</param>
        public HigherTaxaParserWithDataService(TService service, ILogger<HigherTaxaParserWithDataService<TService, T>> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<long> ParseAsync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var uniqueHigherTaxaList = new HashSet<string>(context
                .ExtractUniqueNonParsedHigherTaxa()
                .Select(n => n.ToFirstLetterUpperCase()))
                .ToArray();

            if (uniqueHigherTaxaList.Length < 1)
            {
                return uniqueHigherTaxaList.LongLength;
            }

            var response = await this.service.ResolveAsync(uniqueHigherTaxaList);
            if (response == null)
            {
                throw new ServiceReturnedNullException("Current taxa rank data service instance returned null.");
            }

            var resolvedTaxa = response
                .Select(t => new TaxonRankResponseModel
                {
                    ScientificName = t.ScientificName,
                    Rank = t.Rank.MapTaxonRankTypeToTaxonRankString(),
                });

            long numberOfResolvedTaxa = resolvedTaxa.LongCount();
            if (numberOfResolvedTaxa > 0L)
            {
                foreach (var scientificName in uniqueHigherTaxaList)
                {
                    var ranks = resolvedTaxa.Where(
                        t =>
                            !string.IsNullOrWhiteSpace(t.Rank) &&
                            string.Compare(t.ScientificName, scientificName, true) == 0)
                        .Select(t => t.Rank.ToLowerInvariant())
                        .Distinct()
                        .ToList();

                    int numberOfRanks = ranks?.Count ?? 0;
                    switch (numberOfRanks)
                    {
                        case 0:
                            this.ProcessZeroRanksCase(scientificName);
                            break;

                        case 1:
                            this.ProcessSingleRankCase(scientificName, ranks, context);
                            break;

                        default:
                            this.ProcessMultipleRanksCase(scientificName, ranks);
                            break;
                    }
                }
            }

            return numberOfResolvedTaxa;
        }

        private void ProcessMultipleRanksCase(string scientificName, IEnumerable<string> ranks)
        {
            this.logger.LogWarning("{0} --> Multiple matches.", scientificName);
            foreach (var rank in ranks)
            {
                this.logger.LogDebug("{0} --> {1}", scientificName, rank);
            }
        }

        private void ProcessSingleRankCase(string scientificName, IEnumerable<string> ranks, XmlNode context)
        {
            string rank = ranks.Single();

            string xpath = $".//tn[@type='higher'][not(tn-part)][translate(normalize-space(.),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='{scientificName.ToUpperInvariant()}']";

            context?.SelectNodes(xpath)
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(tn =>
                {
                    XmlElement taxonNamePart = tn.OwnerDocument.CreateElement(ElementNames.TaxonNamePart);
                    taxonNamePart.SetAttribute(AttributeNames.Type, rank);
                    taxonNamePart.InnerXml = tn.InnerXml;
                    tn.InnerXml = taxonNamePart.OuterXml;
                });
        }

        private void ProcessZeroRanksCase(string scientificName)
        {
            this.logger.LogWarning("{0} --> No match or error.\n", scientificName);
        }
    }
}
