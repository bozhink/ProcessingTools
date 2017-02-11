namespace ProcessingTools.Processors.Strategies.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using Common.Bio.Taxonomy;
    using Contracts.Strategies.Bio.Taxonomy;
    using ProcessingTools.Constants.Schema;

    public class ParseLowerTaxaWithFullStringMatchStrategy : IParseLowerTaxaWithFullStringMatchStrategy
    {
        private readonly string[,] replaces = ParseLowerTaxaReplacePatterns.Replaces;

        public int ExecutionPriority => 300;

        public async Task<object> Parse(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.SelectNodes(XPathStrings.LowerTaxonNameWithNoTaxonNamePart)
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(node =>
                {
                    node.InnerXml = this.ParseFullStringMatch(node.InnerXml);
                });

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Parse taxa with full string match.
        /// </summary>
        /// <param name="text">Text string to be parsed.</param>
        /// <returns>Parsed text string.</returns>
        private string ParseFullStringMatch(string text)
        {
            int length = this.replaces.GetLength(0);
            for (int i = 0; i < length; ++i)
            {
                var re = new Regex(@"\A" + replaces[i, 0] + @"\Z");
                if (re.IsMatch(text))
                {
                    return re.Replace(text, replaces[i, 1]);
                }
            }

            return text;
        }
    }
}
