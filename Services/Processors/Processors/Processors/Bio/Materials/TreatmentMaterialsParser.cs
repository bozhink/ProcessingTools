namespace ProcessingTools.Processors.Bio.Materials
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts.Bio.Materials;
    using Contracts.Transformers;

    using ProcessingTools.Bio.ServiceClient.MaterialsParser.Contracts;
    using ProcessingTools.Contracts;

    public class TreatmentMaterialsParser : ITreatmentMaterialsParser
    {
        private readonly IMaterialCitationsParser materialCitationsParser;
        private readonly ITaxonTreatmentExtractMaterialsTransformer taxonTreatmentExtractMaterialsTransformer;
        private readonly IFormatTaxonTreatmentsTransformer formatTaxonTreatmentsTransformer;

        public TreatmentMaterialsParser(
            IMaterialCitationsParser materialCitationsParser,
            ITaxonTreatmentExtractMaterialsTransformer taxonTreatmentExtractMaterialsTransformer,
            IFormatTaxonTreatmentsTransformer formatTaxonTreatmentsTransformer)
        {
            if (materialCitationsParser == null)
            {
                throw new ArgumentNullException(nameof(materialCitationsParser));
            }

            if (taxonTreatmentExtractMaterialsTransformer == null)
            {
                throw new ArgumentNullException(nameof(taxonTreatmentExtractMaterialsTransformer));
            }

            if (formatTaxonTreatmentsTransformer == null)
            {
                throw new ArgumentNullException(nameof(formatTaxonTreatmentsTransformer));
            }

            this.materialCitationsParser = materialCitationsParser;
            this.taxonTreatmentExtractMaterialsTransformer = taxonTreatmentExtractMaterialsTransformer;
            this.formatTaxonTreatmentsTransformer = formatTaxonTreatmentsTransformer;
        }

        public async Task<object> Parse(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            await this.FormatTaxonTreatments(document.XmlDocument);

            var queryDocument = await this.GenerateQueryDocument(document.XmlDocument);

            string response = await this.materialCitationsParser.Invoke(queryDocument.OuterXml);

            var responseDocument = this.GenerateResponseDocument(response);
            responseDocument.SelectNodes("//p[@id]")
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(p =>
                {
                    string id = p.Attributes["id"].InnerText;
                    var paragraph = document.SelectSingleNode($"//p[@id='{id}']");
                    if (paragraph != null)
                    {
                        paragraph.InnerXml = p.InnerXml;
                    }
                });

            return true;
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

            var text = await this.taxonTreatmentExtractMaterialsTransformer.Transform(document);
            queryDocument.LoadXml(text);
            return queryDocument;
        }

        private async Task FormatTaxonTreatments(XmlDocument document)
        {
            var text = await this.formatTaxonTreatmentsTransformer.Transform(document);
            document.LoadXml(text);
        }
    }
}
