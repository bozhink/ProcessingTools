namespace ProcessingTools.Processors.Strategies.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts.Processors.Strategies.Bio.Taxonomy;
    using ProcessingTools.Processors.Common.Bio.Taxonomy;

    public class ParseLowerTaxaFromLeftStrategy : IParseLowerTaxaFromLeftStrategy
    {
        private readonly string[,] replaces = ParseLowerTaxaReplacePatterns.Replaces;

        public int ExecutionPriority => 400;

        public async Task<object> ParseAsync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.SelectNodes(XPathStrings.LowerTaxonNames + "[count(text()[normalize-space()!='']) != 0]")
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(node =>
                {
                    node.InnerXml = this.ParseLeftStringMatch(node.InnerXml);
                });

            return await Task.FromResult(true).ConfigureAwait(false);
        }

        private string ParseLeftStringMatch(string text)
        {
            int length = this.replaces.GetLength(0);
            for (int i = 0; i < length; ++i)
            {
                var re = new Regex(@"\A" + this.replaces[i, 0]);
                if (re.IsMatch(text))
                {
                    return re.Replace(text, this.replaces[i, 1]);
                }
            }

            return text;
        }
    }
}
