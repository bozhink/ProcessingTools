namespace ProcessingTools.Harvesters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Contracts;
    using Extensions;

    public class UrlHarvester : IUrlHarvester
    {
        private ICollection<string> data;

        public UrlHarvester()
        {
            this.data = new HashSet<string>();
        }

        public IQueryable<string> Data
        {
            get
            {
                return this.data.AsQueryable();
            }
        }

        public void Harvest(string content)
        {
            var links = new List<string>();

            const string HttpPattern = @"\b(?:(?:http(?:s)?:\/\/)|(?:www\.))\s*(?:[\w_-]+(?:\.[\w_-]+)+(?:[\w\.,@?^=%&amp;:/~\+#-]*[\w\@?^=%&amp;/~\+#-])?)";
            Regex matchHttp = new Regex(HttpPattern);
            links.AddRange(content.GetMatches(matchHttp));

            const string IPAddressPattern = @"(?:https?|s?ftp)://\s*(?:(?:(?:0?0?[0-9]|0?[0-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])\.){3,3}(?:0?0?[0-9]|0?[0-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5]))(?::\d+)?(/[^<>""\s]*[A-Za-z0-9_/-])?";
            Regex matchIPAddress = new Regex(IPAddressPattern);
            links.AddRange(content.GetMatches(matchIPAddress));

            this.data = new HashSet<string>(links);
        }
    }
}