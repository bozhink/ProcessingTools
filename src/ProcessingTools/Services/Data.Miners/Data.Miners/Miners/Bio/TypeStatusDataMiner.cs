namespace ProcessingTools.Data.Miners.Miners.Bio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Contracts.Services.Data.Bio;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;

    public class TypeStatusDataMiner : ITypeStatusDataMiner
    {
        private readonly ITypeStatusDataService service;

        public TypeStatusDataMiner(ITypeStatusDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<string[]> MineAsync(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            var data = (await this.service.SelectAsync(null).ConfigureAwait(false))
                .Select(t => t.Name)
                .Distinct()
                .ToList();
            var matchers = data.Select(t => new Regex(@"(?i)\b" + t + @"s?\b"));

            var matches = new List<string>();
            foreach (var matcher in matchers)
            {
                matches.AddRange(await context.GetMatchesAsync(matcher).ConfigureAwait(false));
            }

            return matches.Distinct().ToArray();
        }
    }
}
