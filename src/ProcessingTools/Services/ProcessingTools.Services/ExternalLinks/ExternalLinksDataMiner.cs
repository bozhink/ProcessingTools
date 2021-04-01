// <copyright file="ExternalLinksDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.ExternalLinks
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Contracts.Models.ExternalLinks;
    using ProcessingTools.Contracts.Services.ExternalLinks;
    using ProcessingTools.Extensions.Text;
    using ProcessingTools.Services.Models.ExternalLinks;

    /// <summary>
    /// External links data miner.
    /// </summary>
    public class ExternalLinksDataMiner : IExternalLinksDataMiner
    {
        /// <summary>
        /// URI pattern suffix.
        /// </summary>
        public const string UriPatternSuffix = @"(?=(?:&gt;|>)?(?:[,;:\.\)\]]+)?(?:\s|$)|$)";

        /// <summary>
        /// Match URL DOI prefix pattern.
        /// </summary>
        public const string MatchUrlDoiPrefixPattern = @"\A(?<prefix>https?://(?:dx\.)?doi.org/)(?<value>10\.\d{4,5}/.+)\Z";

        /// <summary>
        /// DOI pattern.
        /// </summary>
        public const string DoiPattern = @"(?i)(?<=\bdoi\W{0,3})\d+\S+" + UriPatternSuffix + @"|" +
            @"10\.\d{4,5}/\S+" + UriPatternSuffix;

        /// <summary>
        /// FTP pattern.
        /// </summary>
        public const string FtpPattern = @"(?i)s?ftp://(?:www)?\S+?" + UriPatternSuffix;

        /// <summary>
        /// IP address pattern.
        /// </summary>
        public const string IPAddressPattern = @"\b(?:(?:(?:0?0?[0-9]|0?[0-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])\.){3,3}(?:0?0?[0-9]|0?[0-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5]))\b(?:(?::\d+)?/)?";

        /// <summary>
        /// HTTP pattern.
        /// </summary>
        public const string HttpPattern = @"(?i)https?://(?:www)?\S+?" + UriPatternSuffix + @"|" +
            @"(?i)(?<!://)www\S+?" + UriPatternSuffix + @"|" +
            ////IPAddressPattern + @"(?:\D\S*)" + UriPatternSuffix + @"|" +
            @"[A-Za-z0-9][A-Za-z0-9@~&:\.\-_]*\.(?:com|org|net|edu)\b\S*?" + UriPatternSuffix;

        /// <summary>
        /// PMCID pattern.
        /// </summary>
        public const string PmcidPattern = @"(?i)\bpmc\W*\d+|(?i)(?<=\bpmcid\W*)\d+";

        /// <summary>
        /// PMID pattern.
        /// </summary>
        public const string PmidPattern = @"(?i)(?<=\bpmid\W*)\d+";

        /// <inheritdoc/>
        public Task<IList<IExternalLink>> MineAsync(string context)
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
                { PmcidPattern, ExternalLinkType.Pmcid },
            };

            var data = ExtractData(context, patterns).ToList();

            DataCleansing(data);

            var result = new HashSet<IExternalLink>(data);
            return Task.FromResult<IList<IExternalLink>>(result.ToArray());
        }

        private static void DataCleansing(IEnumerable<ExternalLink> data)
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

                bool hasProtocolPrefix = item.Href.IndexOf("://", StringComparison.InvariantCulture) >= 0;
                bool hasPureUrnStructure = item.Href.IndexOf("urn:", StringComparison.InvariantCulture) == 0;
                if ((item.Type == ExternalLinkType.Uri) && !hasProtocolPrefix && !hasPureUrnStructure)
                {
                    item.Href = "http://" + item.Href.Trim();
                }
            }
        }

        private static IEnumerable<ExternalLink> ExtractData(string content, IDictionary<string, ExternalLinkType> patterns)
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
                            Type = patterns[key],
                            Href = item
                                .Replace("<", "%3C", StringComparison.InvariantCulture)
                                .Replace(">", "%3E", StringComparison.InvariantCulture)
                                .Replace("&", "%26", StringComparison.InvariantCulture),
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
