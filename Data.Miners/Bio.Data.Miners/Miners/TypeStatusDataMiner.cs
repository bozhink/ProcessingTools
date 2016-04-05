namespace ProcessingTools.Bio.Data.Miners.Miners
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Services.Data.Contracts;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Infrastructure.Extensions;

    public class TypeStatusDataMiner : ITypeStatusDataMiner
    {
        private const int NumberOfItemsToTake = DefaultPagingConstants.MaximalItemsPerPageAllowed;

        private readonly ITypeStatusDataService service;

        public TypeStatusDataMiner(ITypeStatusDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        public async Task<IQueryable<string>> Mine(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            var matchers = this.service.All()
                .Select(t => t.Name)
                .ToList()
                .Select(t => new Regex(@"(?i)\b" + t + @"s?\b"));

            var matches = new List<string>();
            foreach (var matcher in matchers)
            {
                matches.AddRange(await content.GetMatchesAsync(matcher));
            }

            var result = new HashSet<string>(matches);
            return result.AsQueryable();
        }
    }
}
