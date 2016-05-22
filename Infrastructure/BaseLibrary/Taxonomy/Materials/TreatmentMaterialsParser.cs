namespace ProcessingTools.BaseLibrary.Taxonomy.Materials
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Bio.ServiceClient.MaterialsParser.Contracts;
    using ProcessingTools.Xml.Extensions;

    public class TreatmentMaterialsParser : ITreatmentMaterialsParser
    {
        private const string FormatTaxonTreatmentsXslPathKey = "FormatTaxonTreatmentsXslPath";
        private const string TaxonTreatmentExtractMaterialsXslPathKey = "TaxonTreatmentExtractMaterialsXslPath";

        private readonly IMaterialCitationsParser materialCitationsParser;

        private readonly string formatTaxonTreatmentsXslFileName;
        private readonly string taxonTreatmentExtractMaterialsXslFileName;

        public TreatmentMaterialsParser(IMaterialCitationsParser materialCitationsParser)
        {
            if (materialCitationsParser == null)
            {
                throw new ArgumentNullException(nameof(materialCitationsParser));
            }

            this.materialCitationsParser = materialCitationsParser;

            this.formatTaxonTreatmentsXslFileName = ConfigurationManager.AppSettings[FormatTaxonTreatmentsXslPathKey];
            this.taxonTreatmentExtractMaterialsXslFileName = ConfigurationManager.AppSettings[TaxonTreatmentExtractMaterialsXslPathKey];
        }

        public async Task Parse(XmlDocument document, XmlNamespaceManager namespaceManager)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (namespaceManager == null)
            {
                throw new ArgumentNullException(nameof(namespaceManager));
            }

            document.LoadXml(document.ApplyXslTransform(this.formatTaxonTreatmentsXslFileName));

            XmlDocument queryDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };

            queryDocument.LoadXml(document.ApplyXslTransform(this.taxonTreatmentExtractMaterialsXslFileName));
            queryDocument.Save(@"C:\temp\query-xx.xml");

            string response = await this.materialCitationsParser.Invoke(queryDocument.OuterXml);

            XmlDocument responseDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };

            responseDocument.LoadXml(response);
            responseDocument.Save(@"C:\temp\response-xx.xml");

            responseDocument.SelectNodes("//p[@id]", namespaceManager)
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(p =>
                {
                    string id = p.Attributes["id"].InnerText;
                    var paragraph = document.SelectSingleNode($"//p[@id='{id}']", namespaceManager);
                    if (paragraph != null)
                    {
                        paragraph.InnerXml = p.InnerXml;
                    }
                });
        }
    }
}