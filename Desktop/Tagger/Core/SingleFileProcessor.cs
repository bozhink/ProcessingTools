namespace ProcessingTools.Tagger.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts;
    using Contracts.Commands;
    using ProcessingTools.Constants;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Files.Generators;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;
    using ProcessingTools.Processors.Contracts.Documents;

    public partial class SingleFileProcessor : IFileProcessor
    {
        private readonly Func<Type, ITaggerCommand> commandFactory;
        private readonly IDocumentFactory documentFactory;
        private readonly IDocumentMerger documentMerger;
        private readonly IDocumentReader documentReader;
        private readonly IDocumentWriter documentWriter;
        private readonly IDocumentNormalizer documentNormalizer;
        private readonly IFileNameGenerator fileNameGenerator;
        private readonly ILogger logger;
        private IDocument document;
        private IProgramSettings settings;
        private ConcurrentQueue<Task> tasks;

        public SingleFileProcessor(
            IFileNameGenerator fileNameGenerator,
            IDocumentFactory documentFactory,
            IDocumentMerger documentMerger,
            IDocumentReader documentReader,
            IDocumentWriter documentWriter,
            IDocumentNormalizer documentNormalizer,
            Func<Type, ITaggerCommand> commandFactory,
            ILogger logger)
        {
            if (fileNameGenerator == null)
            {
                throw new ArgumentNullException(nameof(fileNameGenerator));
            }

            if (documentFactory == null)
            {
                throw new ArgumentNullException(nameof(documentFactory));
            }

            if (documentMerger == null)
            {
                throw new ArgumentNullException(nameof(documentMerger));
            }

            if (documentReader == null)
            {
                throw new ArgumentNullException(nameof(documentReader));
            }

            if (documentWriter == null)
            {
                throw new ArgumentNullException(nameof(documentWriter));
            }

            if (documentNormalizer == null)
            {
                throw new ArgumentNullException(nameof(documentNormalizer));
            }

            if (commandFactory == null)
            {
                throw new ArgumentNullException(nameof(commandFactory));
            }

            this.fileNameGenerator = fileNameGenerator;
            this.documentFactory = documentFactory;
            this.documentMerger = documentMerger;
            this.documentReader = documentReader;
            this.documentWriter = documentWriter;
            this.documentNormalizer = documentNormalizer;
            this.commandFactory = commandFactory;
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

            if (this.settings.MergeInputFiles)
            {
                this.OutputFileName = await this.fileNameGenerator.Generate(
                    Path.Combine(Path.GetDirectoryName(this.InputFileName), FileConstants.DefaultBundleXmlFileName),
                    FileConstants.MaximalLengthOfGeneratedNewFileName,
                    true);

                this.logger?.Log(
                    Messages.InputOutputFileNamesMessageFormat,
                    "*",
                    this.OutputFileName,
                    string.Empty);
            }
            else
            {
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
        }

        private async Task ContextProcessing(XmlNode context)
        {
            if (this.settings.TagFloats)
            {
                await this.InvokeProcessor<ITagFloatsCommand>(context);
            }

            if (this.settings.TagReferences)
            {
                await this.InvokeProcessor<ITagReferencesCommand>(context);
            }

            if (this.settings.ExpandLowerTaxa)
            {
                for (int i = 0; i < ProcessingConstants.NumberOfExpandIterations; ++i)
                {
                    await this.InvokeProcessor<IExpandLowerTaxaCommand>(context);
                }
            }
        }

        private async Task ProcessDocument(XmlNode context)
        {
            if (this.settings.ZoobankCloneXml)
            {
                await this.InvokeProcessor<IZooBankCloneXmlCommand>(context);
                return;
            }

            if (this.settings.ZoobankCloneJson)
            {
                await this.InvokeProcessor<IZooBankCloneJsonCommand>(context);
                return;
            }

            if (this.settings.ZoobankGenerateRegistrationXml)
            {
                await this.InvokeProcessor<IZooBankGenerateRegistrationXmlCommand>(context);
                return;
            }

            if (this.settings.QueryReplace)
            {
                await this.InvokeProcessor<IQueryReplaceCommand>(context);
                return;
            }

            if (this.settings.RunXslTransform)
            {
                await this.InvokeProcessor<IRunCustomXslTransformCommand>(context);
            }

            if (this.settings.InitialFormat)
            {
                await this.InvokeProcessor<IInitialFormatCommand>(context);
            }

            if (this.settings.ParseReferences)
            {
                await this.InvokeProcessor<IParseReferencesCommand>(context);
            }

            if (this.settings.TagDoi || this.settings.TagWebLinks)
            {
                await this.InvokeProcessor<ITagWebLinksCommand>(context);
            }

            if (this.settings.ResolveMediaTypes)
            {
                await this.InvokeProcessor<IResolveMediaTypesCommand>(context);
            }

            if (this.settings.TagTableFn)
            {
                await this.InvokeProcessor<ITagTableFootnoteCommand>(context);
            }

            if (this.settings.TagCoordinates)
            {
                await this.InvokeProcessor<ITagCoordinatesCommand>(context);
            }

            if (this.settings.ParseCoordinates)
            {
                await this.InvokeProcessor<IParseCoordinatesCommand>(context);
            }

            foreach (var commandType in this.settings.CalledCommands)
            {
                await this.InvokeProcessor(commandType, context);
            }

            if (this.settings.TagEnvironmentTermsWithExtract)
            {
                await this.InvokeProcessor<ITagEnvironmentTermsWithExtractCommand>(context);
            }

            // Tag envo terms using environment database
            if (this.settings.TagEnvironmentTerms)
            {
                await this.InvokeProcessor<ITagEnvironmentTermsCommand>(context);
            }

            if (this.settings.TagAbbreviations)
            {
                await this.InvokeProcessor<ITagAbbreviationsCommand>(context);
            }

            // Tag institutions, institutional codes, and specimen codes
            if (this.settings.TagCodes)
            {
                await this.InvokeProcessor<ITagInstitutionalCodesCommand>(context);
            }

            if (this.settings.TagLowerTaxa)
            {
                await this.InvokeProcessor<ITagLowerTaxaCommand>(context);
            }

            if (this.settings.TagHigherTaxa)
            {
                await this.InvokeProcessor<ITagHigherTaxaCommand>(context);
            }

            if (this.settings.ParseLowerTaxa)
            {
                await this.InvokeProcessor<IParseLowerTaxaCommand>(context);
            }

            if (this.settings.ParseHigherTaxa)
            {
                await this.InvokeProcessor<IParseHigherTaxaWithLocalDbCommand>(context);
            }

            if (this.settings.ParseHigherWithAphia)
            {
                await this.InvokeProcessor<IParseHigherTaxaWithAphiaCommand>(context);
            }

            if (this.settings.ParseHigherWithCoL)
            {
                await this.InvokeProcessor<IParseHigherTaxaWithCatalogueOfLifeCommand>(context);
            }

            if (this.settings.ParseHigherWithGbif)
            {
                await this.InvokeProcessor<IParseHigherTaxaWithGbifCommand>(context);
            }

            if (this.settings.ParseHigherBySuffix)
            {
                await this.InvokeProcessor<IParseHigherTaxaBySuffixCommand>(context);
            }

            if (this.settings.ParseHigherAboveGenus)
            {
                await this.InvokeProcessor<IParseHigherTaxaAboveGenusCommand>(context);
            }

            // Main Tagging part of the program
            foreach (XmlNode subcontext in this.document.SelectNodes(XPathStrings.HigherDocumentStructure))
            {
                await this.ContextProcessing(subcontext);
            }

            if (this.settings.ExtractTaxa || this.settings.ExtractLowerTaxa || this.settings.ExtractHigherTaxa)
            {
                await this.InvokeProcessor<IExtractTaxaCommand>(context);
            }

            if (this.settings.ValidateTaxa)
            {
                this.tasks.Enqueue(this.InvokeProcessor<IValidateTaxaCommand>(context));
            }

            if (this.settings.UntagSplit)
            {
                await this.InvokeProcessor<IRemoveAllTaxonNamePartTagsCommand>(context);
            }

            if (this.settings.FormatTreat)
            {
                await this.InvokeProcessor<IFormatTreatmentsCommand>(context);
            }

            if (this.settings.ParseTreatmentMetaWithAphia)
            {
                await this.InvokeProcessor<IParseTreatmentMetaWithAphiaCommand>(context);
            }

            if (this.settings.ParseTreatmentMetaWithGbif)
            {
                await this.InvokeProcessor<IParseTreatmentMetaWithGbifCommand>(context);
            }

            if (this.settings.ParseTreatmentMetaWithCol)
            {
                await this.InvokeProcessor<IParseTreatmentMetaWithCatalogueOfLifeCommand>(context);
            }

            return;
        }

        private async Task ReadDocument()
        {
            if (settings.MergeInputFiles)
            {
                this.document = await this.documentMerger.Merge(this.settings.FileNames.ToArray());
            }
            else
            {
                this.document = await this.documentReader.ReadDocument(this.InputFileName);
            }

            this.settings.ArticleSchemaType = this.document.SchemaType;
            this.document.SchemaType = this.settings.ArticleSchemaType;

            await this.documentNormalizer.NormalizeToSystem(this.document);
        }

        private void SetUpConfigParameters()
        {
            this.settings.OutputFileName = this.OutputFileName;
        }

        private Task WriteOutputFile() => InvokeProcessor(
            Messages.WriteOutputFileMessage,
            () =>
            {
                return this.documentNormalizer.NormalizeToDocumentSchema(this.document)
                    .ContinueWith(
                        __ =>
                        {
                            __.Wait();
                            var o = this.documentWriter.WriteDocument(this.OutputFileName, this.document).Result;
                        });
            },
            this.logger);
    }
}
