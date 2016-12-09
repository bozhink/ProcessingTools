namespace ProcessingTools.Tagger.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts;
    using Contracts.Controllers;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Files.Generators;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;
    using ProcessingTools.Services.Data.Contracts.Files;

    public partial class SingleFileProcessor : IFileProcessor
    {
        private readonly Func<Type, ITaggerController> controllerFactory;
        private readonly IDocumentFactory documentFactory;
        private readonly IDocumentNormalizer documentNormalizer;
        private readonly IFileNameGenerator fileNameGenerator;
        private readonly IXmlFileContentDataService filesManager;
        private readonly ILogger logger;
        private IDocument document;
        private IProgramSettings settings;
        private ConcurrentQueue<Task> tasks;

        public SingleFileProcessor(
            IXmlFileContentDataService filesManager,
            IFileNameGenerator fileNameGenerator,
            IDocumentFactory documentFactory,
            IDocumentNormalizer documentNormalizer,
            Func<Type, ITaggerController> controllerFactory,
            ILogger logger)
        {
            if (filesManager == null)
            {
                throw new ArgumentNullException(nameof(filesManager));
            }

            if (fileNameGenerator == null)
            {
                throw new ArgumentNullException(nameof(fileNameGenerator));
            }

            if (documentFactory == null)
            {
                throw new ArgumentNullException(nameof(documentFactory));
            }

            if (documentNormalizer == null)
            {
                throw new ArgumentNullException(nameof(documentNormalizer));
            }

            if (controllerFactory == null)
            {
                throw new ArgumentNullException(nameof(controllerFactory));
            }

            this.filesManager = filesManager;
            this.fileNameGenerator = fileNameGenerator;
            this.documentFactory = documentFactory;
            this.documentNormalizer = documentNormalizer;
            this.controllerFactory = controllerFactory;
            this.logger = logger;

            this.tasks = new ConcurrentQueue<Task>();
        }

        public string InputFileName { get; set; }

        public string OutputFileName { get; set; }

        public string QueryFileName { get; set; }

        public async Task Run(IProgramSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            this.settings = settings;

            try
            {
                await this.ConfigureFileProcessor();

                this.SetUpConfigParameters();

                await this.ReadDocument();

                await this.ProcessDocument(this.document.XmlDocument.DocumentElement);

                this.tasks.Enqueue(this.WriteOutputFile());

                Task.WaitAll(this.tasks.ToArray());
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
                throw;
            }
        }

        private async Task ConfigureFileProcessor()
        {
            int numberOfFileNames = this.settings.FileNames.Count;

            if (numberOfFileNames < 1)
            {
                throw new InvalidOperationException("The name of the input file is required");
            }

            this.InputFileName = this.settings.FileNames[0];

            this.OutputFileName = numberOfFileNames > 1 ?
                this.settings.FileNames[1] :
                await this.fileNameGenerator.Generate(
                    this.InputFileName,
                    FileConstants.MaximalLengthOfGeneratedNewFileName,
                    true);

            this.QueryFileName = numberOfFileNames > 2 ?
                this.settings.FileNames[2] :
                string.Empty;

            this.logger?.Log(
                Messages.InputOutputFileNamesMessageFormat,
                this.InputFileName,
                this.OutputFileName,
                this.QueryFileName);
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

        private async Task ProcessDocument(XmlNode context)
        {
            if (this.settings.ZoobankCloneXml)
            {
                await this.InvokeProcessor<IZooBankCloneXmlController>(context);
                return;
            }

            if (this.settings.ZoobankCloneJson)
            {
                await this.InvokeProcessor<IZooBankCloneJsonController>(context);
                return;
            }

            if (this.settings.ZoobankGenerateRegistrationXml)
            {
                await this.InvokeProcessor<IZooBankGenerateRegistrationXmlController>(context);
                return;
            }

            if (this.settings.QueryReplace)
            {
                await this.InvokeProcessor<IQueryReplaceController>(context);
                return;
            }

            if (this.settings.RunXslTransform)
            {
                await this.InvokeProcessor<IRunCustomXslTransformController>(context);
            }

            if (this.settings.InitialFormat)
            {
                await this.InvokeProcessor<IInitialFormatController>(context);
            }

            if (this.settings.ParseReferences)
            {
                await this.InvokeProcessor<IParseReferencesController>(context);
            }

            if (this.settings.TagDoi || this.settings.TagWebLinks)
            {
                await this.InvokeProcessor<ITagWebLinksController>(context);
            }

            if (this.settings.ResolveMediaTypes)
            {
                await this.InvokeProcessor<IResolveMediaTypesController>(context);
            }

            if (this.settings.TagTableFn)
            {
                await this.InvokeProcessor<ITagTableFootnoteController>(context);
            }

            if (this.settings.TagCoordinates)
            {
                await this.InvokeProcessor<ITagCoordinatesController>(context);
            }

            if (this.settings.ParseCoordinates)
            {
                await this.InvokeProcessor<IParseCoordinatesController>(context);
            }

            foreach (var controllerType in this.settings.CalledControllers)
            {
                await this.InvokeProcessor(controllerType, context);
            }

            if (this.settings.TagEnvironmentTermsWithExtract)
            {
                await this.InvokeProcessor<ITagEnvironmentTermsWithExtractController>(context);
            }

            // Tag envo terms using environment database
            if (this.settings.TagEnvironmentTerms)
            {
                await this.InvokeProcessor<ITagEnvironmentTermsController>(context);
            }

            if (this.settings.TagAbbreviations)
            {
                await this.InvokeProcessor<ITagAbbreviationsController>(context);
            }

            // Tag institutions, institutional codes, and specimen codes
            if (this.settings.TagCodes)
            {
                await this.InvokeProcessor<ITagInstitutionalCodesController>(context);
            }

            if (this.settings.TagLowerTaxa)
            {
                await this.InvokeProcessor<ITagLowerTaxaController>(context);
            }

            if (this.settings.TagHigherTaxa)
            {
                await this.InvokeProcessor<ITagHigherTaxaController>(context);
            }

            if (this.settings.ParseLowerTaxa)
            {
                await this.InvokeProcessor<IParseLowerTaxaController>(context);
            }

            if (this.settings.ParseHigherTaxa)
            {
                await this.InvokeProcessor<IParseHigherTaxaWithLocalDbController>(context);
            }

            if (this.settings.ParseHigherWithAphia)
            {
                await this.InvokeProcessor<IParseHigherTaxaWithAphiaController>(context);
            }

            if (this.settings.ParseHigherWithCoL)
            {
                await this.InvokeProcessor<IParseHigherTaxaWithCatalogueOfLifeController>(context);
            }

            if (this.settings.ParseHigherWithGbif)
            {
                await this.InvokeProcessor<IParseHigherTaxaWithGbifController>(context);
            }

            if (this.settings.ParseHigherBySuffix)
            {
                await this.InvokeProcessor<IParseHigherTaxaBySuffixController>(context);
            }

            if (this.settings.ParseHigherAboveGenus)
            {
                await this.InvokeProcessor<IParseHigherTaxaAboveGenusController>(context);
            }

            // Main Tagging part of the program
            if (this.settings.ParseBySection)
            {
                foreach (XmlNode subcontext in this.document.XmlDocument.SelectNodes(this.settings.HigherStructrureXpath, this.document.NamespaceManager))
                {
                    await this.ContextProcessing(subcontext);
                }
            }
            else
            {
                await this.ContextProcessing(context);
            }

            if (this.settings.ExtractTaxa || this.settings.ExtractLowerTaxa || this.settings.ExtractHigherTaxa)
            {
                await this.InvokeProcessor<IExtractTaxaController>(context);
            }

            if (this.settings.ValidateTaxa)
            {
                this.tasks.Enqueue(this.InvokeProcessor<IValidateTaxaController>(context));
            }

            if (this.settings.UntagSplit)
            {
                await this.InvokeProcessor<IRemoveAllTaxonNamePartTagsController>(context);
            }

            if (this.settings.FormatTreat)
            {
                await this.InvokeProcessor<IFormatTreatmentsController>(context);
            }

            if (this.settings.ParseTreatmentMetaWithAphia)
            {
                await this.InvokeProcessor<IParseTreatmentMetaWithAphiaController>(context);
            }

            if (this.settings.ParseTreatmentMetaWithGbif)
            {
                await this.InvokeProcessor<IParseTreatmentMetaWithGbifController>(context);
            }

            if (this.settings.ParseTreatmentMetaWithCol)
            {
                await this.InvokeProcessor<IParseTreatmentMetaWithCatalogueOfLifeController>(context);
            }

            return;
        }

        private async Task ReadDocument()
        {
            var document = await this.filesManager.ReadXmlFile(this.InputFileName);
            this.document = this.documentFactory.Create(document.OuterXml);

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
            this.settings.ReferencesGetReferencesXmlPath = $"{this.OutputFileName}-references.xml";
        }

        private Task WriteOutputFile() => InvokeProcessor(
            Messages.WriteOutputFileMessage,
            _ => this.documentNormalizer.NormalizeToDocumentSchema(this.document)
                .ContinueWith(
                    __ =>
                    {
                        __.Wait();
                        var o = this.filesManager.WriteXmlFile(this.OutputFileName, this.document.XmlDocument).Result;
                    }),
                this.logger);
    }
}
