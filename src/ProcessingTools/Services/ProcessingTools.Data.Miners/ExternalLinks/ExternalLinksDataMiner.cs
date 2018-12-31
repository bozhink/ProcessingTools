﻿// <copyright file="ExternalLinksDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.ExternalLinks
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Data.Miners.Contracts.ExternalLinks;
    using ProcessingTools.Data.Miners.Models.Contracts.ExternalLinks;
    using ProcessingTools.Data.Miners.Models.ExternalLinks;
    using ProcessingTools.Extensions;

    /// <summary>
    /// External links data miner.
    /// </summary>
    public class ExternalLinksDataMiner : IExternalLinksDataMiner
    {
        private const string UriPatternSuffix = @"(?=(?:&gt;|>)?(?:[,;:\.\)\]]+)?(?:\s|$)|$)";

        private const string MatchUrlDoiPrefixPattern = @"\A(?<prefix>https?://(?:dx\.)?doi.org/)(?<value>10\.\d{4,5}/.+)\Z";

        private const string DoiPattern = @"(?i)(?<=\bdoi\W{0,3})\d+\S+" + UriPatternSuffix + @"|" +
            @"10\.\d{4,5}/\S+" + UriPatternSuffix;

        private const string FtpPattern = @"(?i)s?ftp://(?:www)?\S+?" + UriPatternSuffix;

        private const string IPAddressPattern = @"\b(?:(?:(?:0?0?[0-9]|0?[0-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])\.){3,3}(?:0?0?[0-9]|0?[0-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5]))\b(?:(?::\d+)?/)?";

        private const string HttpPattern = @"(?i)https?://(?:www)?\S+?" + UriPatternSuffix + @"|" +
            @"(?i)(?<!://)www\S+?" + UriPatternSuffix + @"|" +
            ////IPAddressPattern + @"(?:\D\S*)" + UriPatternSuffix + @"|" +
            @"[A-Za-z0-9][A-Za-z0-9@~&:\.\-_]*\.(?:com|org|net|edu)\b\S*?" + UriPatternSuffix;

        private const string PmcidPattern = @"(?i)\bpmc\W*\d+|(?i)(?<=\bpmcid\W*)\d+";
        private const string PmidPattern = @"(?i)(?<=\bpmid\W*)\d+";

        /// <inheritdoc/>
        public Task<IExternalLink[]> MineAsync(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            var patterns = new Dictionary<string, ExternalLinkType>()
            {
                { HttpPattern, ExternalLinkType.Uri },
                { FtpPattern, ExternalLinkType.Ftp },
                { DoiPattern, ExternalLinkType.Doi },
                { PmidPattern, ExternalLinkType.Pmid },
                { PmcidPattern, ExternalLinkType.Pmcid }
            };

            var data = this.ExtractData(context, patterns).ToList();

            this.DataCleansing(data);

            var result = new HashSet<IExternalLink>(data);
            return Task.FromResult(result.ToArray());
        }

        private void DataCleansing(IEnumerable<ExternalLink> data)
        {
            //// var matchDoiPrefixRegex = new Regex(MatchUrlDoiPrefixPattern);
            foreach (var item in data)
            {
                var matchDoiPrefix = Regex.Match(item.Href, MatchUrlDoiPrefixPattern);
                if (matchDoiPrefix.Success)
                {
                    item.Href = matchDoiPrefix.Groups["value"].Value.Trim();
                    item.Type = ExternalLinkType.Doi;
                }

                bool hasProtocolPrefix = item.Href.IndexOf("://") >= 0;
                bool hasPureUrnStructure = item.Href.IndexOf("urn:") == 0;
                if ((item.Type == ExternalLinkType.Uri) && !hasProtocolPrefix && !hasPureUrnStructure)
                {
                    item.Href = "http://" + item.Href.Trim();
                }
            }
        }

        private IEnumerable<ExternalLink> ExtractData(string content, IDictionary<string, ExternalLinkType> patterns)
        {
            var data = new ConcurrentBag<ExternalLink>();

            patterns.Keys
                .AsParallel()
                .ForAll((key) =>
                {
                    content.GetMatches(new Regex(key))
                        .Select(m => m.Trim(new[] { ' ', ',', ':', ';', '.' }))
                        .Distinct()
                        .Select(item => new ExternalLink
                        {
                            Content = item,
                            Href = item.Replace("<", "%3C").Replace(">", "%3E").Replace("&", "%26"),
                            Type = patterns[key]
                        })
                        .ToList()
                        .ForEach(e =>
                        {
                            data.Add(e);
                        });
                });

            return data;
        }
    }
}
