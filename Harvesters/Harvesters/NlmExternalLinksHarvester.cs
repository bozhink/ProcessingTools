namespace ProcessingTools.Harvesters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Contracts;
    using Extensions;
    using Models;
    using Models.Contracts;
    using Nlm.Publishing.Types;

    public class NlmExternalLinksHarvester : INlmExternalLinksHarvester
    {
        private const string UriPatternPostfix = @"(?=(?:&gt;|>)?\]?[,;\.]?[\)\s]|[\.;\)]$|$)";
        private const string IPAddressPattern = @"\b(?:(?:(?:0?0?[0-9]|0?[0-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])\.){3,3}(?:0?0?[0-9]|0?[0-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5]))\b";

        private const string HttpPattern = @"(?i)https?://(?:www)?\S+?" + UriPatternPostfix + @"|" +
            @"(?i)(?<!://)www\S+?" + UriPatternPostfix + @"|" +
            IPAddressPattern + @"(?:\D\S*)" + UriPatternPostfix;

        private const string FtpPattern = @"(?i)s?ftp://(?:www)?\S+?" + UriPatternPostfix;

        private const string DoiPattern = @"(?i)(?<=\bdoi:?\s*)\d+\S+" + UriPatternPostfix + @"|" +
            @"10\.\d{4,5}/\S+" + UriPatternPostfix;

        private const string PmidPattern = @"(?i)(?<=\bpmid\W?)\d+";

        private const string PmcidPattern = @"(?i)\bpmc\W?\d+|(?i)(?<=\bpmcid\W?)\d+";

        private HashSet<INlmExternalLink> data;

        public NlmExternalLinksHarvester()
        {
            this.data = new HashSet<INlmExternalLink>();
        }

        public IQueryable<INlmExternalLink> Data
        {
            get
            {
                return this.data.AsQueryable();
            }
        }

        public void Harvest(string content)
        {
            this.data = new HashSet<INlmExternalLink>(this.HarvestAsync(content).Result);
        }

        private async Task<IEnumerable<INlmExternalLink>> HarvestAsync(string content)
        {
            var doiItems = await this.DoiHarvest(content);
            var uriItems = await this.UriHarvest(content);
            var ftpItems = await this.FtpHarvest(content);
            var pmidItems = await this.PmidHarvest(content);
            var pmcidItems = await this.PmcidHarvest(content);

            var items = new List<INlmExternalLink>();
            items.AddRange(pmcidItems);
            items.AddRange(pmidItems);
            items.AddRange(ftpItems);
            items.AddRange(uriItems);
            items.AddRange(doiItems);

            return items;
        }

        private async Task<IEnumerable<INlmExternalLink>> UriHarvest(string content)
        {
            var matches = await content.GetMatchesAsync(new Regex(HttpPattern));
            return matches.Select(item => new NlmExternalLinkResponseModel
            {
                Content = item,
                Type = ExternalLinkType.Uri
            });
        }

        private async Task<IEnumerable<INlmExternalLink>> FtpHarvest(string content)
        {
            var matches = await content.GetMatchesAsync(new Regex(FtpPattern));
            return matches.Select(item => new NlmExternalLinkResponseModel
            {
                Content = item,
                Type = ExternalLinkType.Ftp
            });
        }

        private async Task<IEnumerable<INlmExternalLink>> DoiHarvest(string content)
        {
            var matches = await content.GetMatchesAsync(new Regex(DoiPattern));
            return matches.Select(item => new NlmExternalLinkResponseModel
            {
                Content = item,
                Type = ExternalLinkType.Doi
            });
        }

        private async Task<IEnumerable<INlmExternalLink>> PmidHarvest(string content)
        {
            var matches = await content.GetMatchesAsync(new Regex(PmidPattern));
            return matches.Select(item => new NlmExternalLinkResponseModel
            {
                Content = item,
                Type = ExternalLinkType.Pmid
            });
        }

        private async Task<IEnumerable<INlmExternalLink>> PmcidHarvest(string content)
        {
            var matches = await content.GetMatchesAsync(new Regex(PmcidPattern));
            return matches.Select(item => new NlmExternalLinkResponseModel
            {
                Content = item,
                Type = ExternalLinkType.Pmcid
            });
        }
    }
}