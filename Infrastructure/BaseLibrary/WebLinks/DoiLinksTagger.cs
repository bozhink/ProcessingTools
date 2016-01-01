namespace ProcessingTools.BaseLibrary.HyperLinks
{
    using Attributes;
    using Configurator;
    using Contracts;
    using Extensions;
    using Nlm.Publishing.Types;

    public class DoiLinksTagger : Base, IBaseTagger
    {
        private const string ReplacementTagTemplate = @"<ext-link ext-link-type=""{0}"">$1</ext-link>";

        public DoiLinksTagger(Config config, string xml)
            : base(config, xml)
        {
        }

        public DoiLinksTagger(IBase baseObject)
            : base(baseObject)
        {
        }

        public void Tag()
        {
            this.TagDoi();
            this.TagPmcLinks();
        }

        private void TagDoi()
        {
            string xml = this.Xml;

            xml = xml
                .RegexReplace(
                    @"(?i)(?<!<ext-link [^>]*>)(?<=\bdoi:?\s*)(\d+\.[^,<>\s]+[A-Za-z0-9]((?<=&[A-Za-z0-9#]+);)?)",
                    string.Format(ReplacementTagTemplate, ExternalLinkType.Doi.GetValue()));

            this.Xml = xml;
        }

        private void TagPmcLinks()
        {
            string xml = this.Xml;

            xml = xml
                .RegexReplace(
                    @"(?i)(?<=\bpmid\W?)(\d+)",
                    string.Format(ReplacementTagTemplate, ExternalLinkType.Pmid.GetValue()))
                .RegexReplace(
                    @"(?i)(\bpmc\W?\d+)",
                    string.Format(ReplacementTagTemplate, ExternalLinkType.Pmcid.GetValue()))
                .RegexReplace(
                    @"(?i)(?<=\bpmcid\W?)(\d+)",
                    string.Format(ReplacementTagTemplate, ExternalLinkType.Pmcid.GetValue()));

            this.Xml = xml;
        }
    }
}