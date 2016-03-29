namespace ProcessingTools.Tagger
{
    using System;
    using System.Collections.Concurrent;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Extensions;
    using Ninject;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.DocumentProvider;

    public class SingleFileProcessor : FileProcessor
    {
        private ConcurrentQueue<Task> tasks;

        private XmlFileProcessor fileProcessor;
        private TaxPubDocument document;

        private ILogger logger;
        private ProgramSettings settings;

        public SingleFileProcessor(ProgramSettings settings, ILogger logger)
        {
            this.tasks = new ConcurrentQueue<Task>();
            this.settings = settings;
            this.logger = logger;
            this.document = new TaxPubDocument();
        }

        public override async Task Run()
        {
            try
            {
                this.ConfigureFileProcessor();

                this.SetUpConfigParameters();

                this.ReadDocument();

                using (IKernel kernel = NinjectConfig.CreateKernel())
                {
                    if (this.settings.ZoobankCloneXml)
                    {
                        this.InvokeProcessor<IZooBankCloneXmlController>(kernel).Wait();
                    }
                    else if (this.settings.ZoobankCloneJson)
                    {
                        this.InvokeProcessor<IZooBankCloneJsonController>(kernel).Wait();
                    }
                    else if (this.settings.ZoobankGenerateRegistrationXml)
                    {
                        this.InvokeProcessor<IZooBankGenerateRegistrationXmlController>(kernel).Wait();
                    }
                    else if (this.settings.QueryReplace)
                    {
                        this.InvokeProcessor<IQueryReplaceController>(kernel).Wait();
                    }
                    else
                    {
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

                        // Tag envo terms using envornment database
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
                                this.MainProcessing(context, kernel).Wait();
                            }
                        }
                        else
                        {
                            this.MainProcessing(this.document.XmlDocument.DocumentElement, kernel).Wait();
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
                    }
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

        protected override Task InvokeProcessor(string message, Action action)
        {
            return Task.Run(() =>
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.logger?.Log(message);

                try
                {
                    action.Invoke();
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, string.Empty);
                }

                this.PrintElapsedTime(timer);
            });
        }

        protected async Task InvokeProcessor<TController>(IKernel kernel)
            where TController : ITaggerController
        {
            await this.InvokeProcessor<TController>(this.document.XmlDocument.DocumentElement, kernel);
        }

        protected async Task InvokeProcessor<TController>(XmlNode context, IKernel kernel)
            where TController : ITaggerController
        {
            var controller = kernel.Get<TController>();

            string message = controller.GetDescriptionMessageForController();

            await this.InvokeProcessor(
                message,
                () =>
                {
                    controller.Run(context, this.document.NamespaceManager, this.settings, this.logger).Wait();
                });
        }

        protected async Task InvokeProcessor(Type controllerType, IKernel kernel)
        {
            // Do not wait validation controllers to return.
            var validationController = controllerType.GetInterfaces()?.FirstOrDefault(t => t == typeof(INotAwaitableController));
            if (validationController != null)
            {
                // Validation controllers should not overwrite the content of this.document.XmlDocument,
                // and here this content is copied in a new DOM object.
                XmlDocument document = new XmlDocument
                {
                    PreserveWhitespace = true
                };

                document.LoadXml(this.document.Xml);
                this.tasks.Enqueue(this.InvokeProcessor(controllerType, document.DocumentElement, kernel));
            }
            else
            {
                await this.InvokeProcessor(controllerType, this.document.XmlDocument.DocumentElement, kernel);
            }
        }

        protected async Task InvokeProcessor(Type controllerType, XmlNode context, IKernel kernel)
        {
            var controller = kernel.Get(controllerType) as ITaggerController;

            string message = controller.GetDescriptionMessageForController();

            await this.InvokeProcessor(
                message,
                () =>
                {
                    controller.Run(context, this.document.NamespaceManager, this.settings, this.logger).Wait();
                });
        }

        private Task MainProcessing(XmlNode context, IKernel kernel)
        {
            return Task.Run(() =>
            {
                if (this.settings.TagFloats)
                {
                    this.InvokeProcessor<ITagFloatsController>(context, kernel).Wait();
                }

                if (this.settings.TagReferences)
                {
                    this.InvokeProcessor<ITagReferencesController>(context, kernel).Wait();
                }

                if (this.settings.ExpandLowerTaxa)
                {
                    for (int i = 0; i < ProcessingConstants.NumberOfExpandIterations; ++i)
                    {
                        this.InvokeProcessor<IExpandLowerTaxaController>(context, kernel).Wait();
                    }
                }
            });
        }

        private void PrintElapsedTime(Stopwatch timer)
        {
            this.logger?.Log(LogType.Info, Messages.ElapsedTimeMessageFormat, timer.Elapsed);
        }

        private void ReadDocument()
        {
            this.fileProcessor.Read(this.document);

            switch (this.document.XmlDocument.DocumentElement.Name)
            {
                case "article":
                    this.settings.Config.ArticleSchemaType = SchemaType.Nlm;
                    break;

                default:
                    this.settings.Config.ArticleSchemaType = SchemaType.System;
                    break;
            }

            this.document.Xml = this.document.Xml.NormalizeXmlToSystemXml();
        }

        private void SetUpConfigParameters()
        {
            string tempDirectory = this.settings.Config.TempDirectoryPath;
            string outputFileName = Path.GetFileNameWithoutExtension(this.fileProcessor.OutputFileName);

            this.settings.Config.ReferencesGetReferencesXmlPath = $"{this.fileProcessor.OutputFileName}-references.xml";
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
            await this.InvokeProcessor(
                Messages.WriteOutputFileMessage,
                () =>
                {
                    this.document.Xml = this.document.Xml.NormalizeXmlToCurrentXml(this.settings.Config.ArticleSchemaType);
                    this.fileProcessor.Write(this.document, null, null);
                });
        }
    }
}