namespace ProcessingTools.MainProgram
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Xml;

    using BaseLibrary;
    using BaseLibrary.Abbreviations;
    using BaseLibrary.Coordinates;
    using BaseLibrary.Dates;
    using BaseLibrary.Floats;
    using BaseLibrary.HyperLinks;
    using BaseLibrary.Measurements;
    using BaseLibrary.References;
    using BaseLibrary.Taxonomy;
    using BaseLibrary.ZooBank;
    using Bio.Environments.Data;
    using Bio.Environments.Data.Models;
    using Bio.Environments.Data.Repositories;
    using Bio.Environments.Services.Data;
    using Bio.Harvesters;
    using Bio.ServiceClient.ExtractHcmr;
    using Bio.Taxonomy.Contracts;
    using Bio.Taxonomy.Harvesters;
    using Bio.Taxonomy.ServiceClient.CatalogueOfLife;
    using Bio.Taxonomy.ServiceClient.Gbif;
    using Bio.Taxonomy.Services.Data;
    using Bio.Taxonomy.Types;
    using Contracts.Log;
    using DocumentProvider;
    using Extensions;

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

        public override void Run()
        {
            try
            {
                this.ConfigureFileProcessor();

                this.SetUpConfigParameters();

                this.ReadDocument();

                if (this.settings.ZoobankCloneXml)
                {
                    this.ZooBankCloneXml();
                }
                else if (this.settings.ZoobankCloneJson)
                {
                    this.ZooBankCloneJson();
                }
                else if (this.settings.ZoobankGenerateRegistrationXml)
                {
                    this.ZooBankGenerateRegistrationXml();
                }
                else if (this.settings.QueryReplace && this.settings.QueryFileName != null && this.settings.QueryFileName.Length > 0)
                {
                    this.document.Xml = QueryReplace.Replace(this.settings.Config, this.document.Xml, this.settings.QueryFileName);
                }
                else
                {
                    if (this.settings.RunXslTransform)
                    {
                        this.RunCustomXslTransform();
                    }

                    if (this.settings.InitialFormat)
                    {
                        this.InitialFormat();
                    }

                    if (this.settings.ParseReferences)
                    {
                        this.ParseReferences();
                    }

                    if (this.settings.TagDoi)
                    {
                        this.TagDoi();
                    }

                    if (this.settings.TagWebLinks)
                    {
                        this.TagWebLinks();
                    }

                    if (this.settings.ResolveMediaTypes)
                    {
                        this.ResolveMediaTypes();
                    }

                    if (this.settings.TagCoordinates)
                    {
                        this.TagCoordinates();
                    }

                    if (this.settings.ParseCoordinates)
                    {
                        this.ParseCoordinates();
                    }

                    if (this.settings.TagEnvo)
                    {
                        this.TagEnvo();
                    }

                    // Tag envo terms using envornment database
                    if (this.settings.TagEnvironments)
                    {
                        this.TagEnvoTerms();
                    }

                    if (this.settings.TagQuantities)
                    {
                        this.TagQuantities();
                    }

                    if (this.settings.TagDates)
                    {
                        this.TagDates();
                    }

                    if (this.settings.TagAbbreviations)
                    {
                        this.TagAbbreviations();
                    }

                    // Tag institutions, institutional codes, and specimen codes
                    if (this.settings.TagCodes)
                    {
                        this.TagCodes();
                    }

                    // Do something as an experimental feature
                    if (this.settings.TestFlag)
                    {
                        this.logger?.Log("\n\n\tTest\n\n");
                        var test = new Test(this.document.Xml);

                        test.MoveAuthorityTaxonNamePartToTaxonAuthorityTagInTaxPubTpNomenclaure();

                        this.document.Xml = test.Xml;
                    }

                    // Main Tagging part of the program
                    if (this.settings.ParseBySection)
                    {
                        var xmlDocument = new XmlDocument(this.document.NamespaceManager.NameTable)
                        {
                            PreserveWhitespace = true
                        };

                        try
                        {
                            xmlDocument.LoadXml(this.document.Xml);
                        }
                        catch
                        {
                            throw;
                        }

                        try
                        {
                            foreach (XmlNode node in xmlDocument.SelectNodes(this.settings.HigherStructrureXpath, this.document.NamespaceManager))
                            {
                                var fragment = node.OwnerDocument.CreateDocumentFragment();
                                fragment.InnerXml = this.MainProcessing(node.OuterXml);
                                node.ParentNode.ReplaceChild(fragment, node);
                            }

                            this.document.Xml = xmlDocument.OuterXml;
                        }
                        catch
                        {
                            throw;
                        }
                    }
                    else
                    {
                        this.document.Xml = this.MainProcessing(this.document.Xml);
                    }
                }

                this.WriteOutputFile();
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
                throw;
            }
        }

        protected override void InvokeProcessor(string message, Action action)
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
        }

        private void ValidateTaxa(string xmlContent)
        {
            var validator = new TaxonomicNamesValidator(this.settings.Config, xmlContent, this.logger);
            this.InvokeProcessor(Messages.ValidateTaxaUsingGnrMessage, validator);
        }

        private string ExpandTaxa(string xmlContent)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log(Messages.ExpandTaxa);

            try
            {
                var expand = new BaseLibrary.Taxonomy.Nlm.Expander(this.settings.Config, xmlContent, this.logger);
                var exp = new Expander(this.settings.Config, xmlContent, this.logger);

                for (int i = 0; i < TaggerStartup.NumberOfExpandingIterations; ++i)
                {
                    if (this.settings.ExpandLowerTaxa)
                    {
                        exp.Xml = expand.Xml;
                        exp.StableExpand();
                        expand.Xml = exp.Xml;
                    }

                    if (this.settings.Flag1)
                    {
                        expand.UnstableExpand1();
                    }

                    if (this.settings.Flag2)
                    {
                        expand.UnstableExpand2();
                    }

                    if (this.settings.Flag3)
                    {
                        exp.Xml = expand.Xml;
                        exp.UnstableExpand3();
                        expand.Xml = exp.Xml;
                    }

                    if (this.settings.Flag4)
                    {
                        expand.UnstableExpand4();
                    }

                    if (this.settings.Flag5)
                    {
                        expand.UnstableExpand5();
                    }

                    if (this.settings.Flag6)
                    {
                        expand.UnstableExpand6();
                    }

                    if (this.settings.Flag7)
                    {
                        expand.UnstableExpand7();
                    }

                    if (this.settings.Flag8)
                    {
                        exp.Xml = expand.Xml;
                        exp.UnstableExpand8();
                        expand.Xml = exp.Xml;
                    }

                    xmlContent = expand.Xml;
                    this.PrintElapsedTime(timer);
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            return xmlContent;
        }

        private void ExtractTaxa(string xmlContent)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlContent);

                if (this.settings.ExtractTaxa)
                {
                    this.logger?.Log(Messages.ExtractAllTaxaMessage);
                    xmlDocument
                        .ExtractTaxa(true)
                        .ToList()
                        .ForEach(t => this.logger?.Log(t));
                    return;
                }

                if (this.settings.ExtractLowerTaxa)
                {
                    this.logger?.Log(Messages.ExtractLowerTaxaMessage);
                    xmlDocument
                        .ExtractTaxa(true, TaxaType.Lower)
                        .ToList()
                        .ForEach(t => this.logger?.Log(t));
                }

                if (this.settings.ExtractHigherTaxa)
                {
                    this.logger?.Log(Messages.ExtractHigherTaxaMessage);
                    xmlDocument
                        .ExtractTaxa(true, TaxaType.Higher)
                        .ToList()
                        .ForEach(t => this.logger?.Log(t));
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }
        }

        private void RunCustomXslTransform()
        {
            var processor = new CustomXslRunner(this.settings.QueryFileName, this.document.Xml);
            this.InvokeProcessor(Messages.RunCustomXslTransformMessage, processor);
            this.document.Xml = processor.Xml;
        }

        private string FormatTreatments(string xmlContent)
        {
            var formatter = new TreatmentFormatter(this.settings.Config, xmlContent, this.logger);
            this.InvokeProcessor(Messages.FormatTreatmentsMessage, formatter);
            return formatter.Xml;
        }

        private void InitialFormat()
        {
            switch (this.settings.Config.ArticleSchemaType)
            {
                case SchemaType.Nlm:
                    {
                        string xml = this.document.Xml.ApplyXslTransform(this.settings.Config.NlmInitialFormatXslPath);
                        var formatter = new BaseLibrary.Format.Nlm.Formatter(this.settings.Config, xml);
                        this.InvokeProcessor(Messages.InitialFormatMessage, formatter);
                        this.document.Xml = formatter.Xml;
                    }

                    break;

                default:
                    {
                        string xml = this.document.Xml.ApplyXslTransform(this.settings.Config.SystemInitialFormatXslPath);
                        var formatter = new BaseLibrary.Format.NlmSystem.Formatter(this.settings.Config, xml);
                        this.InvokeProcessor(Messages.InitialFormatMessage, formatter);
                        this.document.Xml = formatter.Xml;
                    }

                    break;
            }
        }

        private string MainProcessing(string xml)
        {
            string xmlContent = xml;

            if (this.settings.TagFloats)
            {
                xmlContent = this.TagFloats(xmlContent);
            }

            if (this.settings.TagTableFn)
            {
                xmlContent = this.TagTableFootnote(xmlContent);
            }

            if (this.settings.TagLowerTaxa || this.settings.TagHigherTaxa)
            {
                var blackList = new XmlListDataService(this.settings.Config.BlackListXmlFilePath);
                var whiteList = new XmlListDataService(this.settings.Config.WhiteListXmlFilePath);

                if (this.settings.TagLowerTaxa)
                {
                    xmlContent = this.TagLowerTaxa(xmlContent, blackList);
                }

                if (this.settings.TagHigherTaxa)
                {
                    xmlContent = this.TagHigherTaxa(xmlContent, blackList, whiteList);
                }
            }

            xmlContent = this.ParseLowerTaxa(xmlContent);
            xmlContent = this.ParseHigherTaxa(xmlContent);

            if (this.settings.ExpandLowerTaxa || this.settings.Flag1 || this.settings.Flag2 || this.settings.Flag3 || this.settings.Flag4 || this.settings.Flag5 || this.settings.Flag6 || this.settings.Flag7 || this.settings.Flag8)
            {
                xmlContent = this.ExpandTaxa(xmlContent);
            }

            if (this.settings.ExtractTaxa || this.settings.ExtractLowerTaxa || this.settings.ExtractHigherTaxa)
            {
                this.ExtractTaxa(xmlContent);
            }

            if (this.settings.ValidateTaxa)
            {
                this.ValidateTaxa(xmlContent);
            }

            if (this.settings.UntagSplit)
            {
                xmlContent = this.RemoveAllTaxaTags(xmlContent);
            }

            if (this.settings.FormatTreat)
            {
                xmlContent = this.FormatTreatments(xmlContent);
            }

            xmlContent = this.ParseTreatmentMeta(xmlContent);

            if (this.settings.TagReferences)
            {
                xmlContent = this.TagReferences(xmlContent);
            }

            return xmlContent;
        }

        private void ParseCoordinates()
        {
            var parser = new CoordinatesParser(this.settings.Config, this.document.Xml, this.logger);
            this.InvokeProcessor(Messages.ParseCoordinatesMessage, parser);
            this.document.Xml = parser.Xml;
        }

        private void ResolveMediaTypes()
        {
            var context = MediaType.Data.MediaTypesDbContext.Create();

            var repository = new MediaType.Data.Repositories.MediaTypesGenericRepository<MediaType.Data.Models.FileExtension>(context);

            var mediatypeDataService = new MediaType.Services.Data.MediaTypeDataService(repository);

            var parser = new MediaTypesResolver(this.document.Xml, mediatypeDataService, this.logger);

            this.InvokeProcessor(Messages.ResolveMediaTypesMessage, parser);
            this.document.Xml = parser.Xml;
        }

        private string ParseHigherTaxa(string xmlContent)
        {
            string result = xmlContent;

            if (this.settings.ParseHigherTaxa)
            {
                {
                    var service = new LocalDbTaxaRankDataService(this.settings.Config.RankListXmlFilePath);
                    var parser = new HigherTaxaParserWithDataService<ITaxonRank>(result, service, this.logger);
                    this.InvokeProcessor(Messages.ParseHigherTaxaMessage, parser);
                    parser.XmlDocument.PrintNonParsedTaxa(this.logger);
                    result = parser.Xml;
                }

                if (this.settings.ParseHigherWithAphia)
                {
                    var service = new AphiaTaxaClassificationDataService();
                    var parser = new HigherTaxaParserWithDataService<ITaxonClassification>(result, service, this.logger);
                    this.InvokeProcessor(Messages.ParseHigherTaxaWithAphiaMessage, parser);
                    parser.XmlDocument.PrintNonParsedTaxa(this.logger);
                    result = parser.Xml;
                }

                if (this.settings.ParseHigherWithCoL)
                {
                    var requester = new CatalogueOfLifeDataRequester();
                    var service = new CatalogueOfLifeTaxaClassificationDataService(requester);
                    var parser = new HigherTaxaParserWithDataService<ITaxonClassification>(result, service, this.logger);
                    this.InvokeProcessor(Messages.ParseHigherTaxaWithCoLMessage, parser);
                    parser.XmlDocument.PrintNonParsedTaxa(this.logger);
                    result = parser.Xml;
                }

                if (this.settings.ParseHigherWithGbif)
                {
                    var requester = new GbifDataRequester();
                    var service = new GbifTaxaClassificationDataService(requester);
                    var parser = new HigherTaxaParserWithDataService<ITaxonClassification>(result, service, this.logger);
                    this.InvokeProcessor(Messages.ParseHigherTaxaWithGbifMessage, parser);
                    parser.XmlDocument.PrintNonParsedTaxa(this.logger);
                    result = parser.Xml;
                }

                if (this.settings.ParseHigherBySuffix)
                {
                    var service = new SuffixHigherTaxaRankDataService();
                    var parser = new HigherTaxaParserWithDataService<ITaxonRank>(result, service, this.logger);
                    this.InvokeProcessor(Messages.ParseHigherTaxaBySuffixMessage, parser);
                    parser.XmlDocument.PrintNonParsedTaxa(this.logger);
                    result = parser.Xml;
                }

                if (this.settings.ParseHigherAboveGenus)
                {
                    var service = new AboveGenusTaxaRankDataService();
                    var parser = new HigherTaxaParserWithDataService<ITaxonRank>(result, service, this.logger);
                    this.InvokeProcessor(Messages.ParseHigherTaxaAboveGenusMessage, parser);
                    parser.XmlDocument.PrintNonParsedTaxa(this.logger);
                    result = parser.Xml;
                }
            }

            return result;
        }

        private string ParseLowerTaxa(string xmlContent)
        {
            if (this.settings.ParseLowerTaxa)
            {
                var parser = new LowerTaxaParser(this.settings.Config, xmlContent, this.logger);
                this.InvokeProcessor(Messages.ParseLowerTaxaMessage, parser);
                xmlContent = parser.Xml;
            }

            return xmlContent;
        }

        private void ParseReferences()
        {
            var parser = new ReferencesTagger(this.settings.Config, this.document.Xml, this.logger);
            this.InvokeProcessor(Messages.ParseReferencesMessage, parser);
            this.document.Xml = parser.Xml;
        }

        private string ParseTreatmentMeta(string xmlContent)
        {
            string result = xmlContent;

            if (this.settings.ParseTreatmentMetaWithAphia)
            {
                var service = new AphiaTaxaClassificationDataService();
                var parser = new TreatmentMetaParser(service, result, this.logger);
                this.InvokeProcessor(Messages.ParseTreatmentMetaWithAphiaMessage, parser);
                result = parser.Xml;
            }

            if (this.settings.ParseTreatmentMetaWithGbif)
            {
                var requester = new GbifDataRequester();
                var service = new GbifTaxaClassificationDataService(requester);
                var parser = new TreatmentMetaParser(service, result, this.logger);
                this.InvokeProcessor(Messages.ParseTreatmentMetaWithGbifMessage, parser);
                result = parser.Xml;
            }

            if (this.settings.ParseTreatmentMetaWithCol)
            {
                var requester = new CatalogueOfLifeDataRequester();
                var service = new CatalogueOfLifeTaxaClassificationDataService(requester);
                var parser = new TreatmentMetaParser(service, result, this.logger);
                this.InvokeProcessor(Messages.ParseTreatmentMetaWithCoLMessage, parser);
                result = parser.Xml;
            }

            return result;
        }

        private void PrintElapsedTime(Stopwatch timer)
        {
            this.logger?.Log(LogType.Info, Messages.ElapsedTimeMessageFormat, timer.Elapsed);
        }

        private string RemoveAllTaxaTags(string xmlContent)
        {
            try
            {
                var parser = new LowerTaxaParser(this.settings.Config, xmlContent, this.logger);
                parser.XmlDocument.RemoveTaxonNamePartTags();
                xmlContent = parser.Xml;
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            return xmlContent;
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
            string outputFileDirectory = Path.GetDirectoryName(this.fileProcessor.OutputFileName);
            string outputFileName = Path.GetFileNameWithoutExtension(this.fileProcessor.OutputFileName);

            this.settings.Config.EnvoResponseOutputXmlFileName = $"{tempDirectory}\\envo-{outputFileName}.xml";
            this.settings.Config.GnrOutputFileName = $"{tempDirectory}\\gnr-{outputFileName}.xml";
            this.settings.Config.ReferencesGetReferencesXmlPath = $"{outputFileDirectory}\\zzz-{outputFileName}-references.xml";
            this.settings.Config.ReferencesTagTemplateXmlPath = $"{tempDirectory}\\zzz-{outputFileName}-references-tag-template.xml";
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

        private void TagAbbreviations()
        {
            var tagger = new AbbreviationsTagger(this.settings.Config, this.document.Xml);
            this.InvokeProcessor(Messages.TagAbbreviationsMessage, tagger);
            this.document.Xml = tagger.Xml;
        }

        // TODO
        private void TagCodes()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log(Messages.TagCodesMessage);

            try
            {
                var xpathProvider = new XPathProvider(this.settings.Config);
                var dataProvider = new DataProvider(this.settings.Config, this.document.Xml, this.logger);

                try
                {
                    Codes codes = new Codes(this.settings.Config, this.document.Xml, this.logger);
                    codes.TagInstitutions(xpathProvider, dataProvider);
                    codes.TagInstitutionalCodes(xpathProvider, dataProvider);

                    this.document.Xml = codes.Xml;
                }
                catch
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            this.PrintElapsedTime(timer);
        }

        private void TagCoordinates()
        {
            var tagger = new CoordinatesTagger(this.settings.Config, this.document.Xml, this.logger);
            this.InvokeProcessor(Messages.TagCoordinatesMessage, tagger);
            this.document.Xml = tagger.Xml;
        }

        private void TagDates()
        {
            var tagger = new DatesTagger(this.settings.Config, this.document.Xml, this.logger);
            this.InvokeProcessor(Messages.TagDatesMessage, tagger);
            this.document.Xml = tagger.Xml;
        }

        private void TagDoi()
        {
            var tagger = new DoiLinksTagger(this.settings.Config, this.document.Xml);
            this.InvokeProcessor(Messages.TagDoiMessage, tagger);
            this.document.Xml = tagger.Xml;
        }

        private void TagEnvo()
        {
            var requester = new ExtractHcmrDataRequester();
            var harvester = new ExtractHcmrHarvester(requester);
            var tagger = new Envo(this.settings.Config, this.document.Xml, harvester, this.logger);
            this.InvokeProcessor(Messages.TagEnvironmentsMessage, tagger);
            this.document.Xml = tagger.Xml;
        }

        private void TagEnvoTerms()
        {
            var context = new BioEnvironmentsDbContext();
            var repository = new BioEnvironmentsGenericRepository<EnvoName>(context);
            var service = new EnvoTermsDataService(repository);
            var harvester = new EnvoTermsHarvester(service);
            var tagger = new Environments(this.settings.Config, this.document.Xml, harvester);
            this.InvokeProcessor(Messages.TagEnvoTermsMessage, tagger);
            this.document.Xml = tagger.Xml;
        }

        private string TagFloats(string xmlContent)
        {
            var tagger = new FloatsTagger(this.settings.Config, xmlContent, this.logger);
            this.InvokeProcessor(Messages.TagFloatsMessage, tagger);
            return tagger.Xml;
        }

        private string TagHigherTaxa(string xmlContent, XmlListDataService blackList, XmlListDataService whiteList)
        {
            var harvester = new HigherTaxaHarvester(whiteList);
            var tagger = new HigherTaxaTagger(this.settings.Config, xmlContent, harvester, blackList, this.logger);
            this.InvokeProcessor(Messages.TagHigherTaxaMessage, tagger);
            return tagger.Xml.NormalizeXmlToSystemXml(this.settings.Config);
        }

        private string TagLowerTaxa(string xmlContent, XmlListDataService blackList)
        {
            var tagger = new LowerTaxaTagger(this.settings.Config, xmlContent, blackList, this.logger);
            this.InvokeProcessor(Messages.TagLowerTaxaMessage, tagger);
            return tagger.Xml.NormalizeXmlToSystemXml(this.settings.Config);
        }

        // TODO
        private void TagQuantities()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log(Messages.TagQuantitiesMessage);

            try
            {
                var xpathProvider = new XPathProvider(this.settings.Config);
                var quantitiesTagger = new QuantitiesTagger(this.settings.Config, this.document.Xml, this.logger);

                quantitiesTagger.TagQuantities(xpathProvider);
                quantitiesTagger.TagDeviation(xpathProvider);
                quantitiesTagger.TagAltitude(xpathProvider);

                this.document.Xml = quantitiesTagger.Xml;
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            this.PrintElapsedTime(timer);
        }

        private string TagReferences(string xmlContent)
        {
            var tagger = new ReferencesTagger(this.settings.Config, xmlContent, this.logger);
            this.InvokeProcessor(Messages.TagReferencesMessage, tagger);
            return tagger.Xml;
        }

        private string TagTableFootnote(string xmlContent)
        {
            var tagger = new TableFootNotesTagger(this.settings.Config, xmlContent, this.logger);
            this.InvokeProcessor(Messages.TagTableFootNotesMessage, tagger);
            return tagger.Xml;
        }

        private void TagWebLinks()
        {
            var tagger = new UrlLinksTagger(this.settings.Config, this.document.Xml);
            this.InvokeProcessor(Messages.TagWebLinksMessage, tagger);
            this.document.Xml = tagger.Xml;
        }

        private void WriteOutputFile()
        {
            this.InvokeProcessor(
                Messages.WriteOutputFileMessage,
                () =>
                {
                    this.document.Xml = this.document.Xml.NormalizeXmlToCurrentXml(this.settings.Config);
                    this.fileProcessor.Write(this.document);
                });
        }

        private void ZooBankCloneJson()
        {
            string jsonStringContent = File.ReadAllText(this.settings.QueryFileName);
            var cloner = new ZoobankJsonCloner(jsonStringContent, this.document.Xml, this.logger);
            this.InvokeProcessor(Messages.CloneZooBankJsonMessage, cloner);
            this.document.Xml = cloner.Xml;
        }

        private void ZooBankCloneXml()
        {
            var nlmDocument = new TaxPubDocument();
            var fileProcessorNlm = new XmlFileProcessor(this.settings.QueryFileName, this.settings.OutputFileName);
            fileProcessorNlm.Read(nlmDocument);

            var cloner = new ZoobankXmlCloner(nlmDocument.Xml, this.document.Xml, this.logger);
            this.InvokeProcessor(Messages.CloneZooBankXmlMessage, cloner);
            this.document.Xml = cloner.Xml;
        }

        private void ZooBankGenerateRegistrationXml()
        {
            var generator = new ZoobankRegistrationXmlGenerator(this.settings.Config, this.document.Xml);
            this.InvokeProcessor(Messages.GenerateRegistrationXmlForZooBankMessage, generator);
            this.document.Xml = generator.Xml;
        }
    }
}