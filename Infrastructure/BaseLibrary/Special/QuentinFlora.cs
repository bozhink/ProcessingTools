namespace ProcessingTools.BaseLibrary
{
    using System.Text.RegularExpressions;

    using Contracts;

    public class QuentinFlora : TaggerBase
    {
        public QuentinFlora(string xml)
            : base(xml)
        {
        }

        public QuentinFlora(IBase baseObject)
            : base(baseObject)
        {
        }

        public void InitialFormat()
        {
            string xml = this.Xml;

            xml = Regex.Replace(xml, "(<notes>|<latinname>)\\s+", "$1");
            xml = Regex.Replace(xml, "\\s+(</notes>|</latinname>)", "$1");

            // Paste paragraphs in notes-tags
            for (Match m = Regex.Match(xml, "<notes>[\\s\\S]*?</notes>"); m.Success; m = m.NextMatch())
            {
                string replace = Regex.Replace(m.Value, "(?<=<notes>)([\\s\\S]*?)(?=</notes>)", "<p>$1</p>");
                replace = Regex.Replace(replace, "\n", "</p><p>");
                xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
            }

            xml = Regex.Replace(xml, "(<p>)\\s+", "$1");
            xml = Regex.Replace(xml, "\\s+(</p>)", "$1");

            this.Xml = xml;
        }

        public void Split1()
        {
            string xml = this.Xml;

            for (Match m = Regex.Match(xml, "<sec type=\"family\">[\\s\\S]*?</sec>"); m.Success; m = m.NextMatch())
            {
                string replace = m.Value;
                replace = Regex.Replace(replace, "(<genus_header>[\\s\\S]*?)(?=<genus_header>)", "<sec type=\"genus\">\n$1\n</sec>\n");
                replace = Regex.Replace(replace, "(</sec>\\s*|</family_header>\\s*)(<genus_header>[\\s\\S]*?)(?=</sec>)", "$1<sec type=\"genus\">\n$2\n</sec>\n");

                xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
            }

            this.Xml = xml;
        }

        public void Split2()
        {
            this.Xml = Regex.Replace(this.Xml, "(<family_header>[\\s\\S]*?)(?=<family_header>)", "<sec type=\"family\">\n$1\n</sec>\n");
            this.Xml = Regex.Replace(this.Xml, "(</sec>\\s*)(<family_header>[\\s\\S]*?)(?=</sec>)", "$1<sec type=\"family\">\n$2\n</sec>\n");
        }

        public void FinalFormat()
        {
            string xml = this.Xml;

            xml = Regex.Replace(xml, "(<p>|<title>)\\s+", "$1");
            xml = Regex.Replace(xml, "\\s+(</p>|</title>)", "$1");

            for (Match m = Regex.Match(xml, "(<p>|<title>)[\\s\\S]*?(</p>|</title>)"); m.Success; m = m.NextMatch())
            {
                string replace = Regex.Replace(m.Value, "\\s+", " ");
                xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
            }

            xml = Regex.Replace(xml, "\\s*(</?content>|</?ballon_content>)\\s*", "$1");
            xml = Regex.Replace(xml, "(</div>)\\s+(<div)", "$1$2");
            xml = Regex.Replace(xml, "(<tp:taxon-name>)\\s+", "$1");
            xml = Regex.Replace(xml, "\\s+(</tp:taxon-name>)", "$1");
            xml = Regex.Replace(xml, "\\s+(<br/>)\\s+", "$1");
            xml = Regex.Replace(xml, "(</span>)\\s*<br/>\\s*(</ballon_content>)", "$1$2");

            ////xml = Regex.Replace(xml, "(</ballon_wrapper>)\\s*(<ballon_wrapper)", "$1<span class=\"space\"/> $2");

            this.Xml = xml;
        }
    }
}
