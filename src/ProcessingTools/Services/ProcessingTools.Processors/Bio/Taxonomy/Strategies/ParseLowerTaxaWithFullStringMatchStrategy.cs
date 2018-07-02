// <copyright file="ParseLowerTaxaWithFullStringMatchStrategy.cs" company="ProcessingTools">
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
    using ProcessingTools.Extensions;

    /// <summary>
    /// Parse lower taxa with full string match strategy.
    /// </summary>
    public class ParseLowerTaxaWithFullStringMatchStrategy : IParseLowerTaxaWithFullStringMatchStrategy
    {
        private readonly string[,] replaces = ParseLowerTaxaReplacePatterns.Replaces;

        /// <inheritdoc/>
        public int ExecutionPriority => 300;

        /// <inheritdoc/>
        public Task<object> ParseAsync(XmlNode context)
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

            context.SelectNodes(XPathStrings.TaxonNamePartsOfLowerTaxonNames + $"[@{AttributeNames.Type}='{AttributeValues.XRank}']")
                .Cast<XmlElement>()
                .AsParallel()
                .ForAll(element =>
                {
                    element.SetAttribute(AttributeNames.Delete, true.ToString().ToLowerInvariant());
                    element.InnerXml = this.ParseFullStringMatch(element.InnerXml);
                });

            foreach (XmlNode node in context.SelectNodes(XPathStrings.TaxonNamePartsOfLowerTaxonNames + $"[@{AttributeNames.Delete}]"))
            {
                node.ReplaceXmlNodeByItsInnerXml();
            }

            return Task.FromResult<object>(true);
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
                var re = new Regex(@"\A" + this.replaces[i, 0] + @"\Z");
                if (re.IsMatch(text))
                {
                    return re.Replace(text, this.replaces[i, 1]);
                }
            }

            return text;
        }
    }
}
