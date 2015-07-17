using System.Text.RegularExpressions;

namespace Base.Format.Nlm
{
    public class Nlm : Base
    {
        public Nlm()
            : base()
        {
        }

        public Nlm(string xml)
            : base(xml)
        {
        }

        /// <summary>
        /// Generate NLM Xml file from given NLM-like system xml
        /// </summary>
        public void Format()
        {
            // DOCTYPE
            ////xml = Regex.Replace(xml, @"\s*<\!DOCTYPE [^>]*?>\s*", "\n");
            this.xml = Regex.Replace(this.xml, @"(<\?.*?\?>)", "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<!DOCTYPE article PUBLIC \"-//TaxonX//DTD Taxonomic Treatment Publishing DTD v0 20100105//EN\" \"tax-treatment-NS0.dtd\">");
            ////xml = Regex.Replace(xml, @"<\!DOCTYPE [^>]*?>", "<!DOCTYPE article PUBLIC \"-//TaxonX//DTD Taxonomic Treatment Publishing DTD v0 20100105//EN\" \"tax-treatment-NS0.dtd\">");
            ////xml = Regex.Replace(xml, @"<article [^>]*>", "<article article-type=\"research-article\" xmlns:mml=\"http://www.w3.org/1998/Math/MathML\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:tp=\"http://www.plazi.org/taxpub\">");

            this.xml = Regex.Replace(this.xml, " unfold=\"true\"", string.Empty);
            this.xml = Regex.Replace(this.xml, "<tp:taxon-name-part [^>]*(/>|>\\s*</tp:taxon-name-part>)", string.Empty);
            this.xml = Regex.Replace(this.xml, "<tp:taxon-name [^>]*>", "<tp:taxon-name>");
            this.xml = Regex.Replace(this.xml, " full-name=\"(.*?)\">\\w+\\.", ">$1");

            this.xml = Regex.Replace(this.xml, @"(<tp:taxon-name-part [^>]*>)\s*\((.*?)\)\s*(</tp:taxon-name-part>)", "($1$2$3)"); // Subgenus
            this.xml = Regex.Replace(this.xml, @"(<tp:nomenclature-citation>)\s*[=≡]\s*((<italic>)?<tp:taxon-name>)", "$1$2"); // Remove "=" sign in synonymy
            this.xml = Regex.Replace(
                this.xml,
                @"(?<=<tp:nomenclature-citation>)\s*(.*?)?(<italic>)?(<tp:taxon-name>.*?</tp:taxon-name>)(</italic>)?(.+?)\s*(?=</tp:nomenclature-citation>)",
                "$3<comment> $1$5</comment>");

            // Format xref-group tags
            for (Match m = Regex.Match(this.xml, "<xref-group>[\\s\\S]*?</xref-group>"); m.Success; m = m.NextMatch())
            {
                string replace = Regex.Replace(m.Value, "\\s+", " ");
                replace = Regex.Replace(replace, "(<xref-group>\\s*|\\s*</xref-group>)", string.Empty);
                replace = Regex.Replace(replace, @"(</xref>)(.*?)(<xref [^>]*>)", "$1$3$2");
                replace = Regex.Replace(replace, @"(<xref [^>]*>)\s+", "$1");

                this.xml = Regex.Replace(this.xml, Regex.Escape(m.Value), replace);
            }

            this.xml = Regex.Replace(this.xml, @"(<comment>)\s+", "$1 ");
            this.xml = Regex.Replace(this.xml, @"(<comment>)\s+([:,;\.])\s*", "$1$2 ");

            // Remove punctuation after taxon-authority
            this.xml = Regex.Replace(this.xml, @"(</tp:taxon-authority>)\W+(\n)", "$1$2");

            // Format object-id tags
            int maxNumOfObjectId = 16;
            for (int i = 0; i < maxNumOfObjectId; i++)
            {
                this.xml = Regex.Replace(this.xml, "(\\s*?)(</tp:taxon-name>)((\n\\s*?<[^>]*?>.*?</[^>]*?>)*?)\n\\s*?(<object-id [^<>]*>.*</object-id>)", "$1    $5$1$2$3");
            }

            // Remove successive titles
            this.xml = Regex.Replace(this.xml, @"</title>(\s*)<title>", "<break/>$1");

            // Format Graphics
            this.xml = Regex.Replace(this.xml, @"export\.php_files/", string.Empty);

            // Format Tables
            this.xml = Regex.Replace(
                this.xml,
                "<table-wrap position=\"float\" orientation=\"portrait\">(\\s*?(<label>.*?</label>|<caption>.*?</caption>|<label>.*?</label>\\s*?<caption>.*?</caption>))(\\s*?)<table id=\"T(\\d+)\">",
                "<table-wrap id=\"T$4\" position=\"float\" orientation=\"portrait\">$1$3<table>");
            this.xml = Regex.Replace(this.xml, "\\s*&lt;br/&gt;\\s*(</p>\\s*</caption>)", "$1");

            // Final Format
            this.xml = Regex.Replace(this.xml, @"(</name>)\s*,\s*(<name)", "$1$2");
            this.xml = Regex.Replace(this.xml, "person-group1", "person-group");
            this.xml = Regex.Replace(this.xml, "sec-type=\"Methods?\"", "sec-type=\"methods\"");
            this.xml = Regex.Replace(this.xml, "sec-type=\"[mM]aterials? and [mM]ethods?\"", "sec-type=\"materials|methods\"");
            this.xml = Regex.Replace(this.xml, "sec-type=\"[mM]ethods? and [mM]aterials?\"", "sec-type=\"materials|methods\"");
            this.xml = Regex.Replace(this.xml, " full-name=\".*?\"", string.Empty);
            this.xml = Regex.Replace(this.xml, " class=\".*?\"", string.Empty);

            this.xml = Regex.Replace(this.xml, "<locality-coordinates [^>]*>(.*?)</locality-coordinates>", "<named-content content-type=\"dwc:verbatimCoordinates\">$1</named-content>");

            // Remove blanks and empty lines
            this.xml = Regex.Replace(this.xml, @"(\s*?\n)+?", "\n");
        }
    }
}
