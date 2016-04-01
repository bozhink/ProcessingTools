/*
 * ~16–~61m
 * 28.4–30.0 °C
 * * 30.1–31.2 ppt
 * 1,500 m × 200 m
 * * 15 mM
 * 15 units/ µl
 * 30 s
 * 5 min
 * 8 minutes
 * between 432 and 887 bp
 * 0.6–1.9 mm, 1.1–1.7 × 0.5–0.8 mm
 * 3.5 cm × 3 cm
 * 11 cm x 8 cm
 * * 59 kV
 * * 167 µA
 * 2–4 mm
 * 2.2–2.6 mm
 */

namespace ProcessingTools.Data.Miners
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Contracts;
    using ProcessingTools.Infrastructure.Extensions;

    public class QuantitiesDataMiner : IQuantitiesDataMiner
    {
        public async Task<IQueryable<string>> Mine(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            const string Pattern = @"(?:(?:[\(\)\[\]\{\}–—−‒-]\s*)??\d+(?:[,\.]\d+)?(?:\s*[\(\)\[\]\{\}×\*])?\s*)+?(?:[kdcmµnp][gmMlLVA]|[kdcmµ]mol|meters?|[º°˚]\s*[FC]|[M]?bp|ppt|fe*t|m|mi(?:le)|min(?:ute))\b";

            var items = await content.GetMatchesAsync(new Regex(Pattern));
            var result = new HashSet<string>(items);

            return result.AsQueryable();
        }
    }
}