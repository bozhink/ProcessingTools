namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Configurator;
    using Contracts;

    public class TreatmentFormatter : TaggerBase, IBaseFormatter
    {
        private ILogger logger;

        public TreatmentFormatter(string xml, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
        }

        public TreatmentFormatter(Config config, string xml, ILogger logger)
            : base(config, xml)
        {
            this.logger = logger;
        }

        public TreatmentFormatter(IBase baseObject, ILogger logger)
            : base(baseObject)
        {
            this.logger = logger;
        }

        public void Format()
        {
            try
            {
                foreach (XmlNode nomenclature in this.XmlDocument.SelectNodes("//tp:nomenclature", this.NamespaceManager))
                {
                    this.FormatNomenclatureTitle(nomenclature);

                    nomenclature.InnerXml = this.FormatNomencatureContent(nomenclature.InnerXml);

                    this.FormatObjectIdInNomenclature(nomenclature);

                    nomenclature.InnerXml = Regex.Replace(nomenclature.InnerXml, @"\n\s*\n", "\n");
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }
        }

        private string FormatNomencatureContent(string nomenclatureContent)
        {
            string result = nomenclatureContent;

            /*
             * Extract label preceding lower taxa and Authority and Status tags
             */
            result = Regex.Replace(
                result,
                @"(\s*)<title[^>]*>([^<>]+?)\s*(<tn [^>]*>.*?</tn>)\s*([^<>]*)</title>",
                "$1<label>$2</label>$1$3$1<tp:taxon-authority>$4</tp:taxon-authority>");
            /*
             * Extract Authority and Status tags if there is no label
             */
            result = Regex.Replace(
                result,
                @"(\s*)<title[^>]*>(<tn [^>]*>.*?</tn>)\s*([^<>]*)</title>",
                "$1$2$1<tp:taxon-authority>$3</tp:taxon-authority>");

            result = Regex.Replace(result, @"\s*<tp:taxon-authority>\s*</tp:taxon-authority>", string.Empty);

            /*
             * Format nomenclature
             */
            result = Regex.Replace(
                result,
                @"(?<=</label>)(\s*)(<tn [^>]*>)(<tn-part[\s\S]*?)(</tn>)",
                "$1$2$1    $3$1$4");
            result = Regex.Replace(
                result,
                @"^(\s*)(<tn [^>]*>)(<tn-part[\s\S]*?)(</tn>)",
                "$1$2$1    $3$1$4");
            for (int i = 0; i < 8; i++)
            {
                result = Regex.Replace(
                    result,
                    @"(\n\s*)(\(?<tn-part [^>]*>.*?</tn-part>\)?) (\(?<tn-part [^>]*>.*?</tn-part>\)?)",
                    "$1$2$1$3");
            }

            /*
             * Split authority and status
             */
            result = Regex.Replace(
                result,
                @"<tp:taxon-authority>((([a-z]+\.(\s*)(n|nov))|(n\.\s*[a-z]+)|(([a-z]+\.)?(\s*)spp))(\.)?|new record)</tp:taxon-authority>",
                "<tp:taxon-status>$1</tp:taxon-status>");

            result = Regex.Replace(
                result,
                @"(\s*)<tp:taxon-authority>([\w\-\,\;\.\(\)\&\s-]+)(\s*\W\s*)([Ii]ncertae\s+[Ss]edis|nom\.?\s+cons\.?|[a-z]+\.\s*(n|nov|r|rev)(\.)?|new record)</tp:taxon-authority>",
                "$1<tp:taxon-authority>$2</tp:taxon-authority>$1<tp:taxon-status>$4</tp:taxon-status>");

            result = Regex.Replace(
                result,
                @"(<tp:taxon-authority>.*)((?<!&[a-z]+)[\s,;:]+)(</tp:taxon-authority>)",
                "$1$3");

            result = Regex.Replace(
                result,
                @"(<tp:taxon-authority>.*?</tp:taxon-authority>\S*)\s+?(\n?)",
                "$1\n");

            result = Regex.Replace(
                result,
                @"(?<=<tp:taxon-authority>)\s+|\s+(?=</tp:taxon-authority>)",
                string.Empty);

            return result;
        }

        private void FormatNomenclatureTitle(XmlNode nomenclature)
        {
            if (nomenclature["title"] != null)
            {
                string title = nomenclature["title"].InnerXml;
                title = Regex.Replace(title, "</?i>", string.Empty);
                title = Regex.Replace(title, @"\s+", " ");
                title = Regex.Replace(title, @"\A\s+|\s+\Z", string.Empty);
                nomenclature["title"].InnerXml = title;
            }
        }

        private void FormatObjectIdInNomenclature(XmlNode nomenclature)
        {
            if (nomenclature["object-id"] != null && nomenclature["tn"] != null)
            {
                foreach (XmlNode objectId in nomenclature.SelectNodes("./object-id", this.NamespaceManager))
                {
                    nomenclature["tn"].AppendChild(objectId);
                }
            }
        }
    }
}