// <copyright file="ParseLowerTaxaFromLeftStrategy.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Bio.Taxonomy.Strategies
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts.Strategies.Bio.Taxonomy;

    /// <summary>
    /// Parse lower taxa from left strategy.
    /// </summary>
    public class ParseLowerTaxaFromLeftStrategy : IParseLowerTaxaFromLeftStrategy
    {
        private readonly string[,] replaces = ParseLowerTaxaReplacePatterns.Replaces;

        /// <inheritdoc/>
        public int ExecutionPriority => 400;

        /// <inheritdoc/>
        public Task<object> ParseAsync(XmlNode context)
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

            return Task.FromResult<object>(true);
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
