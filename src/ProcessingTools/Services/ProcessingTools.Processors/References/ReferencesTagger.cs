// <copyright file="ReferencesTagger.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.References
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Models.Contracts.Layout.Styles.References;
    using ProcessingTools.Processors.Contracts.References;
    using ProcessingTools.Processors.Models.Contracts.References;
    using ProcessingTools.Processors.Models.References;

    /// <summary>
    /// References tagger.
    /// </summary>
    public class ReferencesTagger : IReferencesTagger
    {
        private const int NumberOfSequentalReferenceCitationsPerAuthority = 10;

        private readonly IXmlTransformerFactory xmlTransformerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencesTagger"/> class.
        /// </summary>
        /// <param name="xmlTransformerFactory">Transformer factory.</param>
        public ReferencesTagger(IXmlTransformerFactory xmlTransformerFactory)
        {
            this.xmlTransformerFactory = xmlTransformerFactory ?? throw new ArgumentNullException(nameof(xmlTransformerFactory));
        }

        /// <inheritdoc/>
        public async Task<object> TagAsync(XmlNode context, IEnumerable<IReferenceTagStyleModel> styles)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (styles == null || !styles.Any())
            {
                return false;
            }

            foreach (var style in styles)
            {
                var templates = await this.GetReferencesTemplatesAsync(context, style.Script).ConfigureAwait(false);

                foreach (XmlNode node in context.SelectNodes(style.TargetXPath))
                {
                    node.InnerXml = this.TagReferencecUsingTemplates(node.InnerXml, templates);
                }
            }

            return true;
        }

        private string TagReferencecUsingTemplates(string xml, IEnumerable<IReferenceTemplateItem> templates)
        {
            /*
             * Tag references using generated template
             */
            foreach (var template in templates)
            {
                xml = Regex.Replace(
                    xml,
                    "(?i)(?<!<xref [^>]*>)(?<!<[^>]+)(" + template.Authors + "[’´'s]*\\s*\\(" + template.Year + "\\))",
                    @"<xref ref-type=""bibr"" rid=""" + template.Id + @""">$1</xref>");

                xml = Regex.Replace(
                    xml,
                    "(?i)(?<!<xref [^>]*>)(?<!<[^>]+)(" + template.Authors + "[’´'s]*\\s*\\[" + template.Year + "\\])",
                    @"<xref ref-type=""bibr"" rid=""" + template.Id + @""">$1</xref>");

                xml = Regex.Replace(
                    xml,
                    "(?i)(?<!<xref [^>]*>)(?<!<[^>]+)(" + template.Authors + "[’´'s]*\\s*[\\(\\[]" + template.Year + ")(?=[;:,\\s])",
                    @"<xref ref-type=""bibr"" rid=""" + template.Id + @""">$1</xref>");

                xml = Regex.Replace(
                    xml,
                    "(?i)(?<!<xref [^>]*>)(?<!<[^>]+)(" + template.Authors + "[’´'s]*\\s*\\[[a-z\\d\\W]{0,20}\\]\\s*)(" + template.Year + ")",
                    @"$1<xref ref-type=""bibr"" rid=""" + template.Id + @""">$2</xref>");

                xml = Regex.Replace(
                    xml,
                    "(?i)(?<!<xref [^>]*>)(?<!<[^>]+)(" + template.Authors + "[’´'s]*\\s*" + template.Year + ")",
                    @"<xref ref-type=""bibr"" rid=""" + template.Id + @""">$1</xref>");
            }

            // Polenec 1952, 1954, 1957, 1958, 1959, 1960, 1961a, b, c, d, 1962a
            for (int i = 0; i < NumberOfSequentalReferenceCitationsPerAuthority; ++i)
            {
                for (Match m = Regex.Match(xml, @"<xref ref-type=""bibr""[^>]+>[^<>]+(?<=[^\)\]])</xref>(\s*(?:and|&amp;|\W)\s*\b(\d{2,4}(\W\d{1,4})?[a-z]?|[a-z])\b)*\W\s*\b\d{2,4}(\W\d{1,4})?[a-z]?\b"); m.Success; m = m.NextMatch())
                {
                    string rid = Regex.Replace(m.Value, @"\A.*<xref [^<>]*rid=""(\w*?)""[^<>]*>.*\Z", "$1");

                    string authors = templates
                        .FirstOrDefault(r => r.Id == rid)?.Authors;

                    if (!string.IsNullOrWhiteSpace(authors))
                    {
                        string replace = m.Value;

                        for (Match l = Regex.Match(m.Value, @"(?<=<xref [^>]+>[^<>]*</xref>(\s*(?:and|&amp;|\W)\s*\b(\d{2,4}(\W\d{1,4})?[a-z]?|[a-z])\b)*\W\s*)\b\d{2,4}(\W\d{1,4})?[a-z]?\b"); l.Success; l = l.NextMatch())
                        {
                            try
                            {
                                string id = templates
                                    .FirstOrDefault(r => (r.Authors == authors) && (r.Year == l.Value))?.Id;

                                if (!string.IsNullOrWhiteSpace(id))
                                {
                                    replace = Regex.Replace(
                                        replace,
                                        @"(?<=<xref [^>]+>[^<>]*</xref>(?:\s*(?:and|&amp;|\W)\s*\b(?:\d{2,4}(?:\W\d{1,4})?[a-z]?|[a-z])\b)*\W\s*)\b" + l.Value + @"\b",
                                        @"<xref ref-type=""bibr"" rid=""" + id + @""">" + Regex.Escape(l.Value) + "</xref>");
                                }
                            }
                            catch
                            {
                                // skip
                            }
                        }

                        xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
                    }
                }
            }

            // <xref ref-type="bibr" rid="B38">Kitahara et al. 2012a</xref>, b, c
            for (Match m = Regex.Match(xml, @"<xref ref-type=""bibr"" [^>]+>[^<>]*\d+\W*[a-z]\W*</xref>(\W\s*(\b[a-z]\b))+"); m.Success; m = m.NextMatch())
            {
                string rid = Regex.Replace(m.Value, @"\A.*<xref [^<>]*rid=""(\w+)""[^<>]*>.*\Z", "$1");

                var reference = templates.FirstOrDefault(r => r.Id == rid);
                if (reference != null)
                {
                    string authors = reference.Authors;
                    string year = Regex.Replace(reference.Year, "[A-Za-z]", string.Empty);

                    if (!(string.IsNullOrWhiteSpace(authors) && string.IsNullOrWhiteSpace(year)))
                    {
                        string replace = m.Value;
                        for (Match l = Regex.Match(m.Value, @"(?<=<xref [^>]+>[^<>]*</xref>(\W\s*\b[a-z]\b)*\W\s*)\b[a-z]\b"); l.Success; l = l.NextMatch())
                        {
                            try
                            {
                                string id = templates
                                    .FirstOrDefault(r => (r.Authors == authors) && (r.Year == year + l.Value))?.Id;

                                if (!string.IsNullOrWhiteSpace(id))
                                {
                                    replace = Regex.Replace(
                                        replace,
                                        @"(?<=<xref [^>]+>[^<>]*</xref>(?:\W\s*\b[a-z]\b)*\W\s*)\b" + l.Value + @"\b",
                                        @"<xref ref-type=""bibr"" rid=""" + id + @""">" + l.Value + "</xref>");
                                }
                            }
                            catch
                            {
                                // skip
                            }
                        }

                        xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
                    }
                }
            }

            // This loop must be executed separately because is slow
            foreach (var reference in templates)
            {
                xml = Regex.Replace(
                    xml,
                    "(?i)(?<!<xref [^>]*>)(?<!<[^>]+)(" + reference.Authors + "[’'s]*\\s*[\\(\\[]*(\\d{4,4}[a-z]?[,;\\s–-]*(and|&amp;|[a-z])?\\s*)+)(" + reference.Year + ")",
                    @"$1<xref ref-type=""bibr"" rid=""" + reference.Id + @""">$4</xref>");
                ////xml = Regex.Replace(
                ////    xml,
                ////    "(?i)(?<!<xref [^>]*>)(?<!<[^>]+)(" + reference.Authors + "[’'s]*\\s*[\\(\\[]*(\\d{4,4}[a-z]?[,;\\s–-]*(and|&amp;|[a-z])?\\s*)+([a-z][,;\\s–-]*(and|&amp;|[a-z])?\\s*)*)(" + reference.Year + ")",
                ////    $"$1<xref ref-type=\"bibr\" rid=\"{reference.Id}\">$6</xref>");
            }

            //// TODO: Call here the two loops above
            return xml;
        }

        private async Task<IReferenceTemplateItem[]> GetReferencesTemplatesAsync(XmlNode context, string script)
        {
            var text = await this.xmlTransformerFactory.CreateXmlTransformerFromSourceScript(script)
                .TransformAsync(context)
                .ConfigureAwait(false);

            var referencesTemplatesXml = XDocument.Parse(text);

            var referencesTemplates = referencesTemplatesXml.Descendants("reference")
                .OrderByDescending(r => r.Attribute("authors").Value.Length)
                .ThenBy(r => r.Attribute("year").Value)
                .Select(r => new ReferenceTemplateItem
                {
                    Id = r.Attribute("id").Value,
                    Year = Regex.Escape(r.Attribute("year").Value),
                    Authors = Regex.Replace(r.Attribute("authors").Value, @"\W+", "\\W*") + "[^\\w,]{0,3}"
                })
                .Where(s => !string.IsNullOrWhiteSpace(s.Id) && !string.IsNullOrWhiteSpace(s.Year) && !string.IsNullOrWhiteSpace(s.Authors))
                .ToArray();

            return referencesTemplates;
        }
    }
}
