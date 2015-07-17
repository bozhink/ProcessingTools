using System.Text.RegularExpressions;

namespace Base
{
    public class QuentinFlora : Base
    {
        public QuentinFlora()
        {
            this.xml = string.Empty;
        }

        public QuentinFlora(string xml)
        {
            this.xml = Regex.Replace(xml, "\r", string.Empty);
        }

        public void InitialFormat()
        {
            this.xml = Regex.Replace(this.xml, "(<notes>|<latinname>)\\s+", "$1");
            this.xml = Regex.Replace(this.xml, "\\s+(</notes>|</latinname>)", "$1");

            // Paste paragraphs in notes-tags
            for (Match m = Regex.Match(this.xml, "<notes>[\\s\\S]*?</notes>"); m.Success; m = m.NextMatch())
            {
                string replace = Regex.Replace(m.Value, "(?<=<notes>)([\\s\\S]*?)(?=</notes>)", "<p>$1</p>");
                replace = Regex.Replace(replace, "\n", "</p><p>");
                this.xml = Regex.Replace(this.xml, Regex.Escape(m.Value), replace);
            }

            this.xml = Regex.Replace(this.xml, "(<p>)\\s+", "$1");
            this.xml = Regex.Replace(this.xml, "\\s+(</p>)", "$1");
        }

        public void Split1()
        {
            for (Match m = Regex.Match(this.xml, "<sec type=\"family\">[\\s\\S]*?</sec>"); m.Success; m = m.NextMatch())
            {
                string replace = m.Value;
                replace = Regex.Replace(replace, "(<genus_header>[\\s\\S]*?)(?=<genus_header>)", "<sec type=\"genus\">\n$1\n</sec>\n");
                replace = Regex.Replace(replace, "(</sec>\\s*|</family_header>\\s*)(<genus_header>[\\s\\S]*?)(?=</sec>)", "$1<sec type=\"genus\">\n$2\n</sec>\n");

                this.xml = Regex.Replace(this.xml, Regex.Escape(m.Value), replace);
            }
        }

        public void Split2()
        {
            this.xml = Regex.Replace(this.xml, "(<family_header>[\\s\\S]*?)(?=<family_header>)", "<sec type=\"family\">\n$1\n</sec>\n");
            this.xml = Regex.Replace(this.xml, "(</sec>\\s*)(<family_header>[\\s\\S]*?)(?=</sec>)", "$1<sec type=\"family\">\n$2\n</sec>\n");
        }

        public void FinalFormat()
        {
            this.xml = Regex.Replace(this.xml, "(<p>|<title>)\\s+", "$1");
            this.xml = Regex.Replace(this.xml, "\\s+(</p>|</title>)", "$1");

            for (Match m = Regex.Match(this.xml, "(<p>|<title>)[\\s\\S]*?(</p>|</title>)"); m.Success; m = m.NextMatch())
            {
                string replace = Regex.Replace(m.Value, "\\s+", " ");
                this.xml = Regex.Replace(this.xml, Regex.Escape(m.Value), replace);
            }

            this.xml = Regex.Replace(this.xml, "\\s*(</?content>|</?ballon_content>)\\s*", "$1");
            this.xml = Regex.Replace(this.xml, "(</div>)\\s+(<div)", "$1$2");
            this.xml = Regex.Replace(this.xml, "(<tp:taxon-name>)\\s+", "$1");
            this.xml = Regex.Replace(this.xml, "\\s+(</tp:taxon-name>)", "$1");
            this.xml = Regex.Replace(this.xml, "\\s+(<br/>)\\s+", "$1");
            this.xml = Regex.Replace(this.xml, "(</span>)\\s*<br/>\\s*(</ballon_content>)", "$1$2");

            ////this.xml = Regex.Replace(this.xml, "(</ballon_wrapper>)\\s*(<ballon_wrapper)", "$1<span class=\"space\"/> $2");
        }
    }
}
