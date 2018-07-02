// <copyright file="DocumentInitialFormatter.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Layout
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;
    using ProcessingTools.Processors.Contracts.Layout;

    /// <summary>
    /// Document initial formatter.
    /// </summary>
    public class DocumentInitialFormatter : IDocumentInitialFormatter
    {
        private const int NumberOfPostFormattingIterations = 3;
        private const string SensuSelector = @"(?:(?i)(?:\bs\.\s*|\bsens?u?\.?\s+)[sl][a-z]*\.?|\bsensu\b)";

        private readonly IInitialFormatTransformerFactory transformerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentInitialFormatter"/> class.
        /// </summary>
        /// <param name="transformerFactory">Transformer factory.</param>
        public DocumentInitialFormatter(IInitialFormatTransformerFactory transformerFactory)
        {
            this.transformerFactory = transformerFactory ?? throw new ArgumentNullException(nameof(transformerFactory));
        }

        /// <inheritdoc/>
        public async Task<object> FormatAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var transformer = this.transformerFactory.Create(context.SchemaType);
            context.Xml = await transformer.TransformAsync(context.Xml).ConfigureAwait(false);

            this.TrimBlockElements(context);

            context.Xml = context.Xml
                .RegexReplace(@"[^\S\r\n ]+", " ")
                .RegexReplace(@"\&lt;\s*br\s*/\s*\&gt;", "<break />");

            context.XmlDocument.RemoveXmlNodes("//break[count(ancestor::aff) + count(ancestor::alt-title) + count(ancestor::article-title) + count(ancestor::chem-struct) + count(ancestor::disp-formula) + count(ancestor::product) + count(ancestor::sig) + count(ancestor::sig-block) + count(ancestor::subtitle) + count(ancestor::td) + count(ancestor::th) + count(ancestor::title) + count(ancestor::trans-subtitle) + count(ancestor::trans-title) = 0]");

            this.InitialRefactor(context);
            this.RefactorEmailTags(context);
            this.FinalRefactor(context);
            this.TrimBlockElements(context);

            return true;
        }

        private void BoldItalic(IDocument document)
        {
            string result = document.Xml;
            result = this.BoldItalicSpaces(result)
                .RegexReplace(@"(?<!\&[a-z]+)([^\w<>\.\(\)]+)(</i>|</b>)", "$2$1")
                .RegexReplace(@"(<i>|<b>)([^\w<>\.\(\)\&]+)", "$2$1")
                .RegexReplace(@"(<i>)([A-Za-z][a-z]{0,2})(</i>)(\.)", "$1$2$4$3")
                .RegexReplace(@"\s*\(\s*(</i>|</b>)", "$1 (")
                .RegexReplace(@"(<i>|<b>)\s*\)\s*", ") $1")
                .RegexReplace(@"<(a|b|i|u|s|sup|sub)>([,\s\.:;\-––])</\1>", "$2")
                .RegexReplace(@"(</i>)(\()", "$1 $2")
                .RegexReplace(@"(<i>)([\.,;:\s]+)", "$2$1")

                // Genus + (Subgenus)
                .RegexReplace(@"<i>([A-Z][a-z\.]+)</i>\s*\(\s*<i>([A-Za-z][a-z\.]+)</i>\s*\)", "<i>$1 ($2)</i>")

                // Genus + (Subgenus) + species
                .RegexReplace(@"<i>([A-Z][a-z\.]+\s\([A-Za-z][a-z\.]+\))</i>\s*<i>([a-z\.\-]+)</i>", "<i>$1 $2</i>")

                .RegexReplace(@"\.</i>(?=</p>)", "</i>.");

            document.Xml = result;
        }

        private string BoldItalicSpaces(string xml)
        {
            string result = xml
                .RegexReplace(@"([\(\[])\s+(<i>|<b>|<u>|<sub>|<sup>)\s*", " $1$2")
                .RegexReplace(@"\s*(</i>|</b>|</u>|</sub>|</sup>)\s+([\)\]])", "$1$2 ")
                .RegexReplace(@"</(b|i|u|s|sup|sub)>\s+<\1>", " ")
                .RegexReplace(@"<(b|i|u|s|sup|sub)>\s+</\1>", " ")
                .RegexReplace(@"</(b|i|u|s|sup|sub)><\1>", string.Empty)
                .RegexReplace(@"<(b|i|u|s|sup|sub)></\1>", string.Empty);

            return result;
        }

        private void FinalRefactor(IDocument document)
        {
            string result = document.Xml

                // Subspecies, subgenera, etc.
                .RegexReplace(@"<i>([A-Z][a-z]+)</i>\.\s*<i>([a-z]+)</i>", "<i>$1. $2</i>")
                .RegexReplace(@"<i>([A-Z][a-z]*\.\s+[a-z]+)</i>\.\s*<i>([a-z]+)</i>", "<i>$1. $2</i>")
                .RegexReplace(@"(<i>[A-Z][a-z]*\.?\s*\([A-Za-z][a-z]*\.?)(</i>)(\))", "$1$3$2")
                .RegexReplace(@"</i>\s+<i>", " ")
                .RegexReplace(@"</i><i>", string.Empty)

                // Supplementary materials external link
                .RegexReplace("(<ext-link ext-link-type=\")uri(\" [^>]*xlink:href=\")doi:\\s+", "$1doi$2")

                // sensu lato & stricto
                .RegexReplace(@"(?<=\W" + SensuSelector + @")</i>\.", ".</i>")
                .RegexReplace(@"(?<=\w)\s+(?=" + SensuSelector + @"</i>)", "</i> <i>");

            result = this.RemoveEmptyTags(result)

                // Remove empty lines
                .RegexReplace("\n\\s*(?=\n)", string.Empty);

            document.Xml = result;
        }

        private void FormatOpenCloseTags(IDocument document)
        {
            this.ProcessInlineElementWhiteSpaces(document, "//i | //em | //italic | //Italic");
            this.ProcessInlineElementWhiteSpaces(document, "//b | //strong | //bold | //Bold");
            this.ProcessInlineElementWhiteSpaces(document, "//u | //underline");
            this.ProcessInlineElementWhiteSpaces(document, "//s | //strike");
            this.ProcessInlineElementWhiteSpaces(document, "//sup");
            this.ProcessInlineElementWhiteSpaces(document, "//sub");
            this.ProcessInlineElementWhiteSpaces(document, "//monospace");
            this.ProcessInlineElementWhiteSpaces(document, "//article-title[ancestor::ref] | //source | //issue-title | //year | //month | //day | //volume | //fpage | //lpage");
        }

        private void FormatPageBreaks(IDocument document)
        {
            var numberOfPageBreakComments = document.SelectNodes("//comment()[string(.) = 'PageBreak']").LongCount();
            if (numberOfPageBreakComments < 1L)
            {
                return;
            }

            string result = document.Xml
                .RegexReplace(@"(<!--PageBreak-->)(\s+)(<!--PageBreak-->)", "$1$3$2")
                .RegexReplace(@"(<p>|<tp:nomenclature-citation>|<title>|<label>)((?:\s*<!--PageBreak-->\s*)+)", "$2$1")
                .RegexReplace(@"<(t[hd])\b[^>]*>((?:\s*<!--PageBreak-->\s*)+)</\1>", "$2")
                .RegexReplace(@"(<t[hd]\b[^>]*>)((?:\s*<!--PageBreak-->\s*)+)", "$2$1")
                .RegexReplace(@"<(tr)\b[^>]*>((?:\s*<!--PageBreak-->\s*)+)</\1>", "$2")
                .RegexReplace(@"(<tr\b[^>]*>)((?:\s*<!--PageBreak-->\s*)+)", "$2$1")
                .RegexReplace(@"((\s*)<ref [^>]*>.*?)((<!--PageBreak-->)+)", "$2$3$1")
                .RegexReplace(@"(<i>[^<>]*?)((<!--PageBreak-->)+)", "$2$1")
                .RegexReplace(@"(<kwd>.*?)((?:\s*<!--PageBreak-->)+)(.*</kwd>)", "$2$1$3")
                .RegexReplace(@"<(title|label|kwd|p)\b[^>]*>((?:\s*<!--PageBreak-->\s*)+)</\1>", "$2")
                .RegexReplace(@"<(b|i)\b[^>]*>((?:\s*<!--PageBreak-->\s*)+)</\1>", "$2")
                .RegexReplace(@"(\s*)(<xref-group>[\s\S]*?)((<!--PageBreak-->)+)([\s\S]*?</xref-group>)", "$1$2$5$1$3");

            document.Xml = result;
        }

        private void FormatPunctuation(IDocument document)
        {
            string result = document.Xml
                .RegexReplace(@"\s*([\(\[])\s+", " $1")
                .RegexReplace(@"\s+([\)\]])\s*", "$1 ")
                .RegexReplace(@"([\)\]])\s+([\)\]])", "$1$2 ")
                .RegexReplace(@"([\(\[])\s+([\(\[])", " $1$2");

            document.Xml = result;
        }

        private void FormatReferances(IDocument document)
        {
            string result = document.Xml
                .RegexReplace(@"(</source>)((?i)doi:?)", "$1 $2")
                .RegexReplace(@"</volume>\s+\(<issue>", "</volume>(<issue>")
                .RegexReplace(@"</issue>(\))+", "</issue>)")
                .RegexReplace(@"<role>Ed</role>", "<role>Ed.</role>")
                .RegexReplace(@"(?<=</role>\))[\.,;:]", string.Empty)
                .RegexReplace(@"(?<=<article-title>)\.\s+", string.Empty)
                .RegexReplace(@"[,;](?=</surname>)", string.Empty);

            document.Xml = result;
        }

        private void InitialRefactor(IDocument document)
        {
            this.FormatOpenCloseTags(document);
            this.BoldItalic(document);
            this.FormatWrongFloatObjectLabels(document);
            this.FormatReferances(document);
            this.FormatDoiNotations(document);
            this.FormatPageBreaks(document);

            // male and female
            document.Xml = document.Xml
                .RegexReplace(@"<i>([♂♀\s]+)</i>", "$1");

            // Post-formatting
            for (int i = 0; i < NumberOfPostFormattingIterations; i++)
            {
                this.FormatOpenCloseTags(document);
                this.BoldItalic(document);
                this.FormatPunctuation(document);
                document.Xml = this.RemoveEmptyTags(document.Xml);
            }
        }

        private void FormatDoiNotations(IDocument document)
        {
            document.Xml = document.Xml
                .RegexReplace(@"(?i)(?<=\bdoi:?)[^\S ]*(?=\d)", " ");
        }

        private void FormatWrongFloatObjectLabels(IDocument document)
        {
            document.Xml = document.Xml
                .RegexReplace(@"(\s*)(<caption>\s*<p>)\s*<b>\s*((Figure|Map|Plate|Table|Suppl|Box)[^<>]*?)\s*</b>", "$1<label>$3</label>$1$2");
        }

        private void ProcessBlockElementWhiteSpaces(IDocument document, string xpath)
        {
            foreach (var node in document.SelectNodes(xpath))
            {
                node.InnerXml = node.InnerXml
                    .RegexReplace(@"\s+", " ")
                    .Trim();
            }
        }

        private void ProcessInlineElementWhiteSpaces(IDocument document, string xpath)
        {
            foreach (var node in document.SelectNodes(xpath))
            {
                bool beginsWithWhiteSpace = Regex.IsMatch(node.InnerXml, @"\A\s+");
                bool endsWithWhiteSpace = Regex.IsMatch(node.InnerXml, @"\s+\Z");

                if (beginsWithWhiteSpace || endsWithWhiteSpace)
                {
                    // TODO: Needs revision
                    try
                    {
                        node.InnerXml = node.InnerXml.Trim();

                        var replacement = node.OwnerDocument.CreateDocumentFragment();
                        replacement.InnerXml = (beginsWithWhiteSpace ? " " : string.Empty) + node?.OuterXml + (endsWithWhiteSpace ? " " : string.Empty);

                        node?.ParentNode?.ReplaceChild(replacement, node);
                    }
                    catch
                    {
                        // Skip
                    }
                }
            }
        }

        private void RefactorEmailTags(IDocument document)
        {
            var matchMultipleEmails = new Regex(@"(?<!<[^<>]+)(?<=\w)(\W*\s+\W*)(?=\w)(?![^<>]+>)");
            foreach (var email in document.SelectNodes("//email"))
            {
                email.InnerXml = email.InnerXml
                    .RegexReplace(@"\A\s+|\s+\Z", string.Empty);

                if (matchMultipleEmails.IsMatch(email.InnerXml))
                {
                    var fragment = document.XmlDocument.CreateDocumentFragment();
                    fragment.InnerXml = matchMultipleEmails.Replace(email.OuterXml, @"</email>$1<email>");
                    email.ParentNode.ReplaceChild(fragment, email);
                }
            }
        }

        private string RemoveEmptyTags(string xml)
        {
            string result = xml
                .RegexReplace(
                    @"<p>\s*</p>|<xref-group>\s*</xref-group>|<i></i>|<i\s*/>|<sup\s*/>|<sub\s*/>|<b\s*/>|<label\s*/>|<b></b>|<kwd>\s*</kwd>|<sup></sup>|<sub></sub>|<mixed-citation [^>]*>\s*</mixed-citation>|<tp:nomenclature-citation>\s*</tp:nomenclature-citation>|<ref [^>]*>\s*</ref>|<source></source>|<u></u>|<u\s*/>|<monospace></monospace>|<monospace\s*/>",
                    string.Empty)
                .RegexReplace(
                    @"<i>\s+</i>|<b>\s+</b>|<sup>\s+</sup>|<sub>\s+</sub>|<source>\s+</source>|<u>\s+</u>|<monospace>\s+</monospace>",
                    " ");

            return result;
        }

        private void TrimBlockElements(IDocument document)
        {
            this.ProcessBlockElementWhiteSpaces(document, "//title | //label | //article-title[not(ancestor::ref)] | //p[not(ancestor::p)][not(ancestor::li)][not(ancestor::td)][not(ancestor::th)][not(ancestor::title)][not(ancestor::label)] | //license-p | //xref-group");
            this.ProcessBlockElementWhiteSpaces(document, "//mixed-citation | //element-citation");
            this.ProcessBlockElementWhiteSpaces(document, "//tp:nomenclature-citation");
            this.ProcessBlockElementWhiteSpaces(document, "//kwd");
            this.ProcessBlockElementWhiteSpaces(document, "//attrib");
            this.ProcessBlockElementWhiteSpaces(document, "//def");
            this.ProcessBlockElementWhiteSpaces(document, "//li");
            this.ProcessBlockElementWhiteSpaces(document, "//th | //td");
            this.ProcessBlockElementWhiteSpaces(document, "//value");
        }
    }
}
