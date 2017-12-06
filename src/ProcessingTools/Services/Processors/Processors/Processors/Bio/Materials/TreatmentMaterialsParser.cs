namespace ProcessingTools.Processors.Processors.Bio.Materials
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Clients.Bio;
    using ProcessingTools.Contracts.Processors.Factories.Bio;
    using ProcessingTools.Contracts.Processors.Processors.Bio.Materials;

    public class TreatmentMaterialsParser : ITreatmentMaterialsParser
    {
        private readonly IMaterialCitationsParser materialCitationsParser;
        private readonly ITaxonTreatmentsTransformersFactory transformersFactory;

        public TreatmentMaterialsParser(
            IMaterialCitationsParser materialCitationsParser,
            ITaxonTreatmentsTransformersFactory transformersFactory)
        {
            this.materialCitationsParser = materialCitationsParser ?? throw new ArgumentNullException(nameof(materialCitationsParser));
            this.transformersFactory = transformersFactory ?? throw new ArgumentNullException(nameof(transformersFactory));
        }

        public async Task<object> ParseAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            await this.FormatTaxonTreatments(context.XmlDocument).ConfigureAwait(false);

            var queryDocument = await this.GenerateQueryDocument(context.XmlDocument).ConfigureAwait(false);

            string response = await this.materialCitationsParser.ParseAsync(queryDocument.OuterXml).ConfigureAwait(false);

            var responseDocument = this.GenerateResponseDocument(response);
            responseDocument.SelectNodes("//p[@id]")
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(p =>
                {
                    string id = p.Attributes["id"].InnerText;
                    var paragraph = context.SelectSingleNode($"//p[@id='{id}']");
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

        private async Task<XmlDocument> GenerateQueryDocument(XmlNode context)
        {
            XmlDocument queryDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };

            var text = await this.transformersFactory
                .GetTaxonTreatmentExtractMaterialsTransformer()
                .TransformAsync(context)
                .ConfigureAwait(false);

            queryDocument.LoadXml(text);
            return queryDocument;
        }

        private async Task FormatTaxonTreatments(XmlDocument document)
        {
            var text = await this.transformersFactory
                .GetTaxonTreatmentFormatTransformer()
                .TransformAsync(document)
                .ConfigureAwait(false);

            document.LoadXml(text);
        }
    }
}
