namespace ProcessingTools.MainProgram
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Xml;

    using Attributes;
    using BaseLibrary;
    using BaseLibrary.Abbreviations;
    using BaseLibrary.Coordinates;
    using BaseLibrary.Floats;
    using BaseLibrary.Format;
    using BaseLibrary.References;
    using BaseLibrary.Taxonomy;
    using BaseLibrary.ZooBank;
    using Bio.Taxonomy.Types;
    using Common.Constants;
    using Contracts.Log;
    using DocumentProvider;
    using Extensions;
    using Models;

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

                    if (this.settings.TagDoi || this.settings.TagWebLinks)
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

                    if (this.settings.TagMorphologicalEpithets)
                    {
                        this.TagMorphologicalEpithets();
                    }

                    if (this.settings.TagGeoNames)
                    {
                        this.TagGeoNames();
                    }

                    if (this.settings.TagGeoEpithets)
                    {
                        this.TagGeoEpithets();
                    }

                    if (this.settings.TagInstitutions)
                    {
                        this.TagInstitutions();
                    }

                    if (this.settings.TagProducts)
                    {
                        this.TagProducts();
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

                for (int i = 0; i < ProcessingConstants.NumberOfExpandingIterations; ++i)
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
                        string xml = this.document.Xml.ApplyXslTransform(this.settings.Config.NlmInitialFormatXslTransform);
                        var formatter = new NlmInitialFormatter(this.settings.Config, xml);
                        this.InvokeProcessor(Messages.InitialFormatMessage, formatter);
                        this.document.Xml = formatter.Xml;
                    }

                    break;

                default:
                    {
                        string xml = this.document.Xml.ApplyXslTransform(this.settings.Config.SystemInitialFormatXslTransform);
                        var formatter = new SystemInitialFormatter(this.settings.Config, xml);
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
                var blackList = new Bio.Taxonomy.Services.Data.XmlListDataService(this.settings.Config.BlackListXmlFilePath);
                var whiteList = new Bio.Taxonomy.Services.Data.XmlListDataService(this.settings.Config.WhiteListXmlFilePath);

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
                    var service = new Bio.Taxonomy.Services.Data.LocalDbTaxaRankDataService(this.settings.Config.RankListXmlFilePath);
                    var parser = new HigherTaxaParserWithDataService<Bio.Taxonomy.Contracts.ITaxonRank>(result, service, this.logger);
                    this.InvokeProcessor(Messages.ParseHigherTaxaMessage, parser);
                    parser.XmlDocument.PrintNonParsedTaxa(this.logger);
                    result = parser.Xml;
                }

                if (this.settings.ParseHigherWithAphia)
                {
                    var service = new Bio.Taxonomy.Services.Data.AphiaTaxaClassificationDataService();
                    var parser = new HigherTaxaParserWithDataService<Bio.Taxonomy.Contracts.ITaxonClassification>(result, service, this.logger);
                    this.InvokeProcessor(Messages.ParseHigherTaxaWithAphiaMessage, parser);
                    parser.XmlDocument.PrintNonParsedTaxa(this.logger);
                    result = parser.Xml;
                }

                if (this.settings.ParseHigherWithCoL)
                {
                    var requester = new Bio.Taxonomy.ServiceClient.CatalogueOfLife.CatalogueOfLifeDataRequester();
                    var service = new Bio.Taxonomy.Services.Data.CatalogueOfLifeTaxaClassificationDataService(requester);
                    var parser = new HigherTaxaParserWithDataService<Bio.Taxonomy.Contracts.ITaxonClassification>(result, service, this.logger);
                    this.InvokeProcessor(Messages.ParseHigherTaxaWithCoLMessage, parser);
                    parser.XmlDocument.PrintNonParsedTaxa(this.logger);
                    result = parser.Xml;
                }

                if (this.settings.ParseHigherWithGbif)
                {
                    var requester = new Bio.Taxonomy.ServiceClient.Gbif.GbifDataRequester();
                    var service = new Bio.Taxonomy.Services.Data.GbifTaxaClassificationDataService(requester);
                    var parser = new HigherTaxaParserWithDataService<Bio.Taxonomy.Contracts.ITaxonClassification>(result, service, this.logger);
                    this.InvokeProcessor(Messages.ParseHigherTaxaWithGbifMessage, parser);
                    parser.XmlDocument.PrintNonParsedTaxa(this.logger);
                    result = parser.Xml;
                }

                if (this.settings.ParseHigherBySuffix)
                {
                    var service = new Bio.Taxonomy.Services.Data.SuffixHigherTaxaRankDataService();
                    var parser = new HigherTaxaParserWithDataService<Bio.Taxonomy.Contracts.ITaxonRank>(result, service, this.logger);
                    this.InvokeProcessor(Messages.ParseHigherTaxaBySuffixMessage, parser);
                    parser.XmlDocument.PrintNonParsedTaxa(this.logger);
                    result = parser.Xml;
                }

                if (this.settings.ParseHigherAboveGenus)
                {
                    var service = new Bio.Taxonomy.Services.Data.AboveGenusTaxaRankDataService();
                    var parser = new HigherTaxaParserWithDataService<Bio.Taxonomy.Contracts.ITaxonRank>(result, service, this.logger);
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
                var service = new Bio.Taxonomy.Services.Data.AphiaTaxaClassificationDataService();
                var parser = new TreatmentMetaParser(service, result, this.logger);
                this.InvokeProcessor(Messages.ParseTreatmentMetaWithAphiaMessage, parser);
                result = parser.Xml;
            }

            if (this.settings.ParseTreatmentMetaWithGbif)
            {
                var requester = new Bio.Taxonomy.ServiceClient.Gbif.GbifDataRequester();
                var service = new Bio.Taxonomy.Services.Data.GbifTaxaClassificationDataService(requester);
                var parser = new TreatmentMetaParser(service, result, this.logger);
                this.InvokeProcessor(Messages.ParseTreatmentMetaWithGbifMessage, parser);
                result = parser.Xml;
            }

            if (this.settings.ParseTreatmentMetaWithCol)
            {
                var requester = new Bio.Taxonomy.ServiceClient.CatalogueOfLife.CatalogueOfLifeDataRequester();
                var service = new Bio.Taxonomy.Services.Data.CatalogueOfLifeTaxaClassificationDataService(requester);
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
            var harvester = new Harvesters.DatesHarvester();

            XmlElement tagModel = this.document.XmlDocument.CreateElement("named-content");
            tagModel.SetAttribute("content-type", "date");

            var xpathProvider = new XPathProvider(this.settings.Config);

            var harvestableDocument = new HarvestableDocument(this.settings.Config, this.document.Xml);
            var data = harvester.Harvest(harvestableDocument.TextContent).Result;

            var tagger = new StringTagger(this.document.Xml, data, tagModel, xpathProvider.SelectContentNodesXPathTemplate, this.logger);
            this.InvokeProcessor(Messages.TagDatesMessage, tagger);
            this.document.Xml = tagger.Xml;
        }

        private void TagEnvo()
        {
            var requester = new Bio.ServiceClient.ExtractHcmr.ExtractHcmrDataRequester();

            var harvestableDocument = new HarvestableDocument(this.settings.Config, this.document.Xml);
            var harvester = new Bio.Harvesters.ExtractHcmrHarvester(requester);
            var data = harvester.Harvest(harvestableDocument.TextContent).Result
                .Select(t => new EnvoExtractHcmrSerializableModel
                {
                    Value = t.Content,
                    Type = string.Join("|", t.Types),
                    Identifier = string.Join("|", t.Identifiers)
                });

            var tagger = new SimpleXmlSerializableObjectTagger<EnvoExtractHcmrSerializableModel>(this.document.Xml, data, "/*", this.document.NamespaceManager, false, true, this.logger);

            this.InvokeProcessor(Messages.TagEnvironmentsMessage, tagger);
            this.document.Xml = tagger.Xml;
        }

        private void TagEnvoTerms()
        {
            var context = new Bio.Environments.Data.BioEnvironmentsDbContext();
            var repository = new Bio.Environments.Data.Repositories.BioEnvironmentsGenericRepository<Bio.Environments.Data.Models.EnvoName>(context);
            var service = new Bio.Environments.Services.Data.EnvoTermsDataService(repository);

            var harvestableDocument = new HarvestableDocument(this.settings.Config, this.document.Xml);
            var harvester = new Bio.Harvesters.EnvoTermsHarvester(service);
            var data = harvester.Harvest(harvestableDocument.TextContent).Result
                .Select(t => new EnvoTermResponseModel
                {
                    EntityId = t.EntityId,
                    EnvoId = t.EnvoId,
                    Content = t.Content
                })
                .Select(t => new EnvoTermSerializableModel
                {
                    Value = t.Content,
                    EnvoId = t.EnvoId,
                    Id = t.EntityId,
                    VerbatimTerm = t.Content
                });

            var tagger = new SimpleXmlSerializableObjectTagger<EnvoTermSerializableModel>(this.document.Xml, data, "/*", this.document.NamespaceManager, false, false, this.logger);
            this.InvokeProcessor(Messages.TagEnvoTermsMessage, tagger);
            this.document.Xml = tagger.Xml;
        }

        private void TagMorphologicalEpithets()
        {
            var context = new Bio.Data.BioDbContext();
            var repository = new Bio.Data.Repositories.EfBioDataGenericRepository<Bio.Data.Models.MorphologicalEpithet>(context);
            var service = new Bio.Services.Data.MorphologicalEpithetsDataService(repository);
            var harvester = new Bio.Harvesters.MorphologicalEpithetsHarvester(service);

            XmlElement tagModel = this.document.XmlDocument.CreateElement("named-content");
            tagModel.SetAttribute("content-type", "morphological epithet");

            var xpathProvider = new XPathProvider(this.settings.Config);

            var harvestableDocument = new HarvestableDocument(this.settings.Config, this.document.Xml);
            var data = harvester.Harvest(harvestableDocument.TextContent).Result;

            var tagger = new StringTagger(this.document.Xml, data, tagModel, xpathProvider.SelectContentNodesXPathTemplate, this.logger);
            this.InvokeProcessor(Messages.TagMorphologicalEpithetsMessage, tagger);
            this.document.Xml = tagger.Xml;
        }

        private void TagGeoEpithets()
        {
            var context = new Geo.Data.GeoDbContext();
            var repository = new Geo.Data.Repositories.EfGeoDataGenericRepository<Geo.Data.Models.GeoEpithet>(context);
            var service = new Geo.Services.Data.GeoEpithetsDataService(repository);
            var harvester = new Geo.Harvesters.GeoEpithetsHarvester(service);

            XmlElement tagModel = this.document.XmlDocument.CreateElement("named-content");
            tagModel.SetAttribute("content-type", "geo epithet");

            var xpathProvider = new XPathProvider(this.settings.Config);

            var harvestableDocument = new HarvestableDocument(this.settings.Config, this.document.Xml);
            var data = harvester.Harvest(harvestableDocument.TextContent).Result;

            var tagger = new StringTagger(this.document.Xml, data, tagModel, xpathProvider.SelectContentNodesXPathTemplate, this.logger);
            this.InvokeProcessor(Messages.TagGeoEpithetsMessage, tagger);
            this.document.Xml = tagger.Xml;
        }

        private void TagGeoNames()
        {
            var context = new Geo.Data.GeoDbContext();
            var repository = new Geo.Data.Repositories.EfGeoDataGenericRepository<Geo.Data.Models.GeoName>(context);
            var service = new Geo.Services.Data.GeoNamesDataService(repository);
            var harvester = new Geo.Harvesters.GeoNamesHarvester(service);

            XmlElement tagModel = this.document.XmlDocument.CreateElement("named-content");
            tagModel.SetAttribute("content-type", "geo name");

            var xpathProvider = new XPathProvider(this.settings.Config);

            var harvestableDocument = new HarvestableDocument(this.settings.Config, this.document.Xml);
            var data = harvester.Harvest(harvestableDocument.TextContent).Result;

            var tagger = new StringTagger(this.document.Xml, data, tagModel, xpathProvider.SelectContentNodesXPathTemplate, this.logger);
            this.InvokeProcessor(Messages.TagGeoNamesMessage, tagger);
            this.document.Xml = tagger.Xml;
        }

        private void TagInstitutions()
        {
            var context = new Data.DataDbContext();
            var repository = new Data.Repositories.EfDataGenericRepository<Data.Models.Institution>(context);
            var service = new Services.Data.InstitutionsDataService(repository);
            var harvester = new Harvesters.InstitutionsHarvester(service);

            XmlElement tagModel = this.document.XmlDocument.CreateElement("named-content");
            tagModel.SetAttribute("content-type", "institution");

            var xpathProvider = new XPathProvider(this.settings.Config);

            var harvestableDocument = new HarvestableDocument(this.settings.Config, this.document.Xml);
            var data = harvester.Harvest(harvestableDocument.TextContent).Result;

            var tagger = new StringTagger(this.document.Xml, data, tagModel, xpathProvider.SelectContentNodesXPathTemplate, this.logger);
            this.InvokeProcessor(Messages.TagInstitutionsMessage, tagger);
            this.document.Xml = tagger.Xml;
        }

        private void TagProducts()
        {
            var context = new Data.DataDbContext();
            var repository = new Data.Repositories.EfDataGenericRepository<Data.Models.Product>(context);
            var service = new Services.Data.ProductsDataService(repository);
            var harvester = new Harvesters.ProductsHarvester(service);

            XmlElement tagModel = this.document.XmlDocument.CreateElement("named-content");
            tagModel.SetAttribute("content-type", "product");

            var xpathProvider = new XPathProvider(this.settings.Config);

            var harvestableDocument = new HarvestableDocument(this.settings.Config, this.document.Xml);
            var data = harvester.Harvest(harvestableDocument.TextContent).Result;

            var tagger = new StringTagger(this.document.Xml, data, tagModel, xpathProvider.SelectContentNodesXPathTemplate, this.logger);
            this.InvokeProcessor(Messages.TagProductsMessage, tagger);
            this.document.Xml = tagger.Xml;
        }

        private string TagFloats(string xmlContent)
        {
            var tagger = new FloatsTagger(this.settings.Config, xmlContent, this.logger);
            this.InvokeProcessor(Messages.TagFloatsMessage, tagger);
            return tagger.Xml;
        }

        private string TagHigherTaxa(string xmlContent, Bio.Taxonomy.Services.Data.XmlListDataService blackList, Bio.Taxonomy.Services.Data.XmlListDataService whiteList)
        {
            var harvester = new Bio.Taxonomy.Harvesters.HigherTaxaHarvester(whiteList);
            var tagger = new HigherTaxaTagger(this.settings.Config, xmlContent, harvester, blackList, this.logger);
            this.InvokeProcessor(Messages.TagHigherTaxaMessage, tagger);
            return tagger.Xml.NormalizeXmlToSystemXml(this.settings.Config);
        }

        private string TagLowerTaxa(string xmlContent, Bio.Taxonomy.Services.Data.XmlListDataService blackList)
        {
            var tagger = new LowerTaxaTagger(this.settings.Config, xmlContent, blackList, this.logger);
            this.InvokeProcessor(Messages.TagLowerTaxaMessage, tagger);
            return tagger.Xml.NormalizeXmlToSystemXml(this.settings.Config);
        }

        private void TagQuantities()
        {
            var xpathProvider = new XPathProvider(this.settings.Config);

            {
                var harvester = new Harvesters.AltitudesHarvester();

                XmlElement tagModel = this.document.XmlDocument.CreateElement("named-content");
                tagModel.SetAttribute("content-type", "altitude");

                var harvestableDocument = new HarvestableDocument(this.settings.Config, this.document.Xml);
                var data = harvester.Harvest(harvestableDocument.TextContent).Result;

                var tagger = new StringTagger(this.document.Xml, data, tagModel, xpathProvider.SelectContentNodesXPathTemplate, this.logger);

                this.InvokeProcessor(Messages.TagAltitudesMessage, tagger);
                this.document.Xml = tagger.Xml;
            }

            {
                var harvester = new Harvesters.GeographicDeviationsHarvester();

                XmlElement tagModel = this.document.XmlDocument.CreateElement("named-content");
                tagModel.SetAttribute("content-type", "geographic deviation");

                var harvestableDocument = new HarvestableDocument(this.settings.Config, this.document.Xml);
                var data = harvester.Harvest(harvestableDocument.TextContent).Result;

                var tagger = new StringTagger(this.document.Xml, data, tagModel, xpathProvider.SelectContentNodesXPathTemplate, this.logger);

                this.InvokeProcessor(Messages.TagGeographicDeviationsMessage, tagger);
                this.document.Xml = tagger.Xml;
            }

            {
                var harvester = new Harvesters.QuantitiesHarvester();

                XmlElement tagModel = this.document.XmlDocument.CreateElement("named-content");
                tagModel.SetAttribute("content-type", "quantity");

                var harvestableDocument = new HarvestableDocument(this.settings.Config, this.document.Xml);
                var data = harvester.Harvest(harvestableDocument.TextContent).Result;

                var tagger = new StringTagger(this.document.Xml, data, tagModel, xpathProvider.SelectContentNodesXPathTemplate, this.logger);

                this.InvokeProcessor(Messages.TagQuantitiesMessage, tagger);
                this.document.Xml = tagger.Xml;
            }
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
            var harvestableDocument = new HarvestableDocument(this.settings.Config, this.document.Xml);
            var harvester = new Harvesters.NlmExternalLinksHarvester();
            var data = harvester.Harvest(harvestableDocument.TextContent).Result
                .Select(i => new ExternalLinkSerializableModel
                {
                    ExternalLinkType = i.Type.GetValue(),
                    Value = i.Content
                });

            var tagger = new SimpleXmlSerializableObjectTagger<ExternalLinkSerializableModel>(this.document.Xml, data, "/*", this.document.NamespaceManager, false, true, this.logger);
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
                    this.fileProcessor.Write(this.document, null, null);
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