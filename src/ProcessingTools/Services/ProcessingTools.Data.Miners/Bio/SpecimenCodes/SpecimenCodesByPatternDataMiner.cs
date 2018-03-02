namespace ProcessingTools.Data.Miners.Bio.SpecimenCodes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Miners.Contracts.Bio.SpecimenCodes;
    using ProcessingTools.Data.Miners.Models.Bio.SpecimenCodes;
    using ProcessingTools.Data.Miners.Models.Contracts.Bio.SpecimenCodes;
    using ProcessingTools.Extensions;

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
                { @"\bLBP\W*\d+(?:\.\d+)?\b", "Laboratório de Biologia e Genética de Peixes, São Paulo State, Brazil" },
                { @"\bUCFC\s+\d\s+\d{3,3}\s+\d{3,3}\b", string.Empty },
                { @"(?<=CMNH[^A-Za-z]*\D)\d{3,3},\d{3,3}", "CMNH" },
                { @"\bUSNMENT\d{8,8}", "USNMENT" },
                { @"(?<=OSUC[^A-Za-z\r\n]*\D)\d{5,6}(?=\D)", "OSUC" },
                { @"(?<=UCRC ENT[^A-Za-z\r\n]*\D)\d{5,6}(?=\D)", "UCRC ENT" },
                { @"(?<=CASENT[^A-Za-z\r\n]*\D)\d{7,7}(?=\D)", "CASENT" }
            };
        }

        public Task<ISpecimenCode[]> MineAsync(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                return Task.FromResult(new ISpecimenCode[] { });
            }

            var data = this.patterns.AsParallel()
                .SelectMany(p => Regex.Match(context, p.Key).AsEnumerable()
                    .Select(m => new SpecimenCodeResponseModel
                    {
                        Content = m,
                        ContentType = p.Value
                    }))
                .Distinct()
                .ToArray<ISpecimenCode>();

            return Task.FromResult(data);
        }
    }
}
