namespace ProcessingTools.BaseLibrary.Taxonomy.Materials
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.BaseLibrary.Contracts;
    using ProcessingTools.Bio.ServiceClient.MaterialsParser.Contracts;
    using ProcessingTools.Xml.Contracts;

    public class TreatmentMaterialsParser : ITreatmentMaterialsParser
    {
        private readonly IMaterialCitationsParser materialCitationsParser;

        private readonly IXslTransformer transformer;
        private readonly ITaxonTreatmentExtractMaterialsXslTransformProvider taxonTreatmentExtractMaterialsXslTransformProvider;
        private readonly IFormatTaxonTreatmentsXslTransformProvider formatTaxonTreatmentsXslTransformProvider;

        public TreatmentMaterialsParser(
            IMaterialCitationsParser materialCitationsParser,
            IXslTransformer transformer,
            ITaxonTreatmentExtractMaterialsXslTransformProvider taxonTreatmentExtractMaterialsXslTransformProvider,
            IFormatTaxonTreatmentsXslTransformProvider formatTaxonTreatmentsXslTransformProvider)
        {
            if (materialCitationsParser == null)
            {
                throw new ArgumentNullException(nameof(materialCitationsParser));
            }

            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            if (taxonTreatmentExtractMaterialsXslTransformProvider == null)
            {
                throw new ArgumentNullException(nameof(taxonTreatmentExtractMaterialsXslTransformProvider));
            }

            if (formatTaxonTreatmentsXslTransformProvider == null)
            {
                throw new ArgumentNullException(nameof(formatTaxonTreatmentsXslTransformProvider));
            }

            this.materialCitationsParser = materialCitationsParser;
            this.transformer = transformer;
            this.taxonTreatmentExtractMaterialsXslTransformProvider = taxonTreatmentExtractMaterialsXslTransformProvider;
            this.formatTaxonTreatmentsXslTransformProvider = formatTaxonTreatmentsXslTransformProvider;
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

            await this.FormatTaxonTreatments(document);

            var queryDocument = await this.GenerateQueryDocument(document);

            string response = await this.materialCitationsParser.Invoke(queryDocument.OuterXml);

            var responseDocument = this.GenerateResponseDocument(response);
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

        private XmlDocument GenerateResponseDocument(string response)
        {
            XmlDocument responseDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };

            responseDocument.LoadXml(response);
            return responseDocument;
        }

        private async Task<XmlDocument> GenerateQueryDocument(XmlDocument document)
        {
            XmlDocument queryDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };

            var text = await this.transformer.Transform(document, this.taxonTreatmentExtractMaterialsXslTransformProvider);
            queryDocument.LoadXml(text);
            return queryDocument;
        }

        private async Task FormatTaxonTreatments(XmlDocument document)
        {
            var text = await this.transformer.Transform(document, this.formatTaxonTreatmentsXslTransformProvider);
            document.LoadXml(text);
        }
    }
}
