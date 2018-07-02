// <copyright file="ParseLowerTaxaWithBasionymStrategy.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Bio.Taxonomy.Strategies
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts.Strategies.Bio.Taxonomy;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Parse lower taxa with basionym strategy.
    /// </summary>
    public class ParseLowerTaxaWithBasionymStrategy : IParseLowerTaxaWithBasionymStrategy
    {
        /// <inheritdoc/>
        public int ExecutionPriority => 500;

        /// <inheritdoc/>
        public Task<object> ParseAsync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.ProcessBasionymAuthorityElements(context);

            foreach (XmlNode lowerTaxon in context.SelectNodes(XPathStrings.LowerTaxonNames + $"[{ElementNames.TaxonNamePart}[@{AttributeNames.Type}='{AttributeValues.Infraspecific}']]"))
            {
                string xml = lowerTaxon.InnerXml.RegexReplace("</?i>", string.Empty);

                const string InfraPattern = @"<tn-part type=""infraspecific-rank"">(?<rank>[^<>]*)</tn-part>\s*<tn-part type=""infraspecific"">([^<>]*)</tn-part>";
                for (Match m = Regex.Match(xml, InfraPattern); m.Success; m = m.NextMatch())
                {
                    string infraSpecificRank = m.Groups["rank"].Value.ToLowerInvariant().Trim(new[] { ' ', '.' });

                    string rank = SpeciesPartsPrefixesResolver.Resolve(infraSpecificRank);
                    string replacement = m.Value.RegexReplace(
                        @"<tn-part type=""infraspecific"">([^<>]*)</tn-part>",
                        @"<tn-part type=""" + rank + @""">$1</tn-part>");

                    xml = Regex.Replace(xml, Regex.Escape(m.Value), replacement);
                }

                lowerTaxon.InnerXml = xml;
            }

            return Task.FromResult<object>(true);
        }

        private void ProcessBasionymAuthorityElements(XmlNode context)
        {
            var document = context.OwnerDocument();

            var matchBasinymAuthorityAndAuthority = new Regex(@"\A\s*(\(.*?\))(\s*)(.*?)\Z");

            foreach (XmlNode node in context.SelectNodes(".//basionym-authority"))
            {
                var fragment = document.CreateDocumentFragment();

                string xml = node.InnerXml.Trim();
                if (!string.IsNullOrWhiteSpace(xml))
                {
                    if (matchBasinymAuthorityAndAuthority.IsMatch(xml))
                    {
                        xml = matchBasinymAuthorityAndAuthority.Replace(xml, @"<tn-part type=""basionym-authority"">$1</tn-part>$2<tn-part type=""authority"">$3</tn-part>");
                    }
                    else
                    {
                        xml = @"<tn-part type=""authority"">" + xml + @"</tn-part>";
                    }
                }

                fragment.InnerXml = " " + xml;
                node.ParentNode.ReplaceChild(fragment, node);
            }
        }
    }
}
