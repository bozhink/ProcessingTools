namespace ProcessingTools.Bio.Processors.Materials
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts.Materials;

    using ProcessingTools.Bio.ServiceClient.MaterialsParser.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Contracts.Transformers;

    public class TreatmentMaterialsParser : ITreatmentMaterialsParser
    {
        private readonly IMaterialCitationsParser materialCitationsParser;
        private readonly IXslTransformer<ITaxonTreatmentExtractMaterialsXslTransformProvider> taxonTreatmentExtractMaterialsXslTransformer;
        private readonly IXslTransformer<IFormatTaxonTreatmentsXslTransformProvider> formatTaxonTreatmentsXslTransformer;

        public TreatmentMaterialsParser(
            IMaterialCitationsParser materialCitationsParser,
            IXslTransformer<ITaxonTreatmentExtractMaterialsXslTransformProvider> taxonTreatmentExtractMaterialsXslTransformer,
            IXslTransformer<IFormatTaxonTreatmentsXslTransformProvider> formatTaxonTreatmentsXslTransformer)
        {
            if (materialCitationsParser == null)
            {
                throw new ArgumentNullException(nameof(materialCitationsParser));
            }

            if (taxonTreatmentExtractMaterialsXslTransformer == null)
            {
                throw new ArgumentNullException(nameof(taxonTreatmentExtractMaterialsXslTransformer));
            }

            if (formatTaxonTreatmentsXslTransformer == null)
            {
                throw new ArgumentNullException(nameof(formatTaxonTreatmentsXslTransformer));
            }

            this.materialCitationsParser = materialCitationsParser;
            this.taxonTreatmentExtractMaterialsXslTransformer = taxonTreatmentExtractMaterialsXslTransformer;
            this.formatTaxonTreatmentsXslTransformer = formatTaxonTreatmentsXslTransformer;
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

            var text = await this.taxonTreatmentExtractMaterialsXslTransformer.Transform(document);
            queryDocument.LoadXml(text);
            return queryDocument;
        }

        private async Task FormatTaxonTreatments(XmlDocument document)
        {
            var text = await this.formatTaxonTreatmentsXslTransformer.Transform(document);
            document.LoadXml(text);
        }
    }
}
