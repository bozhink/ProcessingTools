using System.Text.RegularExpressions;

namespace ProcessingTools.Base.Format.Nlm
{
    public class Html : Base
    {
        public Html(string xml)
            : base(xml)
        {
        }

        /// <summary>
        /// Reformat Nlm-Html file.
        /// </summary>
        public void Format()
        {
            this.xml = Regex.Replace(this.xml, "\r", string.Empty);
            this.xml = Regex.Replace(this.xml, "(class=\")\\s*(.*)\\s*(\")", "$1$2$3");
            this.xml = Regex.Replace(this.xml, "<div class=\"uris\">\\s*</div>", string.Empty);

            for (Match m = Regex.Match(this.xml, "<div class=\"mainTitle\">[\\s\\S]*?</div>"); m.Success; m = m.NextMatch())
            {
                string replace = Regex.Replace(m.Value, "\\s+", " ");
                replace = Regex.Replace(replace, @"\(\s*(.*)\s*\)", "($1)");
                this.xml = Regex.Replace(this.xml, Regex.Escape(m.Value), replace);
            }

            for (Match m = Regex.Match(this.xml, "<div class=\"affiliationRow\">[\\s\\S]*?</div>"); m.Success; m = m.NextMatch())
            {
                string replace = Regex.Replace(m.Value, "\\s+", " ");
                this.xml = Regex.Replace(this.xml, Regex.Escape(m.Value), replace);
            }

            this.xml = Regex.Replace(this.xml, "\\s+(</div>|</p>|</th>|</td>)", "$1");
            this.xml = Regex.Replace(this.xml, "(<(p|div|td|th)(>|\\s+[^>]*>))\\s+", "$1");

            // Remove useless anchors in the nomenclature
            this.xml = Regex.Replace(this.xml, "(\\s*<a [^>]*taxonNameLink[^>]*%20%20%20[^>]*></a>)", string.Empty);

            // Make taxonomic names on one row
            this.xml = Regex.Replace(this.xml, "(<a [^>]*class=\"taxonNameLink\"[^>]*>)\\s*(.*?)\\s*(</a>)", "$1$2$3");

            // Change italics around lower taxa
            for (Match m = Regex.Match(this.xml, "<i><span [^>]*class=\"node_taxon-name\"><span>.*?</span></span></i>"); m.Success; m = m.NextMatch())
            {
                string replace = Regex.Replace(m.Value, "</?i>", string.Empty);
                replace = Regex.Replace(replace, "(<a [^>]*>)(.*?)(</a>)", "$1<i>$2</i>$3");
                this.xml = Regex.Replace(this.xml, Regex.Escape(m.Value), replace);
            }

            // Nomenclature with species or subspecies
            for (Match m = Regex.Match(this.xml, "<span [^>]*class=\"node_taxon-name\"><span><a [^>]*>[^<>]*?</a>\\s+<a [^>]*>.*?</span></span>"); m.Success; m = m.NextMatch())
            {
                string replace = Regex.Replace(m.Value, "(<a [^>]*>)(.*?)(</a>)", "$1<i>$2</i>$3");
                this.xml = Regex.Replace(this.xml, Regex.Escape(m.Value), replace);
            }

            // Remove MARK123
            this.xml = Regex.Replace(this.xml, "MARK123\\s*(<span [^>]*class=\"node_taxon-name\"><span>)(<a [^>]*?><i>[\\w\\(\\)\\s]*?</i></a>\\s*)+(<a [^>]*?><i>\\w*?</i></a></span></span>)", "$1$3");

            // Remove italics from subgenus brackets
            this.xml = Regex.Replace(this.xml, @"(<a [^>]*>)<i>\(([^<>]*)\)</i>(</a>)", "$1(<i>$2</i>)$3");

            this.xml = Regex.Replace(this.xml, @"\s+(</span>)", " $1");
            this.xml = Regex.Replace(this.xml, @"(<span[^>]*>)\s+", "$1 ");

            // Remove relicts of <xref ref-type="tn"...
            this.xml = Regex.Replace(this.xml, "<span class=\"xrefLink node_fn\"></span>", string.Empty);

            // Breaks
            this.xml = Regex.Replace(this.xml, "(&lt;br\\s*/&gt;|<br>)", "<br/>");

            // Figures in xref section in nomencltures
            this.xml = Regex.Replace(this.xml, "(</span>–)\\s*(<span)", "$1$2");
            this.xml = Regex.Replace(this.xml, "(</span>)\\s+(<span class=\\WxrefLink node_fig\\W)", "$1$2");

            // Other reorganization
            this.xml = Regex.Replace(this.xml, "(<div class=\"section\">)\n(<a\\s)", "$1$2");
            this.xml = Regex.Replace(this.xml, "(<div class=\"reference[^>]*>.*?)\\s*?(<div class=\"unfloat\"></div>)\\s*?(</div>)", "$1$2$3");
            this.xml = Regex.Replace(this.xml, "><table", ">\n<table");
            this.xml = Regex.Replace(this.xml, "\n\\s+?(</script>)", "\n$1");

            // male and female
            this.xml = Regex.Replace(this.xml, "<i>([♂♀\\s]+)</i>", "$1");

            // Format vertical spaces
            this.xml = Regex.Replace(
                this.xml,
                "(\\s+)(<span [^>]*class=\"node_title\")(>.*?</span>)\\s*(<div class=\"section\">)",
                "$1$2 style=\"padding-bottom:0px\"$3$1$4");

            for (int i = 0; i < 2; i++)
            {
                this.xml = Regex.Replace(
                    this.xml,
                    "(\\s+)(<span [^>]*class=\"node_title1 sec\")(>.*?</span>)\\s*(<span [^>]*class=\"node_title1 sec\")",
                    "$1$2 style=\"padding-bottom:0px\"$3$1$4");
            }

            this.xml = Regex.Replace(
                this.xml,
                "(\\s+)(<span [^>]*class=\"node_title(1 sec)?\")(>.*?</span>)\\s+(<div class=\"taxonTreatmentsHolder secHolder\">)",
                "$1$2 style=\"padding-bottom:0px\"$4$1$5");

            // The slowest part
            this.xml = Regex.Replace(this.xml, @"(\s*?\n)+", "\n");
            this.xml = Regex.Replace(this.xml, "\\s*<p", "\n<p");
            this.xml = Regex.Replace(this.xml, "\\s+<div", "\n<div");
            this.xml = Regex.Replace(this.xml, Regex.Escape("    "), "\t");
            ////xml = Regex.Replace(xml, @"\n\s+", "\n");

            // Final formatting
            this.xml = Regex.Replace(this.xml, "</script><div", "</script>\n<div");
        }
    }
}
