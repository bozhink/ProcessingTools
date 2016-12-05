namespace ProcessingTools.Data.Miners.Miners.Bio.SpecimenCodes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio.SpecimenCodes;
    using ProcessingTools.Data.Miners.Contracts.Models.Bio.SpecimenCodes;
    using ProcessingTools.Data.Miners.Models.Bio.SpecimenCodes;
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
                { @"\bCAM[0-9]{4,4}\b", string.Empty },
                { @"\bMIC[0-9]{6,6}\b", string.Empty },
                { @"\b(?i:CNC)[A-Z0-9][0-9]{4,5}\b", "http://grbio.org/cool/y2kv-9w3k" },
                { @"\bCNC\s+Type\s+[A-Z0-9][0-9]{4,5}\b", "http://grbio.org/cool/y2kv-9w3k" },
                { @"\bCNCHYM[0-9]{5,5}\b", string.Empty },
                { @"\bHYM[0-9]{8,8}\b", string.Empty },
                { @"\bCPWH-[0-9]{4,4}\b", string.Empty },
                { @"\bGOU[0-9]{4,4}\b", string.Empty },
                { @"\bWMIC[0-9]{4,4}\b", string.Empty },
                { @"\bUFPB\.ECH\W\d+", "Echinodermata Collection of Federal University of Paraiba" },
                { @"\b(?:ZMMU\W+)?(?:Op|Lc)[‒–—−-]?\d+(?:\.\d+)?", "Zoological Museum of Moscow State University" },
                { @"\bCASIZ\d+\b", string.Empty },
                { @"\bLACM:?\d+(?:\.\d+)?\b", string.Empty },
                { @"\bW\d+(?:_\d+)?\b", string.Empty },
                { @"\bSIO\W+BIC\s*M\d+\b", string.Empty },
                { @"\bCAS:\d+\b", string.Empty },
                { @"\bFJIQBC\W*\d+", string.Empty },
                { @"\bMNHN\W*IM\W*2000\W*\d+\b", string.Empty },
                { @"\bMZUSP\W*\d+(?:\.\d+)?\b", "Museu de Zoologia da Universidade de São Paulo, São Paulo State, Brazil" },
                { @"\bLBP\W*\d+(?:\.\d+)?\b", "Laboratório de Biologia e Genética de Peixes, São Paulo State, Brazil" }
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
