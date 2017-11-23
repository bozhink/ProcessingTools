namespace ProcessingTools.Processors.Processors
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Processors.Contracts;

    public class TextQueryReplacer : ITextQueryReplacer
    {
        public Task<string> ReplaceAsync(string content, string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException(nameof(query));
            }

            return Task.Run(() =>
            {
                if (string.IsNullOrEmpty(content))
                {
                    return content;
                }

                string[] queryItems;
                if (query.Contains("\n"))
                {
                    queryItems = query.Split(new[] { '\n' })
                        .Select(q => q.Replace("\r", string.Empty))
                        .ToArray();
                }
                else
                {
                    queryItems = File.ReadAllLines(query);
                }

                string result = content;
                for (int i = 0; i + 1 < queryItems.Length; i += 2)
                {
                    string pattern = queryItems[i];
                    string replacement = queryItems[i + 1];

                    result = Regex.Replace(input: result, pattern: pattern, replacement: replacement);
                }

                return result;
            });
        }
    }
}
