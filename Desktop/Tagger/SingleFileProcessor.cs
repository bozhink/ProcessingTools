namespace ProcessingTools.Tagger
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.DocumentProvider;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;

    public partial class SingleFileProcessor : ISingleFileProcessor
    {
        private readonly IDocumentNormalizer documentNormalizer;
        private readonly ILogger logger;

        private ConcurrentQueue<Task> tasks;
        private XmlFileProcessor fileProcessor;
        private TaxPubDocument document;
        private ProgramSettings settings;

        public SingleFileProcessor(IDocumentNormalizer documentNormalizer, ILogger logger)
        {
            if (documentNormalizer == null)
            {
                throw new ArgumentNullException(nameof(documentNormalizer));
            }

            this.documentNormalizer = documentNormalizer;
            this.logger = logger;

            this.tasks = new ConcurrentQueue<Task>();
            this.document = new TaxPubDocument();
        }

        public async Task Run(ProgramSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            this.settings = settings;

            try
            {
                this.ConfigureFileProcessor();

                this.SetUpConfigParameters();

                await this.ReadDocument();

                await this.ProcessDocument();

                this.tasks.Enqueue(this.WriteOutputFile());

                Task.WaitAll(this.tasks.ToArray());
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
                throw;
            }
        }

        private async Task ProcessDocument()
        {
            if (this.settings.ZoobankCloneXml)
            {
                await this.InvokeProcessor<IZooBankCloneXmlController>();
                return;
            }

            if (this.settings.ZoobankCloneJson)
            {
                await this.InvokeProcessor<IZooBankCloneJsonController>();
                return;
            }

            if (this.settings.ZoobankGenerateRegistrationXml)
            {
                await this.InvokeProcessor<IZooBankGenerateRegistrationXmlController>();
                return;
            }

            if (this.settings.QueryReplace)
            {
                await this.InvokeProcessor<IQueryReplaceController>();
                return;
            }

            if (this.settings.RunXslTransform)
            {
                await this.InvokeProcessor<IRunCustomXslTransformController>();
            }

            if (this.settings.InitialFormat)
            {
                await this.InvokeProcessor<IInitialFormatController>();
            }

            if (this.settings.ParseReferences)
            {
                await this.InvokeProcessor<IParseReferencesController>();
            }

            if (this.settings.TagDoi || this.settings.TagWebLinks)
            {
                await this.InvokeProcessor<ITagWebLinksController>();
            }

            if (this.settings.ResolveMediaTypes)
            {
                await this.InvokeProcessor<IResolveMediaTypesController>();
            }

            if (this.settings.TagTableFn)
            {
                await this.InvokeProcessor<ITagTableFootnoteController>();
            }

            if (this.settings.TagCoordinates)
            {
                await this.InvokeProcessor<ITagCoordinatesController>();
            }

            if (this.settings.ParseCoordinates)
            {
                await this.InvokeProcessor<IParseCoordinatesController>();
            }

            foreach (var controllerType in this.settings.CalledControllers)
            {
                await this.InvokeProcessor(controllerType);
            }

            if (this.settings.TagEnvironmentTermsWithExtract)
            {
                await this.InvokeProcessor<ITagEnvironmentTermsWithExtractController>();
            }

            // Tag envo terms using environment database
            if (this.settings.TagEnvironmentTerms)
            {
                await this.InvokeProcessor<ITagEnvironmentTermsController>();
            }

            if (this.settings.TagAbbreviations)
            {
                await this.InvokeProcessor<ITagAbbreviationsController>();
            }

            // Tag institutions, institutional codes, and specimen codes
            if (this.settings.TagCodes)
            {
                await this.InvokeProcessor<ITagCodesController>();
            }

            if (this.settings.TagLowerTaxa)
            {
                await this.InvokeProcessor<ITagLowerTaxaController>();
            }

            if (this.settings.TagHigherTaxa)
            {
                await this.InvokeProcessor<ITagHigherTaxaController>();
            }

            if (this.settings.ParseLowerTaxa)
            {
                await this.InvokeProcessor<IParseLowerTaxaController>();
            }

            if (this.settings.ParseHigherTaxa)
            {
                await this.InvokeProcessor<IParseHigherTaxaWithLocalDbController>();
            }

            if (this.settings.ParseHigherWithAphia)
            {
                await this.InvokeProcessor<IParseHigherTaxaWithAphiaController>();
            }

            if (this.settings.ParseHigherWithCoL)
            {
                await this.InvokeProcessor<IParseHigherTaxaWithCatalogueOfLifeController>();
            }

            if (this.settings.ParseHigherWithGbif)
            {
                await this.InvokeProcessor<IParseHigherTaxaWithGbifController>();
            }

            if (this.settings.ParseHigherBySuffix)
            {
                await this.InvokeProcessor<IParseHigherTaxaBySuffixController>();
            }

            if (this.settings.ParseHigherAboveGenus)
            {
                await this.InvokeProcessor<IParseHigherTaxaAboveGenusController>();
            }

            // Main Tagging part of the program
            if (this.settings.ParseBySection)
            {
                foreach (XmlNode context in this.document.XmlDocument.SelectNodes(this.settings.HigherStructrureXpath, this.document.NamespaceManager))
                {
                    await this.ContextProcessing(context);
                }
            }
            else
            {
                await this.ContextProcessing(this.document.XmlDocument.DocumentElement);
            }

            if (this.settings.ExtractTaxa || this.settings.ExtractLowerTaxa || this.settings.ExtractHigherTaxa)
            {
                await this.InvokeProcessor<IExtractTaxaController>();
            }

            if (this.settings.ValidateTaxa)
            {
                this.tasks.Enqueue(this.InvokeProcessor<IValidateTaxaController>());
            }

            if (this.settings.UntagSplit)
            {
                await this.InvokeProcessor<IRemoveAllTaxonNamePartTagsController>();
            }

            if (this.settings.FormatTreat)
            {
                await this.InvokeProcessor<IFormatTreatmentsController>();
            }

            if (this.settings.ParseTreatmentMetaWithAphia)
            {
                await this.InvokeProcessor<IParseTreatmentMetaWithAphiaController>();
            }

            if (this.settings.ParseTreatmentMetaWithGbif)
            {
                await this.InvokeProcessor<IParseTreatmentMetaWithGbifController>();
            }

            if (this.settings.ParseTreatmentMetaWithCol)
            {
                await this.InvokeProcessor<IParseTreatmentMetaWithCatalogueOfLifeController>();
            }

            return;
        }

        private async Task ContextProcessing(XmlNode context)
        {
            if (this.settings.TagFloats)
            {
                await this.InvokeProcessor<ITagFloatsController>(context);
            }

            if (this.settings.TagReferences)
            {
                await this.InvokeProcessor<ITagReferencesController>(context);
            }

            if (this.settings.ExpandLowerTaxa)
            {
                for (int i = 0; i < ProcessingConstants.NumberOfExpandIterations; ++i)
                {
                    await this.InvokeProcessor<IExpandLowerTaxaController>(context);
                }
            }
        }

        private async Task ReadDocument()
        {
            this.fileProcessor.Read(this.document);

            switch (this.document.XmlDocument.DocumentElement.Name)
            {
                case "article":
                    this.settings.ArticleSchemaType = SchemaType.Nlm;
                    break;

                default:
                    this.settings.ArticleSchemaType = SchemaType.System;
                    break;
            }

            this.document.SchemaType = this.settings.ArticleSchemaType;
            await this.documentNormalizer.NormalizeToSystem(this.document);
        }

        private void SetUpConfigParameters()
        {
            this.settings.ReferencesGetReferencesXmlPath = $"{this.fileProcessor.OutputFileName}-references.xml";
        }

        private void ConfigureFileProcessor()
        {
            int numberOfFileNames = this.settings.FileNames.Count();

            string inputFileName = numberOfFileNames > 0 ? this.settings.FileNames.ElementAt(0) : string.Empty;
            string outputFileName = numberOfFileNames > 1 ? this.settings.FileNames.ElementAt(1) : string.Empty;
            string queryFileName = numberOfFileNames > 2 ? this.settings.FileNames.ElementAt(2) : string.Empty;

            this.fileProcessor = new XmlFileProcessor(inputFileName, outputFileName, this.logger);

            this.logger?.Log(
                Messages.InputOutputFileNamesMessageFormat,
                this.fileProcessor.InputFileName,
                this.fileProcessor.OutputFileName,
                queryFileName);
        }

        private Task WriteOutputFile() => InvokeProcessor(
            Messages.WriteOutputFileMessage,
            _ => this.documentNormalizer.NormalizeToDocumentSchema(this.document)
                .ContinueWith(
                    __ =>
                    {
                        __.Wait();
                        this.fileProcessor.Write(this.document, null, null);
                    }),
                this.logger);
    }
}