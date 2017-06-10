namespace ProcessingTools.Data.Miners.Miners.Bio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Services.Data.Contracts;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Constants;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;

    public class TypeStatusDataMiner : ITypeStatusDataMiner
    {
        private const int NumberOfItemsToTake = PagingConstants.MaximalItemsPerPageAllowed;

        private readonly ITypeStatusDataService service;

        public TypeStatusDataMiner(ITypeStatusDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IEnumerable<string>> Mine(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            var matchers = (await this.service.SelectAllAsync())
                .Select(t => t.Name)
                .ToList()
                .Select(t => new Regex(@"(?i)\b" + t + @"s?\b"));

            var matches = new List<string>();
            foreach (var matcher in matchers)
            {
                matches.AddRange(await content.GetMatchesAsync(matcher));
            }

            var result = new HashSet<string>(matches);
            return result;
        }
    }
}
