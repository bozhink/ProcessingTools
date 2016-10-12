namespace ProcessingTools.BaseLibrary.Special
{
    using System;
    using System.Linq;
    using System.Xml;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;

    public class QuentinFlora
    {
        public void InitialFormat(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            document.SelectNodes("//latinname").AsParallel().ForAll(this.ClearWhitespacingAction);
            document.SelectNodes("//notes").AsParallel().ForAll(this.ClearWhitespacingAction);
            document.SelectNodes("//notes").AsParallel().ForAll(n =>
            {
                n.InnerXml = $"<p>{n.InnerXml}</p>";
            });
            document.SelectNodes("//p").AsParallel().ForAll(this.ClearWhitespacingAction);
        }

        public void Split1(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            document.SelectNodes("//sec[@type='family']")
                .AsParallel()
                .ForAll(n =>
                {
                    n.InnerXml = n.InnerXml
                        .RegexReplace("(<genus_header>[\\s\\S]*?)(?=<genus_header>)", "<sec type=\"genus\">\n$1\n</sec>\n")
                        .RegexReplace("(</sec>\\s*|</family_header>\\s*)(<genus_header>[\\s\\S]*?)(?=</sec>)", "$1<sec type=\"genus\">\n$2\n</sec>\n");
                });
        }

        public void Split2(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            document.Xml = document.Xml
                .RegexReplace(@"(<family_header>[\s\S]*?)(?=<family_header>)", "<sec type=\"family\">\n$1\n</sec>\n")
                .RegexReplace(@"(</sec>\s*)(<family_header>[\s\S]*?)(?=</sec>)", "$1<sec type=\"family\">\n$2\n</sec>\n");
        }

        public void FinalFormat(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            document.SelectNodes("//tp:taxon-name").AsParallel().ForAll(this.ClearWhitespacingAction);
            document.SelectNodes("//p").AsParallel().ForAll(this.ClearWhitespacingAction);
            document.SelectNodes("//title").AsParallel().ForAll(this.ClearWhitespacingAction);

            string xml = document.Xml
                .RegexReplace("\\s*(</?content>|</?ballon_content>)\\s*", "$1")
                .RegexReplace("(</div>)\\s+(<div)", "$1$2")
                .RegexReplace("\\s+(<br/>)\\s+", "$1")
                .RegexReplace("(</span>)\\s*<br/>\\s*(</ballon_content>)", "$1$2");

            ////xml = Regex.Replace(xml, "(</ballon_wrapper>)\\s*(<ballon_wrapper)", "$1<span class=\"space\"/> $2");

            document.Xml = xml;
        }

        private Action<XmlNode> ClearWhitespacingAction => n =>
        {
            n.InnerXml = n.InnerXml
                .RegexReplace(@"\s+", " ")
                .Trim();
        };
    }
}
