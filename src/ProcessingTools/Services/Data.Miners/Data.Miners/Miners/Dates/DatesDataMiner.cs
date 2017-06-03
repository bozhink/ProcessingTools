namespace ProcessingTools.Data.Miners.Miners.Dates
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Contracts.Miners.Dates;
    using ProcessingTools.Common.Extensions;

    public class DatesDataMiner : IDatesDataMiner
    {
        private const string RangeSubpattern = @"\s*(?:[–—−‒-]+|to)\s*";

        private const string DaySubpattern = @"(?<!\d)(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:st|nd|rd|th)?\.?(?!\d)";
        private const string DayRangeSubpattern = @"(?:" + DaySubpattern + RangeSubpattern + @")+" + DaySubpattern;

        private const string YearSubpattern = @"(?<!\d)(?:1[6-9][0-9]|20[0-9])[0-9](?!\d)";

        private const string MonthSubpattern = @"(?<![A-Za-z])(?:Jan(?:uary)?|Febr?(?:uary)?|Mar(?:ch)?|Apr(?:il)?|May|June?|July?|Aug(?:ust)?|Sept?(?:ember)?|Oct(?:ober)?|Nov(?:ember)?|Dec(?:ember)?)(?![A-Za-z])";

        private const string MonthRomanSubpattern = @"(?<![IVX])(?:I|II|III|IV|V|VI|VII|VIII|IX|X|XI|XII)(?![IVX])";

        private const string MonthArabicSubpattern = @"(?<!\d)(?:0?[1-9]|1[0-2])(?!\d)";

        public async Task<IEnumerable<string>> Mine(string content)
        {
            var patterns = new string[]
            {
                // DD [month in Arabic] YYYY
                @"(?i)" + DaySubpattern + @"\W{0,4}" + MonthArabicSubpattern + @"\W{0,4}" + YearSubpattern,
                @"(?i)" + DayRangeSubpattern + @"\W{0,4}" + MonthArabicSubpattern + @"\W{0,4}" + YearSubpattern,

                // YYYY [month in Arabic] DD
                @"(?i)" + YearSubpattern + @"\W{0,4}" + MonthArabicSubpattern + @"\W{0,4}" + DaySubpattern,
                @"(?i)" + YearSubpattern + @"\W{0,4}" + MonthArabicSubpattern + @"\W{0,4}" + DayRangeSubpattern,

                // [month string] DD YYYY
                @"(?i)" + MonthSubpattern + @"\W{0,4}" + DaySubpattern + @"\W{0,4}" + YearSubpattern,
                @"(?i)" + MonthSubpattern + @"\W{0,4}" + DayRangeSubpattern + @"\W{0,4}" + YearSubpattern,
                @"(?i)(?:" + MonthSubpattern + @"(?:\W{0,4}" + DaySubpattern + @")?\W{0,4}(?:" + RangeSubpattern + @")?)+\W{0,4}" + YearSubpattern,
                @"(?i)(?:" + MonthSubpattern + @"(?:\W{0,4}" + DaySubpattern + @")?\W{0,4}(?:" + RangeSubpattern + @")?)+\W{0,4}" + YearSubpattern + RangeSubpattern + YearSubpattern,

                // [month in Roman] DD YYYY
                @"(?i)" + MonthRomanSubpattern + @"\W{0,4}" + DaySubpattern + @"\W{0,4}" + YearSubpattern,
                @"(?i)" + MonthRomanSubpattern + @"\W{0,4}" + DayRangeSubpattern + @"\W{0,4}" + YearSubpattern,
                @"(?i)(?:" + MonthRomanSubpattern + @"(?:\W{0,4}" + DaySubpattern + @")?\W{0,4}(?:" + RangeSubpattern + @")?)+\W{0,4}" + YearSubpattern,
                @"(?i)(?:" + MonthRomanSubpattern + @"(?:\W{0,4}" + DaySubpattern + @")?\W{0,4}(?:" + RangeSubpattern + @")?)+\W{0,4}" + YearSubpattern + RangeSubpattern + YearSubpattern,

                // DD [month in Roman] YYYY
                @"(?i)(?:(?:" + DaySubpattern + @"\W{0,4})?" + MonthRomanSubpattern + @"\W{0,4}){1,2}" + YearSubpattern,
                @"(?i)(?:(?:" + DayRangeSubpattern + @"\W{0,4})?" + MonthRomanSubpattern + @"\W{0,4}){1,2}" + YearSubpattern,

                // YYYY [month in Roman] DD
                @"(?i)" + YearSubpattern + @"(?:\W{0,4}" + MonthRomanSubpattern + @"(?:\W{0,4}" + DaySubpattern + @")?){1,2}",
                @"(?i)" + YearSubpattern + @"(?:\W{0,4}" + MonthRomanSubpattern + @"(?:\W{0,4}" + DayRangeSubpattern + @")?){1,2}",
                @"(?i)" + YearSubpattern + @"(?:\W{0,4}(?:" + RangeSubpattern + @")?" + MonthRomanSubpattern + @"(?:\W{0,4}" + DaySubpattern + @")?)+",

                // DD [month string] YYYY
                @"(?i)" + DaySubpattern + @"\W{0,4}(?:\bof\b\W{0,4})?" + MonthSubpattern,
                @"(?i)" + DayRangeSubpattern + @"\W{0,4}(?:\bof\b\W{0,4})" + MonthSubpattern,
                @"(?i)(?:(?:" + DaySubpattern + @"\W{0,4}(?:\bof\b\W{0,4})?)?" + MonthSubpattern + @"(?:" + RangeSubpattern + @")?){2,}",
                @"(?i)(?:(?:" + DaySubpattern + @"\W{0,4}(?:\bof\b\W{0,4})?)?" + MonthSubpattern + @"\W{0,4}){1,2}" + YearSubpattern,
                @"(?i)(?:(?:" + DayRangeSubpattern + @"\W{0,4}(?:\bof\b\W{0,4})?)?" + MonthSubpattern + @"\W{0,4}){1,2}" + YearSubpattern
            };

            var result = await this.GetMatches(content, patterns);

            return result;
        }

        private async Task<IEnumerable<string>> GetMatches(string content, params string[] patterns)
        {
            var matches = new ConcurrentQueue<string>();

            var tasks = new List<Task>();
            foreach (var pattern in patterns)
            {
                tasks.Add(Task.Run(() =>
                {
                    foreach (var item in Regex.Match(content, pattern).AsEnumerable())
                    {
                        matches.Enqueue(item);
                    }
                }));
            }

            await Task.WhenAll(tasks.ToArray());

            var result = new HashSet<string>(matches);
            return result;
        }
    }
}
