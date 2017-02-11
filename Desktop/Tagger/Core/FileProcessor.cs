namespace ProcessingTools.Tagger.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts;
    using ProcessingTools.Constants;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Files.Generators;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts.Processors.Documents;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Xml.Contracts.Wrappers;

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
            if (fileNameGenerator == null)
            {
                throw new ArgumentNullException(nameof(fileNameGenerator));
            }

            if (documentWrapper == null)
            {
                throw new ArgumentNullException(nameof(documentWrapper));
            }

            if (documentManager == null)
            {
                throw new ArgumentNullException(nameof(documentManager));
            }

            if (commandFactory == null)
            {
                throw new ArgumentNullException(nameof(commandFactory));
            }

            this.fileNameGenerator = fileNameGenerator;
            this.documentWrapper = documentWrapper;
            this.documentManager = documentManager;
            this.commandFactory = commandFactory;
            this.logger = logger;

            this.tasks = new ConcurrentQueue<Task>();
        }

        public async Task Run(IProgramSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            this.settings = settings;

            try
            {
                this.settings.OutputFileName = await this.GetOutputFileName();

                IDocument document;

                try
                {
                    document = await this.documentManager.Read(
                        this.settings.MergeInputFiles,
                        this.settings.FileNames.ToArray());
                }
                catch
                {
                    this.logger?.Log(LogType.Error, "One or more input files cannot be read.");
                    return;
                }

                settings.ArticleSchemaType = document.SchemaType;
                document.SchemaType = settings.ArticleSchemaType;

                await this.ProcessDocument(document.XmlDocument.DocumentElement);

                this.tasks.Enqueue(this.WriteOutputFile(document));

                await Task.WhenAll(this.tasks.ToArray());
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
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
                outputFileName = await this.fileNameGenerator.Generate(
                    Path.Combine(Path.GetDirectoryName(inputFileName), FileConstants.DefaultBundleXmlFileName),
                    FileConstants.MaximalLengthOfGeneratedNewFileName,
                    true);
            }
            else
            {
                outputFileName = numberOfFileNames > 1 ?
                    this.settings.FileNames[1] :
                    await this.fileNameGenerator.Generate(
                        inputFileName,
                        FileConstants.MaximalLengthOfGeneratedNewFileName,
                        true);

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

            // Contextual processing
            var subcontextNodeList = context.SelectNodes(XPathStrings.HigherDocumentStructure);
            if (subcontextNodeList.Count < 1)
            {
                await this.ContextProcessing(context);
            }
            else
            {
                foreach (XmlNode subcontext in subcontextNodeList)
                {
                    await this.ContextProcessing(subcontext);
                }
            }

            if (this.settings.ExtractTaxa || this.settings.ExtractLowerTaxa || this.settings.ExtractHigherTaxa)
            {
                await this.InvokeProcessor<IExtractTaxaCommand>(context);
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

        private Task WriteOutputFile(IDocument document) => InvokeProcessor(
            Messages.WriteOutputFileMessage,
            () => this.documentManager.Write(
                this.settings.OutputFileName,
                document,
                this.settings.SplitDocument),
            this.logger);
    }
}
