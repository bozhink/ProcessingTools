namespace ProcessingTools.Tagger.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts.Documents;
    using ProcessingTools.Tagger.Contracts;

    public partial class FileProcessor : IFileProcessor
    {
        private readonly Func<Type, ITaggerCommand> commandFactory;
        private readonly IDocumentWrapper documentWrapper;
        private readonly IDocumentManager documentManager;
        private readonly IFileNameGenerator fileNameGenerator;
        private readonly ILogger logger;

        private IProgramSettings settings;
        private ConcurrentQueue<Task> tasks;

        public FileProcessor(
            IFileNameGenerator fileNameGenerator,
            IDocumentWrapper documentWrapper,
            IDocumentManager documentManager,
            Func<Type, ITaggerCommand> commandFactory,
            ILogger logger)
        {
            this.fileNameGenerator = fileNameGenerator ?? throw new ArgumentNullException(nameof(fileNameGenerator));
            this.documentWrapper = documentWrapper ?? throw new ArgumentNullException(nameof(documentWrapper));
            this.documentManager = documentManager ?? throw new ArgumentNullException(nameof(documentManager));
            this.commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));
            this.logger = logger;

            this.tasks = new ConcurrentQueue<Task>();
        }

        public async Task RunAsync(IProgramSettings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));

            try
            {
                this.settings.OutputFileName = await this.GetOutputFileName().ConfigureAwait(false);

                IDocument document;

                try
                {
                    document = await this.documentManager.ReadAsync(
                        this.settings.MergeInputFiles,
                        this.settings.FileNames.ToArray())
                        .ConfigureAwait(false);
                }
                catch
                {
                    this.logger?.Log(LogType.Error, message: "One or more input files cannot be read.");
                    return;
                }

                settings.ArticleSchemaType = document.SchemaType;
                document.SchemaType = settings.ArticleSchemaType;

                await this.ProcessDocument(document.XmlDocument.DocumentElement).ConfigureAwait(false);

                this.tasks.Enqueue(this.WriteOutputFile(document));

                await Task.WhenAll(this.tasks.ToArray()).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.logger?.Log(e, message: string.Empty);
                throw;
            }
        }

        private async Task<string> GetOutputFileName()
        {
            int numberOfFileNames = this.settings.FileNames.Count;

            if (numberOfFileNames < 1)
            {
                throw new InvalidOperationException("The name of the input file is required");
            }

            string inputFileName = this.settings.FileNames[0];
            string outputFileName = null;

            string inputFileNameMessage = "*";
            string outputFileNameMessage = "*";

            if (this.settings.MergeInputFiles)
            {
                outputFileName = await this.fileNameGenerator.GenerateAsync(
                    Path.Combine(Path.GetDirectoryName(inputFileName), FileConstants.DefaultBundleXmlFileName),
                    FileConstants.MaximalLengthOfGeneratedNewFileName,
                    true)
                    .ConfigureAwait(false);
            }
            else
            {
                outputFileName = numberOfFileNames > 1 ?
                    this.settings.FileNames[1] :
                    await this.fileNameGenerator.GenerateAsync(
                        inputFileName,
                        FileConstants.MaximalLengthOfGeneratedNewFileName,
                        true)
                        .ConfigureAwait(false);

                inputFileNameMessage = inputFileName;
            }

            if (!this.settings.SplitDocument)
            {
                outputFileNameMessage = outputFileName;
            }

            this.logger?.Log(
                Messages.InputOutputFileNamesMessageFormat,
                inputFileNameMessage,
                outputFileNameMessage);

            return outputFileName;
        }

        private async Task ContextProcessing(XmlNode context)
        {
            if (this.settings.TagFloats)
            {
                await this.InvokeProcessor<ITagFloatsCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.TagReferences)
            {
                await this.InvokeProcessor<ITagReferencesCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.ExpandLowerTaxa)
            {
                for (int i = 0; i < ProcessingConstants.NumberOfExpandIterations; ++i)
                {
                    await this.InvokeProcessor<IExpandLowerTaxaCommand>(context).ConfigureAwait(false);
                }
            }
        }

        private async Task ProcessDocument(XmlNode context)
        {
            if (this.settings.ZoobankCloneXml)
            {
                await this.InvokeProcessor<IZooBankCloneXmlCommand>(context).ConfigureAwait(false);
                return;
            }

            if (this.settings.ZoobankCloneJson)
            {
                await this.InvokeProcessor<IZooBankCloneJsonCommand>(context).ConfigureAwait(false);
                return;
            }

            if (this.settings.ZoobankGenerateRegistrationXml)
            {
                await this.InvokeProcessor<IZooBankGenerateRegistrationXmlCommand>(context).ConfigureAwait(false);
                return;
            }

            if (this.settings.QueryReplace)
            {
                await this.InvokeProcessor<IQueryReplaceCommand>(context).ConfigureAwait(false);
                return;
            }

            if (this.settings.RunXslTransform)
            {
                await this.InvokeProcessor<IRunCustomXslTransformCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.InitialFormat)
            {
                await this.InvokeProcessor<IInitialFormatCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.ParseReferences)
            {
                await this.InvokeProcessor<IParseReferencesCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.TagDoi || this.settings.TagWebLinks)
            {
                await this.InvokeProcessor<ITagWebLinksCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.ResolveMediaTypes)
            {
                await this.InvokeProcessor<IResolveMediaTypesCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.TagTableFn)
            {
                await this.InvokeProcessor<ITagTableFootnoteCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.TagCoordinates)
            {
                await this.InvokeProcessor<ITagCoordinatesCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.ParseCoordinates)
            {
                await this.InvokeProcessor<IParseCoordinatesCommand>(context).ConfigureAwait(false);
            }

            foreach (var commandType in this.settings.CalledCommands)
            {
                await this.InvokeProcessor(commandType, context).ConfigureAwait(false);
            }

            if (this.settings.TagEnvironmentTermsWithExtract)
            {
                await this.InvokeProcessor<ITagEnvironmentTermsWithExtractCommand>(context).ConfigureAwait(false);
            }

            // Tag envo terms using environment database
            if (this.settings.TagEnvironmentTerms)
            {
                await this.InvokeProcessor<ITagEnvironmentTermsCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.TagAbbreviations)
            {
                await this.InvokeProcessor<ITagAbbreviationsCommand>(context).ConfigureAwait(false);
            }

            // Tag institutions, institutional codes, and specimen codes
            if (this.settings.TagCodes)
            {
                await this.InvokeProcessor<ITagInstitutionalCodesCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.TagLowerTaxa)
            {
                await this.InvokeProcessor<ITagLowerTaxaCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.TagHigherTaxa)
            {
                await this.InvokeProcessor<ITagHigherTaxaCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.ParseLowerTaxa)
            {
                await this.InvokeProcessor<IParseLowerTaxaCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.ParseHigherTaxa)
            {
                await this.InvokeProcessor<IParseHigherTaxaWithLocalDbCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.ParseHigherWithAphia)
            {
                await this.InvokeProcessor<IParseHigherTaxaWithAphiaCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.ParseHigherWithCoL)
            {
                await this.InvokeProcessor<IParseHigherTaxaWithCatalogueOfLifeCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.ParseHigherWithGbif)
            {
                await this.InvokeProcessor<IParseHigherTaxaWithGbifCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.ParseHigherBySuffix)
            {
                await this.InvokeProcessor<IParseHigherTaxaBySuffixCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.ParseHigherAboveGenus)
            {
                await this.InvokeProcessor<IParseHigherTaxaAboveGenusCommand>(context).ConfigureAwait(false);
            }

            // Contextual processing
            var subcontextNodeList = context.SelectNodes(XPathStrings.HigherDocumentStructure);
            if (subcontextNodeList.Count < 1)
            {
                await this.ContextProcessing(context).ConfigureAwait(false);
            }
            else
            {
                foreach (XmlNode subcontext in subcontextNodeList)
                {
                    await this.ContextProcessing(subcontext).ConfigureAwait(false);
                }
            }

            if (this.settings.ExtractTaxa || this.settings.ExtractLowerTaxa || this.settings.ExtractHigherTaxa)
            {
                await this.InvokeProcessor<IExtractTaxaCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.UntagSplit)
            {
                await this.InvokeProcessor<IRemoveAllTaxonNamePartTagsCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.FormatTreat)
            {
                await this.InvokeProcessor<IFormatTreatmentsCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.ParseTreatmentMetaWithAphia)
            {
                await this.InvokeProcessor<IParseTreatmentMetaWithAphiaCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.ParseTreatmentMetaWithGbif)
            {
                await this.InvokeProcessor<IParseTreatmentMetaWithGbifCommand>(context).ConfigureAwait(false);
            }

            if (this.settings.ParseTreatmentMetaWithCol)
            {
                await this.InvokeProcessor<IParseTreatmentMetaWithCatalogueOfLifeCommand>(context).ConfigureAwait(false);
            }

            return;
        }

        private Task WriteOutputFile(IDocument document) => InvokeProcessor(
            Messages.WriteOutputFileMessage,
            () => this.documentManager.WriteAsync(
                this.settings.OutputFileName,
                document,
                this.settings.SplitDocument),
            this.logger);
    }
}
