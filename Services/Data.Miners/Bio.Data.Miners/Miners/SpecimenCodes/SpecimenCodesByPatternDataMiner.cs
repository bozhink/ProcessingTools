namespace ProcessingTools.Bio.Data.Miners.SpecimenCodes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Contracts.Models.SpecimenCodes;
    using Contracts.SpecimenCodes;
    using Models.SpecimenCodes;
    using ProcessingTools.Extensions;
    using ProcessingTools.Extensions.Linq;

    public class SpecimenCodesByPatternDataMiner : ISpecimenCodesByPatternDataMiner
    {
        private readonly IDictionary<string, string> patterns;

        public SpecimenCodesByPatternDataMiner()
        {
            this.patterns = new Dictionary<string, string>
            {
                { @"\bBOLD:[A-Z]{3,3}[0-9]{4,4}\b", "BOLD BIN" },
                { @"\bCAM[0-9]{4,4}\b", "" },
                { @"\bMIC[0-9]{6,6}\b", "" },
                { @"\b(?i:CNC)[A-Z0-9][0-9]{4,5}\b", "http://grbio.org/cool/y2kv-9w3k" },
                { @"\bCNC\s+Type\s+[A-Z0-9][0-9]{4,5}\b", "http://grbio.org/cool/y2kv-9w3k" },
                { @"\bCNCHYM[0-9]{5,5}\b", "" },
                { @"\bHYM[0-9]{8,8}\b", "" },
                { @"\bCPWH-[0-9]{4,4}\b", "" },
                { @"\bGOU[0-9]{4,4}\b", "" },
                { @"\bWMIC[0-9]{4,4}\b", "" }
            };
        }

        public async Task<IEnumerable<ISpecimenCode>> Mine(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return new ISpecimenCode[] { };
            }

            var data = await this.patterns.AsParallel()
                .SelectMany(p => Regex.Match(content, p.Key).ToIEnumerable()
                    .Select(m => new SpecimenCodeResponseModel
                    {
                        Content = m,
                        ContentType = p.Value
                    }))
                .Distinct()
                .ToListAsync();

            return data;
        }
    }
}
