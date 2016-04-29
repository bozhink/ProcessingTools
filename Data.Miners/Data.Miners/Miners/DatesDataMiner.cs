/*
 01. Oct. 1930
 24.- 29.09.1929
 19/August/2002
 2012/12/10
 15th October 2014
 2nd March 2015
 July 01.2015
 26–30. June, 2014
 29th of April, 2015
 13-III-08-V-1998
 30Aug1923
*/

namespace ProcessingTools.Data.Miners
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using ProcessingTools.Data.Miners.Contracts;
    using ProcessingTools.Extensions;

    public class DatesDataMiner : IDatesDataMiner
    {
        public Task<IQueryable<string>> Mine(string content)
        {
            return Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(content))
                {
                    throw new ArgumentNullException(nameof(content));
                }

                var tasks = new Queue<Task>();

                var internalMiner = new InternalMiner(content);

                tasks.Enqueue(internalMiner.MineDayMonthNumberYear());
                tasks.Enqueue(internalMiner.MineMonthStringDayYear());
                tasks.Enqueue(internalMiner.MineDayMonthRomanYear());
                tasks.Enqueue(internalMiner.MineDayMonthStringYear());
                tasks.Enqueue(internalMiner.MineYearDashMonthDashDay());
                tasks.Enqueue(internalMiner.MineYearMonthRomanDay());

                Task.WaitAll(tasks.ToArray());

                var result = new HashSet<string>(internalMiner.Items);
                return result.AsQueryable();
            });
        }

        private class InternalMiner
        {
            private const string DaySubpattern = @"(?:[1-2][0-9]|3[0-1]|0?[1-9])";
            private const string DayRangeSubpattern = @"(?:(?:" + DaySubpattern + @"\s*[–—−‒-]\s*)+" + DaySubpattern + @"|(?<![^\s–—−‒-])" + DaySubpattern + @")";

            private const string YearSubpattern = @"(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9])";

            private ConcurrentQueue<string> items;
            private string content;

            public InternalMiner(string content)
            {
                if (string.IsNullOrWhiteSpace(content))
                {
                    throw new ArgumentNullException("content");
                }

                this.content = content;
                this.items = new ConcurrentQueue<string>();
            }

            public IEnumerable<string> Items => this.items;

            /// <summary>
            /// Finds dates of format DD [mounth as arabic number] YYYY in text and adds them in List dates.
            /// </summary>
            /// <returns>Task.</returns>
            /// <example>16.6.2013</example>
            public async Task MineDayMonthNumberYear()
            {
                const string Pattern = @"((?i)(?:(?:(?:" + DaySubpattern + @"(?:\s*[–—−‒-]\s*))+|(?<![^\s–—−‒-])" + DaySubpattern + @")[^\w<>]{0,4})?\b(?:1[0-2]|0[1-9]|[1-9])\b[^\w<>]{0,4}" + YearSubpattern + @"\b)";

                await this.content.GetMatchesAsync(new Regex(Pattern))
                    .ContinueWith(this.EnqueueInItems);
            }

            /// <summary>
            /// Finds dates in format YYYY-MM-DD
            /// </summary>
            /// <returns>Task.</returns>
            /// <example>1999-07-27</example>
            public async Task MineYearDashMonthDashDay()
            {
                const string Pattern = @"((?i)\b" + YearSubpattern + @"(?:\s*[–—−‒-]\s*)(?:0?[1-9]|1[1-2])(?:\s*[–—−‒-]\s*)" + DaySubpattern + @"\b)";

                await this.content.GetMatchesAsync(new Regex(Pattern))
                    .ContinueWith(this.EnqueueInItems);
            }

            /// <summary>
            /// Finds dates of format [mounth string] DD YYYY in text and adds them in List dates.
            /// </summary>
            /// <returns>Task.</returns>
            /// <example>March 12.2014</example>
            public async Task MineMonthStringDayYear()
            {
                const string Pattern = @"((?i)(?:(?:Jan(?:uary)?|Febr?(?:uary)?|Mar(?:ch)?|Apr(?:il)?|May|June?|July?|Aug(?:ust)?|Sept?(?:ember)?|Oct(?:ober)?|Nov(?:ember)?|Dec(?:ember)?)\s*(?:[–—−‒-]|to)\s*)+(?:(?:(?:" + DaySubpattern + @"(?:\s*[–—−‒-]\s*))+|(?<!\S)" + DaySubpattern + @")[^\w<>]{0,4})?[^\w<>]{0,4}" + YearSubpattern + @"\b)";

                await this.content.GetMatchesAsync(new Regex(Pattern))
                    .ContinueWith(this.EnqueueInItems);
            }

            /// <summary>
            /// Finds dates of format DD [mounth in roman] YYYY in text and adds them in List dates.
            /// </summary>
            /// <returns>Task.</returns>
            /// <example>22–25.I.2007</example>
            public async Task MineDayMonthRomanYear()
            {
                const string Pattern = @"((?i)(?:(?:" + DayRangeSubpattern + @"[^\w<>]{0,4})?\b(?:I|II|III|IV|V|VI|VII|VIII|IX|X|XI|XII)\b[^\w<>]{0,4}){1,2}" + YearSubpattern + @"\b)";

                await this.content.GetMatchesAsync(new Regex(Pattern))
                    .ContinueWith(this.EnqueueInItems);
            }

            /// <summary>
            /// Finds dates of format YYYY [mounth in roman] DD in text and adds them in List dates.
            /// </summary>
            /// <returns>Task.</returns>
            /// <example>2011.IX.27–29</example>
            /// <example>2012.VIII–X</example>
            public async Task MineYearMonthRomanDay()
            {
                const string Pattern = @"((?i)\b" + YearSubpattern + @"(?:[^\w<>]{0,4}\b(?:I|II|III|IV|V|VI|VII|VIII|IX|X|XI|XII)\b(?:[^\w<>]{0,4}" + DayRangeSubpattern + @")?){1,2})";

                await this.content.GetMatchesAsync(new Regex(Pattern))
                    .ContinueWith(this.EnqueueInItems);
            }

            /// <summary>
            /// Finds dates of format DD [mounth string] YYYY in text and adds them in List dates.
            /// </summary>
            /// <returns>Task.</returns>
            /// <example>24–30 March 2013</example>
            /// <example>18 Jan 2008</example>
            public async Task MineDayMonthStringYear()
            {
                const string MonthSubpattern = @"(?:Jan(?:uary)?|Febr?(?:uary)?|Mar(?:ch)?|Apr(?:il)?|May|June?|July?|Aug(?:ust)?|Sept?(?:ember)?|Oct(?:ober)?|Nov(?:ember)?|Dec(?:ember)?)";
                const string Pattern = @"((?i)(?:(?:" + DayRangeSubpattern + @"[^\w<>]{0,4})?\b" + MonthSubpattern + @"\b[^\w<>]{0,4}){1,2}" + YearSubpattern + @"\b)";

                await this.content.GetMatchesAsync(new Regex(Pattern))
                    .ContinueWith(this.EnqueueInItems);
            }

            private async Task EnqueueInItems(Task<IEnumerable<string>> matches)
            {
                foreach (var item in await matches)
                {
                    this.items.Enqueue(item);
                }
            }
        }
    }
}