namespace ProcessingTools.Base.Taxonomy
{
    using System.Text.RegularExpressions;
    using System.Threading;

    public abstract class HigherTaxaParser : Base, IParser
    {
        private static bool delay = false;

        public HigherTaxaParser(string xml)
            : base(xml)
        {
        }

        public HigherTaxaParser(Config config, string xml)
            : base(config, xml)
        {
        }

        public HigherTaxaParser(IBase baseObject)
            : base(baseObject)
        {
        }

        public abstract void Parse();

        protected void Delay()
        {
            if (delay)
            {
                Thread.Sleep(15000);
            }
            else
            {
                delay = true;
            }
        }

        protected void ReplaceTaxonNameByItsParsedContent(string scientificName, string replacement)
        {
            this.XmlDocument.InnerXml = Regex.Replace(this.XmlDocument.InnerXml, "(?<=<tn [^>]*>)(" + Regex.Escape(scientificName) + ")(?=</tn>)", replacement);
        }
    }
}
