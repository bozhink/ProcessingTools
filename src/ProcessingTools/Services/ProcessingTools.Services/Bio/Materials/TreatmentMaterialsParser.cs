// <copyright file="TreatmentMaterialsParser.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Materials
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Bio;
    using ProcessingTools.Contracts.Services.Bio.Materials;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// Treatment materials parser.
    /// </summary>
    public class TreatmentMaterialsParser : ITreatmentMaterialsParser
    {
        private readonly IMaterialCitationsParser materialCitationsParser;
        private readonly ITaxonTreatmentsTransformerFactory transformerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreatmentMaterialsParser"/> class.
        /// </summary>
        /// <param name="materialCitationsParser">Instance of <see cref="IMaterialCitationsParser"/>.</param>
        /// <param name="transformerFactory">Instance <see cref="ITaxonTreatmentsTransformerFactory"/>.</param>
        public TreatmentMaterialsParser(IMaterialCitationsParser materialCitationsParser, ITaxonTreatmentsTransformerFactory transformerFactory)
        {
            this.materialCitationsParser = materialCitationsParser ?? throw new ArgumentNullException(nameof(materialCitationsParser));
            this.transformerFactory = transformerFactory ?? throw new ArgumentNullException(nameof(transformerFactory));
        }

        /// <inheritdoc/>
        public Task<object> ParseAsync(IDocument context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return this.ParseInternalAsync(context);
        }

        private async Task<object> ParseInternalAsync(IDocument context)
        {
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
                PreserveWhitespace = true,
            };

            responseDocument.LoadXml(response);
            return responseDocument;
        }

        private async Task<XmlDocument> GenerateQueryDocument(XmlNode context)
        {
            XmlDocument queryDocument = new XmlDocument
            {
                PreserveWhitespace = true,
            };

            var text = await this.transformerFactory
                .GetTaxonTreatmentExtractMaterialsTransformer()
                .TransformAsync(context)
                .ConfigureAwait(false);

            queryDocument.LoadXml(text);
            return queryDocument;
        }

        private async Task FormatTaxonTreatments(XmlDocument document)
        {
            var text = await this.transformerFactory
                .GetTaxonTreatmentFormatTransformer()
                .TransformAsync(document)
                .ConfigureAwait(false);

            document.LoadXml(text);
        }
    }
}
