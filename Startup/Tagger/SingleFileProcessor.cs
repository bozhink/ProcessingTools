namespace ProcessingTools.MainProgram
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Ninject;
    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.DocumentProvider;

    public class SingleFileProcessor : FileProcessor
    {
        private XmlFileProcessor fileProcessor;
        private TaxPubDocument document;

        private ILogger logger;
        private ProgramSettings settings;

        public SingleFileProcessor(ProgramSettings settings, ILogger logger)
        {
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
                        this.InvokeProcessor<IZooBankCloneXmlController>(Messages.CloneZooBankXmlMessage, kernel).Wait();
                    }
                    else if (this.settings.ZoobankCloneJson)
                    {
                        this.InvokeProcessor<IZooBankCloneJsonController>(Messages.CloneZooBankJsonMessage, kernel).Wait();
                    }
                    else if (this.settings.ZoobankGenerateRegistrationXml)
                    {
                        this.InvokeProcessor<IZooBankGenerateRegistrationXmlController>(Messages.GenerateRegistrationXmlForZooBankMessage, kernel).Wait();
                    }
                    else if (this.settings.QueryReplace && !string.IsNullOrWhiteSpace(this.settings.QueryFileName))
                    {
                        this.InvokeProcessor<IQueryReplaceController>(string.Empty, kernel).Wait();
                    }
                    else
                    {
                        if (this.settings.RunXslTransform)
                        {
                            this.InvokeProcessor<IRunCustomXslTransformController>(Messages.RunCustomXslTransformMessage, kernel).Wait();
                        }

                        if (this.settings.InitialFormat)
                        {
                            this.InvokeProcessor<IInitialFormatController>(Messages.InitialFormatMessage, kernel).Wait();
                        }

                        if (this.settings.ParseReferences)
                        {
                            this.InvokeProcessor<IParseReferencesController>(Messages.ParseReferencesMessage, kernel).Wait();
                        }

                        if (this.settings.TagDoi || this.settings.TagWebLinks)
                        {
                            this.InvokeProcessor<ITagWebLinksController>(Messages.TagWebLinksMessage, kernel).Wait();
                        }

                        if (this.settings.ResolveMediaTypes)
                        {
                            this.InvokeProcessor<IResolveMediaTypesController>(Messages.ResolveMediaTypesMessage, kernel).Wait();
                        }

                        if (this.settings.TagTableFn)
                        {
                            this.InvokeProcessor<ITagTableFootnoteController>(Messages.TagTableFootNotesMessage, kernel).Wait();
                        }

                        if (this.settings.TagCoordinates)
                        {
                            this.InvokeProcessor<ITagCoordinatesController>(Messages.TagCoordinatesMessage, kernel).Wait();
                        }

                        if (this.settings.ParseCoordinates)
                        {
                            this.InvokeProcessor<IParseCoordinatesController>(Messages.ParseCoordinatesMessage, kernel).Wait();
                        }

                        if (this.settings.TagMorphologicalEpithets)
                        {
                            this.InvokeProcessor<ITagMorphologicalEpithetsController>(Messages.TagMorphologicalEpithetsMessage, kernel).Wait();
                        }

                        if (this.settings.TagGeoNames)
                        {
                            this.InvokeProcessor<ITagGeoNamesController>(Messages.TagGeoNamesMessage, kernel).Wait();
                        }

                        if (this.settings.TagGeoEpithets)
                        {
                            this.InvokeProcessor<ITagGeoEpithetsController>(Messages.TagGeoEpithetsMessage, kernel).Wait();
                        }

                        if (this.settings.TagInstitutions)
                        {
                            this.InvokeProcessor<ITagInstitutionsController>(Messages.TagInstitutionsMessage, kernel).Wait();
                        }

                        if (this.settings.TagProducts)
                        {
                            this.InvokeProcessor<ITagProductsController>(Messages.TagProductsMessage, kernel).Wait();
                        }

                        if (this.settings.TagEnvironmentTermsWithExtract)
                        {
                            this.InvokeProcessor<ITagEnvironmentTermsWithExtractController>(Messages.TagEnvironmentTermsWithExtractMessage, kernel).Wait();
                        }

                        // Tag envo terms using envornment database
                        if (this.settings.TagEnvironmentTerms)
                        {
                            this.InvokeProcessor<ITagEnvironmentTermsController>(Messages.TagEnvironmentTermsMessage, kernel).Wait();
                        }

                        if (this.settings.TagQuantities)
                        {
                            this.InvokeProcessor<ITagAltitudesController>(Messages.TagAltitudesMessage, kernel).Wait();

                            this.InvokeProcessor<ITagGeographicDeviationsController>(Messages.TagGeographicDeviationsMessage, kernel).Wait();

                            this.InvokeProcessor<ITagQuantitiesController>(Messages.TagQuantitiesMessage, kernel).Wait();
                        }

                        if (this.settings.TagDates)
                        {
                            this.InvokeProcessor<ITagDatesController>(Messages.TagDatesMessage, kernel).Wait();
                        }

                        if (this.settings.TagAbbreviations)
                        {
                            this.InvokeProcessor<ITagAbbreviationsController>(Messages.TagAbbreviationsMessage, kernel).Wait();
                        }

                        // Tag institutions, institutional codes, and specimen codes
                        if (this.settings.TagCodes)
                        {
                            this.InvokeProcessor<ITagCodesController>(Messages.TagCodesMessage, kernel).Wait();
                        }

                        // Do something as an experimental feature
                        if (this.settings.TestFlag)
                        {
                            this.InvokeProcessor<ITestController>("\n\n\tTest\n\n", kernel).Wait();
                        }

                        if (this.settings.TagLowerTaxa)
                        {
                            this.InvokeProcessor<ITagLowerTaxaController>(Messages.TagLowerTaxaMessage, kernel).Wait();
                        }

                        if (this.settings.TagHigherTaxa)
                        {
                            this.InvokeProcessor<ITagHigherTaxaController>(Messages.TagHigherTaxaMessage, kernel).Wait();
                        }

                        if (this.settings.ParseLowerTaxa)
                        {
                            this.InvokeProcessor<IParseLowerTaxaController>(Messages.ParseLowerTaxaMessage, kernel).Wait();
                        }

                        if (this.settings.ParseHigherTaxa)
                        {
                            this.InvokeProcessor<IParseHigherTaxaWithLocalDbController>(Messages.ParseHigherTaxaMessage, kernel).Wait();
                        }

                        if (this.settings.ParseHigherWithAphia)
                        {
                            this.InvokeProcessor<IParseHigherTaxaWithAphiaController>(Messages.ParseHigherTaxaWithAphiaMessage, kernel).Wait();
                        }

                        if (this.settings.ParseHigherWithCoL)
                        {
                            this.InvokeProcessor<IParseHigherTaxaWithCatalogueOfLifeController>(Messages.ParseHigherTaxaWithCoLMessage, kernel).Wait();
                        }

                        if (this.settings.ParseHigherWithGbif)
                        {
                            this.InvokeProcessor<IParseHigherTaxaWithGbifController>(Messages.ParseHigherTaxaWithGbifMessage, kernel).Wait();
                        }

                        if (this.settings.ParseHigherBySuffix)
                        {
                            this.InvokeProcessor<IParseHigherTaxaBySuffixController>(Messages.ParseHigherTaxaBySuffixMessage, kernel).Wait();
                        }

                        if (this.settings.ParseHigherAboveGenus)
                        {
                            this.InvokeProcessor<IParseHigherTaxaAboveGenusController>(Messages.ParseHigherTaxaAboveGenusMessage, kernel).Wait();
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
                            await this.InvokeProcessor<IExtractTaxaController>(string.Empty, kernel);
                        }

                        if (this.settings.ValidateTaxa)
                        {
                            await this.InvokeProcessor<IValidateTaxaController>(Messages.ValidateTaxaUsingGnrMessage, kernel);
                        }

                        if (this.settings.UntagSplit)
                        {
                            this.InvokeProcessor<IRemoveAllTaxonNamePartTagsController>(string.Empty, kernel).Wait();
                        }

                        if (this.settings.FormatTreat)
                        {
                            this.InvokeProcessor<IFormatTreatmentsController>(Messages.FormatTreatmentsMessage, kernel).Wait();
                        }

                        if (this.settings.ParseTreatmentMetaWithAphia)
                        {
                            this.InvokeProcessor<IParseTreatmentMetaWithAphiaController>(Messages.ParseTreatmentMetaWithAphiaMessage, kernel).Wait();
                        }

                        if (this.settings.ParseTreatmentMetaWithGbif)
                        {
                            this.InvokeProcessor<IParseTreatmentMetaWithGbifController>(Messages.ParseTreatmentMetaWithGbifMessage, kernel).Wait();
                        }

                        if (this.settings.ParseTreatmentMetaWithCol)
                        {
                            this.InvokeProcessor<IParseTreatmentMetaWithCatalogueOfLifeController>(Messages.ParseTreatmentMetaWithCoLMessage, kernel).Wait();
                        }
                    }
                }

                await this.WriteOutputFile();
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

        protected async Task InvokeProcessor<TController>(string message, IKernel kernel)
            where TController : ITaggerController
        {
            await this.InvokeProcessor<TController>(message, this.document.XmlDocument.DocumentElement, kernel);
        }

        protected async Task InvokeProcessor<TController>(string message, XmlNode context, IKernel kernel)
            where TController : ITaggerController
        {
            var controller = kernel.Get<TController>();

            message = GetDescriptionMessageForController(controller);

            await this.InvokeProcessor(
                message,
                () =>
                {
                    controller.Run(context, this.document.NamespaceManager, this.settings, this.logger).Wait();
                });
        }

        private static string GetDescriptionMessageForController(ITaggerController controller)
        {
            string message = controller.GetDescription();

            if (string.IsNullOrWhiteSpace(message))
            {
                var type = controller.GetType();
                var name = Regex.Replace(type.FullName, @".*?([^\.]+)\Z", "$1");
                name = Regex.Replace(name, @"Controller\Z", string.Empty);

                message = Regex.Replace(name, "(?=[A-Z])", " ").Trim();
            }

            return $"\n\t{message}\n";
        }

        private Task MainProcessing(XmlNode context, IKernel kernel)
        {
            return Task.Run(() =>
            {
                if (this.settings.TagFloats)
                {
                    this.InvokeProcessor<ITagFloatsController>(Messages.TagFloatsMessage, context, kernel).Wait();
                }

                if (this.settings.TagReferences)
                {
                    this.InvokeProcessor<ITagReferencesController>(Messages.TagReferencesMessage, context, kernel).Wait();
                }

                if (this.settings.ExpandLowerTaxa)
                {
                    for (int i = 0; i < ProcessingConstants.NumberOfExpandIterations; ++i)
                    {
                        this.InvokeProcessor<IExpandLowerTaxaController>(null, context, kernel).Wait();
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

            this.document.Xml = this.document.Xml.NormalizeXmlToSystemXml(this.settings.Config);
        }

        private void SetUpConfigParameters()
        {
            string tempDirectory = this.settings.Config.TempDirectoryPath;
            string outputFileName = Path.GetFileNameWithoutExtension(this.fileProcessor.OutputFileName);

            this.settings.Config.EnvoResponseOutputXmlFileName = $"{tempDirectory}\\envo-{outputFileName}.xml";
            this.settings.Config.GnrOutputFileName = $"{tempDirectory}\\gnr-{outputFileName}.xml";
            this.settings.Config.ReferencesTagTemplateXmlPath = $"{tempDirectory}\\{outputFileName}-references-tag-template.xml";

            this.settings.Config.ReferencesGetReferencesXmlPath = $"{this.fileProcessor.OutputFileName}-references.xml";
        }

        private void ConfigureFileProcessor()
        {
            this.fileProcessor = new XmlFileProcessor(this.settings.InputFileName, this.settings.OutputFileName, this.logger);

            this.logger?.Log(
                Messages.InputOutputFileNamesMessageFormat,
                this.fileProcessor.InputFileName,
                this.fileProcessor.OutputFileName,
                this.settings.QueryFileName);
        }

        private async Task WriteOutputFile()
        {
            await this.InvokeProcessor(
                Messages.WriteOutputFileMessage,
                () =>
                {
                    this.document.Xml = this.document.Xml.NormalizeXmlToCurrentXml(this.settings.Config);
                    this.fileProcessor.Write(this.document, null, null);
                });
        }
    }
}