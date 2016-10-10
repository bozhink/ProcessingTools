namespace ProcessingTools.Tagger
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Core;
    using Contracts;
    using Ninject;

    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.DocumentProvider;

    public partial class SingleFileProcessor : ISingleFileProcessor
    {
        private readonly ILogger logger;

        private ConcurrentQueue<Task> tasks;
        private XmlFileProcessor fileProcessor;
        private TaxPubDocument document;
        private ProgramSettings settings;

        public SingleFileProcessor(ILogger logger)
        {
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

            var kernel = DI.Kernel;
            try
            {
                this.ConfigureFileProcessor();

                this.SetUpConfigParameters();

                this.ReadDocument();

                if (this.settings.ZoobankCloneXml)
                {
                    await this.InvokeProcessor<IZooBankCloneXmlController>(kernel);
                    return;
                }

                if (this.settings.ZoobankCloneJson)
                {
                    await this.InvokeProcessor<IZooBankCloneJsonController>(kernel);
                    return;
                }

                if (this.settings.ZoobankGenerateRegistrationXml)
                {
                    await this.InvokeProcessor<IZooBankGenerateRegistrationXmlController>(kernel);
                    return;
                }

                if (this.settings.QueryReplace)
                {
                    await this.InvokeProcessor<IQueryReplaceController>(kernel);
                    return;
                }

                if (this.settings.RunXslTransform)
                {
                    this.InvokeProcessor<IRunCustomXslTransformController>(kernel).Wait();
                }

                if (this.settings.InitialFormat)
                {
                    this.InvokeProcessor<IInitialFormatController>(kernel).Wait();
                }

                if (this.settings.ParseReferences)
                {
                    this.InvokeProcessor<IParseReferencesController>(kernel).Wait();
                }

                if (this.settings.TagDoi || this.settings.TagWebLinks)
                {
                    this.InvokeProcessor<ITagWebLinksController>(kernel).Wait();
                }

                if (this.settings.ResolveMediaTypes)
                {
                    this.InvokeProcessor<IResolveMediaTypesController>(kernel).Wait();
                }

                if (this.settings.TagTableFn)
                {
                    this.InvokeProcessor<ITagTableFootnoteController>(kernel).Wait();
                }

                if (this.settings.TagCoordinates)
                {
                    this.InvokeProcessor<ITagCoordinatesController>(kernel).Wait();
                }

                if (this.settings.ParseCoordinates)
                {
                    this.InvokeProcessor<IParseCoordinatesController>(kernel).Wait();
                }

                foreach (var controllerType in this.settings.CalledControllers)
                {
                    this.InvokeProcessor(controllerType, kernel).Wait();
                }

                if (this.settings.TagEnvironmentTermsWithExtract)
                {
                    this.InvokeProcessor<ITagEnvironmentTermsWithExtractController>(kernel).Wait();
                }

                // Tag envo terms using environment database
                if (this.settings.TagEnvironmentTerms)
                {
                    this.InvokeProcessor<ITagEnvironmentTermsController>(kernel).Wait();
                }

                if (this.settings.TagAbbreviations)
                {
                    this.InvokeProcessor<ITagAbbreviationsController>(kernel).Wait();
                }

                // Tag institutions, institutional codes, and specimen codes
                if (this.settings.TagCodes)
                {
                    this.InvokeProcessor<ITagCodesController>(kernel).Wait();
                }

                if (this.settings.TagLowerTaxa)
                {
                    this.InvokeProcessor<ITagLowerTaxaController>(kernel).Wait();
                }

                if (this.settings.TagHigherTaxa)
                {
                    this.InvokeProcessor<ITagHigherTaxaController>(kernel).Wait();
                }

                if (this.settings.ParseLowerTaxa)
                {
                    this.InvokeProcessor<IParseLowerTaxaController>(kernel).Wait();
                }

                if (this.settings.ParseHigherTaxa)
                {
                    this.InvokeProcessor<IParseHigherTaxaWithLocalDbController>(kernel).Wait();
                }

                if (this.settings.ParseHigherWithAphia)
                {
                    this.InvokeProcessor<IParseHigherTaxaWithAphiaController>(kernel).Wait();
                }

                if (this.settings.ParseHigherWithCoL)
                {
                    this.InvokeProcessor<IParseHigherTaxaWithCatalogueOfLifeController>(kernel).Wait();
                }

                if (this.settings.ParseHigherWithGbif)
                {
                    this.InvokeProcessor<IParseHigherTaxaWithGbifController>(kernel).Wait();
                }

                if (this.settings.ParseHigherBySuffix)
                {
                    this.InvokeProcessor<IParseHigherTaxaBySuffixController>(kernel).Wait();
                }

                if (this.settings.ParseHigherAboveGenus)
                {
                    this.InvokeProcessor<IParseHigherTaxaAboveGenusController>(kernel).Wait();
                }

                // Main Tagging part of the program
                if (this.settings.ParseBySection)
                {
                    foreach (XmlNode context in this.document.XmlDocument.SelectNodes(this.settings.HigherStructrureXpath, this.document.NamespaceManager))
                    {
                        this.ContextProcessing(context, kernel).Wait();
                    }
                }
                else
                {
                    this.ContextProcessing(this.document.XmlDocument.DocumentElement, kernel).Wait();
                }

                if (this.settings.ExtractTaxa || this.settings.ExtractLowerTaxa || this.settings.ExtractHigherTaxa)
                {
                    await this.InvokeProcessor<IExtractTaxaController>(kernel);
                }

                if (this.settings.ValidateTaxa)
                {
                    this.tasks.Enqueue(this.InvokeProcessor<IValidateTaxaController>(kernel));
                }

                if (this.settings.UntagSplit)
                {
                    this.InvokeProcessor<IRemoveAllTaxonNamePartTagsController>(kernel).Wait();
                }

                if (this.settings.FormatTreat)
                {
                    this.InvokeProcessor<IFormatTreatmentsController>(kernel).Wait();
                }

                if (this.settings.ParseTreatmentMetaWithAphia)
                {
                    this.InvokeProcessor<IParseTreatmentMetaWithAphiaController>(kernel).Wait();
                }

                if (this.settings.ParseTreatmentMetaWithGbif)
                {
                    this.InvokeProcessor<IParseTreatmentMetaWithGbifController>(kernel).Wait();
                }

                if (this.settings.ParseTreatmentMetaWithCol)
                {
                    this.InvokeProcessor<IParseTreatmentMetaWithCatalogueOfLifeController>(kernel).Wait();
                }

                this.tasks.Enqueue(this.WriteOutputFile());

                Task.WaitAll(this.tasks.ToArray());
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
                throw;
            }
        }

        private async Task ContextProcessing(XmlNode context, IKernel kernel)
        {
            if (this.settings.TagFloats)
            {
                await this.InvokeProcessor<ITagFloatsController>(context, kernel);
            }

            if (this.settings.TagReferences)
            {
                await this.InvokeProcessor<ITagReferencesController>(context, kernel);
            }

            if (this.settings.ExpandLowerTaxa)
            {
                for (int i = 0; i < ProcessingConstants.NumberOfExpandIterations; ++i)
                {
                    await this.InvokeProcessor<IExpandLowerTaxaController>(context, kernel);
                }
            }
        }

        private void ReadDocument()
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

            this.document.Xml = this.document.Xml.NormalizeXmlToSystemXml();
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

        private async Task WriteOutputFile()
        {
            await InvokeProcessor(
                Messages.WriteOutputFileMessage,
                _ => Task.Run(() =>
                {
                    this.document.Xml = this.document.Xml.NormalizeXmlToCurrentXml(this.settings.ArticleSchemaType);
                    this.fileProcessor.Write(this.document, null, null);
                }),
                this.logger);
        }
    }
}