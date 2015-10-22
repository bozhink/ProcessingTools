namespace ProcessingTools.MainProgram
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
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
    using Globals;
    using Globals.Loggers;

    public class SingleFileProcessor
    {
        private TaxonomicBlackList blackList;
        private FileProcessor fileProcessor;
        private ILogger logger;
        private ProgramSettings settings;
        private TaxonomicWhiteList whiteList;

        public SingleFileProcessor(ProgramSettings settings, ILogger logger)
        {
            this.settings = settings;
            this.logger = logger;
        }

        public Task Run()
        {
            return Task.Run(() =>
            {
                DoFileProcessing();
            });
        }

        public void ValidateTaxa(string xmlContent)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tTaxa validation using Global names resolver.\n");

            try
            {
                var validator = new TaxonomicNamesValidator(this.settings.Config, xmlContent, this.logger);
                validator.Validate();
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            this.PrintElapsedTime(timer);
        }

        private void DoFileProcessing()
        {
            try
            {
                this.SetUpFileProcessor();

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
                else if (this.settings.QuentinSpecificActions)
                {
                    this.QuentinSpecific();
                }
                else if (this.settings.Flora)
                {
                    this.FloraSpecific();
                }
                else if (this.settings.QueryReplace && this.settings.QueryFileName != null && this.settings.QueryFileName.Length > 0)
                {
                    this.fileProcessor.Xml = QueryReplace.Replace(this.settings.Config, this.fileProcessor.Xml, this.settings.QueryFileName);
                }
                else
                {
                    // Initial format
                    if (this.settings.FormatInit)
                    {
                        this.InitialFormat();
                    }

                    // Parse reference's parts
                    if (this.settings.ParseReferences)
                    {
                        this.ParseReferences();
                    }

                    // Tag DOI
                    if (this.settings.TagDoi)
                    {
                        this.TagDoi();
                    }

                    // Tag web links
                    if (this.settings.TagWWW)
                    {
                        this.TagWebLinks();
                    }

                    // Tag coordinates
                    if (this.settings.TagCoords)
                    {
                        this.TagCoordinates();
                    }

                    // Parse coordinates
                    if (this.settings.ParseCoords)
                    {
                        this.ParseCoordinates();
                    }

                    // Tag envo terms using the greek tagger
                    if (this.settings.TagEnvo)
                    {
                        this.TagEnvo();
                    }

                    // Tag envo terms using envornment database
                    if (this.settings.TagEnvironments)
                    {
                        this.TagEnvoTerms();
                    }

                    // Tag quatities
                    if (this.settings.TagQuantities)
                    {
                        this.TagQuantities();
                    }

                    // Tag dates
                    if (this.settings.TagDates)
                    {
                        this.TagDates();
                    }

                    // Tag abbreviations
                    if (this.settings.TagAbbrev)
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
                    }

                    // Main Tagging part of the program
                    if (this.settings.ParseBySection)
                    {
                        XmlDocument xmlDocument = new XmlDocument(this.fileProcessor.NamespaceManager.NameTable);
                        xmlDocument.PreserveWhitespace = true;

                        try
                        {
                            xmlDocument.LoadXml(this.fileProcessor.Xml);
                        }
                        catch
                        {
                            throw;
                        }

                        try
                        {
                            foreach (XmlNode node in xmlDocument.SelectNodes(this.settings.HigherStructrureXpath, this.fileProcessor.NamespaceManager))
                            {
                                if (this.settings.TagReferences)
                                {
                                    this.SetRefencesTemplateFileNamesToConfig(this.GenerateReferencesTemplateFileName(node));
                                }

                                XmlDocumentFragment fragment = node.OwnerDocument.CreateDocumentFragment();
                                fragment.InnerXml = this.MainProcessing(node.OuterXml);
                                node.ParentNode.ReplaceChild(fragment, node);
                            }

                            this.fileProcessor.Xml = xmlDocument.OuterXml;
                        }
                        catch
                        {
                            throw;
                        }
                    }
                    else
                    {
                        if (this.settings.TagReferences)
                        {
                            this.SetRefencesTemplateFileNamesToConfig(this.fileProcessor.OutputFileName);
                        }

                        this.fileProcessor.Xml = this.MainProcessing(this.fileProcessor.Xml);
                    }
                }

                this.WriteOutputFile();
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
                throw e;
            }
        }

        private string ExpandTaxa(string xmlContent)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tExpand taxa.\n");

            try
            {
                var expand = new BaseLibrary.Taxonomy.Nlm.Expander(this.settings.Config, xmlContent, this.logger);
                var exp = new Expander(this.settings.Config, xmlContent, this.logger);

                for (int i = 0; i < Program.NumberOfExpandingIterations; ++i)
                {
                    if (this.settings.TaxaE)
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
                IEnumerable<string> taxaList;

                if (this.settings.ExtractTaxa)
                {
                    this.logger?.Log("\n\t\tExtract all taxa\n");
                    taxaList = xmlDocument.ExtractTaxa(true);

                    foreach (string taxon in taxaList)
                    {
                        this.logger?.Log(taxon);
                    }
                }

                if (this.settings.ExtractLowerTaxa)
                {
                    this.logger?.Log("\n\t\tExtract lower taxa\n");
                    taxaList = xmlDocument.ExtractTaxa(true, TaxaType.Lower);

                    foreach (string taxon in taxaList)
                    {
                        this.logger?.Log(taxon);
                    }
                }

                if (this.settings.ExtractHigherTaxa)
                {
                    this.logger?.Log("\n\t\tExtract higher taxa\n");
                    taxaList = xmlDocument.ExtractTaxa(true, TaxaType.Higher);

                    foreach (string taxon in taxaList)
                    {
                        this.logger?.Log(taxon);
                    }
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }
        }

        private void FloraSpecific()
        {
            try
            {
                var flp = new FileProcessor(this.settings.Config, this.settings.InputFileName, this.settings.Config.FloraExtractedTaxaListPath);
                var flpp = new FileProcessor(this.settings.Config, this.settings.InputFileName, this.settings.Config.FloraExtractTaxaPartsOutputPath);
                var floraProcessor = new Flora(this.settings.Config, this.fileProcessor.Xml);

                floraProcessor.ExtractTaxa();
                floraProcessor.DistinctTaxa();
                floraProcessor.GenerateTagTemplate();

                flp.Xml = floraProcessor.Xml;
                flp.Write();

                floraProcessor.Xml = this.fileProcessor.Xml;
                if (this.settings.TaxaA)
                {
                    floraProcessor.PerformReplace();
                }

                if (this.settings.TaxaB)
                {
                    ////fl.TagHigherTaxa();
                }

                if (this.settings.TaxaC)
                {
                    if (this.settings.Flag1)
                    {
                        floraProcessor.ParseInfra();
                    }

                    if (this.settings.Flag2)
                    {
                        floraProcessor.ParseTn();
                    }

                    if (this.settings.Flag3)
                    {
                        ////fl.SplitLowerTaxa();
                    }
                }

                this.fileProcessor.Xml = floraProcessor.Xml;

                flpp.Xml = floraProcessor.ExtractTaxaParts();
                flpp.Write();
            }
            catch
            {
                throw;
            }
        }

        private string FormatTreatments(string xmlContent)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tFormat treatments.\n");

            try
            {
                var formatter = new TreatmentFormatter(this.settings.Config, xmlContent, this.logger);
                formatter.Format();
                xmlContent = formatter.Xml;
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            this.PrintElapsedTime(timer);
            return xmlContent;
        }

        private string GenerateReferencesTemplateFileName(XmlNode node)
        {
            string templateFileName = string.Empty;
            try
            {
                templateFileName = Regex.Replace(node["front"]["article-meta"]["article-id"].InnerText, @"\d+\.\d+/", string.Empty);
            }
            catch
            {
                if (node.Attributes["sec-type"] != null)
                {
                    templateFileName = node.Attributes["sec-type"].InnerText;
                }
            }

            templateFileName = templateFileName
                .RegexReplace(@"\W+", "_")
                .RegexReplace(@"^(.{0,30}).*$", "$1_" + node.GetHashCode());

            this.logger?.Log(templateFileName);

            return templateFileName;
        }

        private void InitialFormat()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tInitial format.\n");

            try
            {
                switch (this.settings.Config.ArticleSchemaType)
                {
                    case SchemaType.Nlm:
                        {
                            string xml = this.fileProcessor.XmlReader.ApplyXslTransform(this.settings.Config.NlmInitialFormatXslPath);
                            var formatter = new BaseLibrary.Format.Nlm.Formatter(this.settings.Config, xml);
                            formatter.Format();
                            this.fileProcessor.Xml = formatter.Xml;
                        }

                        break;

                    default:
                        {
                            string xml = this.fileProcessor.XmlReader.ApplyXslTransform(this.settings.Config.SystemInitialFormatXslPath);
                            var formatter = new BaseLibrary.Format.NlmSystem.Formatter(this.settings.Config, xml);
                            formatter.Format();
                            this.fileProcessor.Xml = formatter.Xml;
                        }

                        break;
                }
            }
            catch
            {
                throw;
            }

            this.PrintElapsedTime(timer);
        }

        private string MainProcessing(string xml)
        {
            string xmlContent = xml;

            if (this.settings.TagFigTab)
            {
                xmlContent = this.TagFloats(xmlContent);
            }

            if (this.settings.TagTableFn)
            {
                xmlContent = this.TagTableFootnote(xmlContent);
            }

            // Taxonomic part
            if (this.settings.TaxaA || this.settings.TaxaB)
            {
                this.blackList = new TaxonomicBlackList(this.settings.Config);
                this.whiteList = new TaxonomicWhiteList(this.settings.Config);

                xmlContent = this.TagLowerTaxa(xmlContent);
                xmlContent = this.TagHigherTaxa(xmlContent);

                ////this.blackList.Clear();
                ////this.whiteList.Clear();
            }

            xmlContent = this.ParseLowerTaxa(xmlContent);
            xmlContent = this.ParseHigherTaxa(xmlContent);

            if (this.settings.TaxaE || this.settings.Flag1 || this.settings.Flag2 || this.settings.Flag3 || this.settings.Flag4 || this.settings.Flag5 || this.settings.Flag6 || this.settings.Flag7 || this.settings.Flag8)
            {
                xmlContent = this.ExpandTaxa(xmlContent);
            }

            //// Flora-like tests
            ////{
            ////    FileProcessor testFp = new FileProcessor();
            ////    testFp.Xml = Xml;

            ////    testFp.OutputFileName = @"C:\temp\taxa-0.xml";
            ////    testFp.Xml = Base.Taxonomy.Tagger.Tagger.ExtractTaxa(config, testFp.Xml);
            ////    testFp.WriteXMLFile();

            ////    //testFp.OutputFileName = @"C:\temp\taxa-1.xml";
            ////    //testFp.Xml = Base.Taxonomy.Tagger.Tagger.DistinctTaxa(config, testFp.Xml);
            ////    //testFp.WriteXMLFile();

            ////    testFp.OutputFileName = @"C:\temp\taxa-2.xml";
            ////    testFp.Xml = Base.Taxonomy.Tagger.Tagger.GenerateTagTemplate(config, testFp.Xml);
            ////    testFp.WriteXMLFile();

            ////    Base.Taxonomy.Tagger.Tagger tagger = new Base.Taxonomy.Tagger.Tagger();
            ////    tagger.Config = config;
            ////    tagger.Xml = Xml;
            ////    tagger.PerformFloraReplace(testFp.Xml);

            ////    testFp.OutputFileName = @"C:\temp\taxa-3-replaced.xml";
            ////    testFp.Xml = tagger.Xml;
            ////    testFp.WriteXMLFile();

            ////}

            // Extract taxa
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
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tParse coordinates.\n");

            try
            {
                var cooredinatesParser = new CoordinatesParser(this.settings.Config, this.fileProcessor.Xml, this.logger);
                cooredinatesParser.Parse();
                this.fileProcessor.Xml = cooredinatesParser.Xml;
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            this.PrintElapsedTime(timer);
        }

        private string ParseHigherTaxa(string xmlContent)
        {
            if (this.settings.TaxaD)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.logger?.Log("\n\tParse higher taxa.\n");

                try
                {
                    var parser = new LocalDataBaseHigherTaxaParser(this.settings.Config, xmlContent, this.logger);
                    parser.Parse();
                    parser.XmlDocument.PrintNonParsedTaxa(this.logger);
                    xmlContent = parser.Xml;
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, string.Empty);
                }

                if (this.settings.ParseHigherWithAphia)
                {
                    this.logger?.Log("\n\tSplit higher taxa using Aphia API\n");

                    try
                    {
                        var parser = new AphiaHigherTaxaParser(this.settings.Config, xmlContent, this.logger);
                        parser.Parse();
                        parser.XmlDocument.PrintNonParsedTaxa(this.logger);
                        xmlContent = parser.Xml;
                    }
                    catch (Exception e)
                    {
                        this.logger?.Log(e, string.Empty);
                    }
                }

                if (this.settings.ParseHigherWithCoL)
                {
                    this.logger?.Log("\n\tSplit higher taxa using CoL API\n");

                    try
                    {
                        var parser = new CoLHigherTaxaParser(this.settings.Config, xmlContent, this.logger);
                        parser.Parse();
                        parser.XmlDocument.PrintNonParsedTaxa(this.logger);
                        xmlContent = parser.Xml;
                    }
                    catch (Exception e)
                    {
                        this.logger?.Log(e, string.Empty);
                    }
                }

                if (this.settings.ParseHigherWithGbif)
                {
                    this.logger?.Log("\n\tSplit higher taxa using GBIF API\n");

                    try
                    {
                        var parser = new GbifHigherTaxaParser(this.settings.Config, xmlContent, this.logger);
                        parser.Parse();
                        parser.XmlDocument.PrintNonParsedTaxa(this.logger);
                        xmlContent = parser.Xml;
                    }
                    catch (Exception e)
                    {
                        this.logger?.Log(e, string.Empty);
                    }
                }

                if (this.settings.ParseHigherBySuffix)
                {
                    this.logger?.Log("\n\tSplit higher taxa by suffix\n");

                    try
                    {
                        var parser = new SuffixHigherTaxaParser(this.settings.Config, xmlContent, this.logger);
                        parser.Parse();
                        parser.XmlDocument.PrintNonParsedTaxa(this.logger);
                        xmlContent = parser.Xml;
                    }
                    catch (Exception e)
                    {
                        this.logger?.Log(e, string.Empty);
                    }
                }

                if (this.settings.ParseHigherAboveGenus)
                {
                    this.logger?.Log("\n\tMake higher taxa of type 'above-genus'\n");

                    try
                    {
                        var parser = new AboveGenusHigherTaxaParser(this.settings.Config, xmlContent, this.logger);
                        parser.Parse();
                        parser.XmlDocument.PrintNonParsedTaxa(this.logger);
                        xmlContent = parser.Xml;
                    }
                    catch (Exception e)
                    {
                        this.logger?.Log(e, string.Empty);
                    }
                }

                this.PrintElapsedTime(timer);
            }

            return xmlContent;
        }

        private string ParseLowerTaxa(string xmlContent)
        {
            if (this.settings.TaxaC)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.logger?.Log("\n\tParse lower taxa.\n");

                try
                {
                    var parser = new LowerTaxaParser(this.settings.Config, xmlContent, this.logger);
                    parser.Parse();
                    xmlContent = parser.Xml;
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, string.Empty);
                }

                this.PrintElapsedTime(timer);
            }

            return xmlContent;
        }

        private void ParseReferences()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tParse references.\n");

            try
            {
                var referencesParser = new References(this.settings.Config, this.fileProcessor.Xml, this.logger);
                referencesParser.SplitReferences();
                this.fileProcessor.Xml = referencesParser.Xml;
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            this.PrintElapsedTime(timer);
        }

        private string ParseTreatmentMeta(string xmlContent)
        {
            if (this.settings.ParseTreatmentMetaWithAphia)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.logger?.Log("\n\tParse treatment meta with Aphia.\n");

                try
                {
                    var parser = new AphiaTreatmentMetaParser(this.settings.Config, xmlContent, this.logger);
                    parser.Parse();
                    xmlContent = parser.Xml;
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, string.Empty);
                }

                this.PrintElapsedTime(timer);
            }

            if (this.settings.ParseTreatmentMetaWithGbif)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.logger?.Log("\n\tParse treatment meta with GBIF.\n");

                try
                {
                    var parser = new GbifTreatmentMetaParser(this.settings.Config, xmlContent, this.logger);
                    parser.Parse();
                    xmlContent = parser.Xml;
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, string.Empty);
                }

                this.PrintElapsedTime(timer);
            }

            if (this.settings.ParseTreatmentMetaWithCol)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.logger?.Log("\n\tParse treatment meta with CoL.\n");

                try
                {
                    var parser = new CoLTreatmentMetaParser(this.settings.Config, xmlContent, this.logger);
                    parser.Parse();
                    xmlContent = parser.Xml;
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, string.Empty);
                }

                this.PrintElapsedTime(timer);
            }

            return xmlContent;
        }

        private void PrintElapsedTime(Stopwatch timer)
        {
            this.logger?.Log(LogType.Info, "Elapsed time {0}.", timer.Elapsed);
        }

        private void QuentinSpecific()
        {
            try
            {
                var flora = new QuentinFlora(this.fileProcessor.Xml);
                if (this.settings.FormatInit)
                {
                    flora.InitialFormat();
                }
                else if (this.settings.Flag1)
                {
                    flora.Split1();
                }
                else if (this.settings.Flag2)
                {
                    flora.Split2();
                }
                else
                {
                    flora.FinalFormat();
                }

                this.fileProcessor.Xml = flora.Xml;
            }
            catch
            {
                throw;
            }
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

        private void SetRefencesTemplateFileNamesToConfig(string fileName)
        {
            this.settings.Config.ReferencesGetReferencesXmlPath = $"{Path.GetDirectoryName(fileName)}\\zzz-{Path.GetFileNameWithoutExtension(fileName)}-references.xml";

            this.settings.Config.ReferencesTagTemplateXmlPath = $"{this.settings.Config.TempDirectoryPath}\\zzz-{Path.GetFileNameWithoutExtension(fileName)}-references-tag-template.xml";
        }

        private void SetUpFileProcessor()
        {
            try
            {
                this.fileProcessor = new FileProcessor(
                                this.settings.Config,
                                this.settings.InputFileName,
                                this.settings.OutputFileName,
                                this.logger);

                this.logger?.Log(
                    "Input file name: {0}\nOutput file name: {1}\n{2}",
                    this.fileProcessor.InputFileName,
                    this.fileProcessor.OutputFileName,
                    this.settings.QueryFileName);

                this.settings.Config.EnvoResponseOutputXmlFileName = $"{this.settings.Config.TempDirectoryPath}\\envo-{Path.GetFileNameWithoutExtension(this.fileProcessor.OutputFileName)}.xml";

                this.settings.Config.GnrOutputFileName = $"{this.settings.Config.TempDirectoryPath}\\gnr-{Path.GetFileNameWithoutExtension(this.fileProcessor.OutputFileName)}.xml";

                this.fileProcessor.Read();

                switch (this.fileProcessor.XmlDocument.DocumentElement.Name)
                {
                    case "article":
                        this.settings.Config.ArticleSchemaType = SchemaType.Nlm;
                        break;

                    default:
                        this.settings.Config.ArticleSchemaType = SchemaType.System;
                        break;
                }

                this.fileProcessor.Xml = this.fileProcessor.Xml.NormalizeXmlToSystemXml(this.settings.Config);
            }
            catch
            {
                throw;
            }
        }

        private void TagAbbreviations()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tTag abbreviations.\n");

            try
            {
                var abbreviationsTagger = new AbbreviationsTagger(this.settings.Config, this.fileProcessor.Xml);
                abbreviationsTagger.Tag();
                this.fileProcessor.Xml = abbreviationsTagger.Xml;
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            this.PrintElapsedTime(timer);
        }

        private void TagCodes()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tTag codes.\n");

            try
            {
                var xpathProvider = new XPathProvider(this.settings.Config);

                ////{
                ////    Codes codes = new Codes(settings.Config, fp.Xml);
                ////    codes.TagKnownSpecimenCodes(xpathProvider);
                ////    fp.Xml = codes.Xml;
                ////}

                ////{
                ////    IBaseTagger abbreviationsTagger = new AbbreviationsTagger(settings.Config, fp.Xml);
                ////    abbreviationsTagger.Tag();
                ////    fp.Xml = abbreviationsTagger.Xml;
                ////}

                ////{
                ////    SpecimenCountTagger specimenCountTagger = new SpecimenCountTagger(settings.Config, fp.Xml);
                ////    specimenCountTagger.TagSpecimenCount(xpathProvider);
                ////    fp.Xml = specimenCountTagger.Xml;
                ////}

                ////{
                ////    QuantitiesTagger quantitiesTagger = new QuantitiesTagger(settings.Config, fp.Xml);
                ////    quantitiesTagger.TagQuantities(xpathProvider);
                ////    quantitiesTagger.TagDeviation(xpathProvider);
                ////    quantitiesTagger.TagAltitude(xpathProvider);
                ////    fp.Xml = quantitiesTagger.Xml;
                ////}

                ////{
                ////    DatesTagger datesTagger = new DatesTagger(settings.Config, fp.Xml);
                ////    datesTagger.TagDates(xpathProvider);
                ////    fp.Xml = datesTagger.Xml;
                ////}

                ////{
                ////    settings.Config.EnvoResponseOutputXmlFileName = @"C:\temp\envo-out.xml";
                ////    Envo envo = new Envo(settings.Config, fp.Xml);
                ////    envo.Tag(xpathProvider);
                ////    fp.Xml = envo.Xml;
                ////}

                DataProvider dataProvider = new DataProvider(this.settings.Config, this.fileProcessor.Xml, this.logger);
                ////{
                ////    ProductsTagger products = new ProductsTagger(settings.Config, fp.Xml);
                ////    products.TagProducts(xpathProvider, dataProvider);
                ////    fp.Xml = products.Xml;
                ////}

                ////{
                ////    GeoNamesTagger geonames = new GeoNamesTagger(settings.Config, fp.Xml);
                ////    geonames.TagGeonames(xpathProvider, dataProvider);
                ////    fp.Xml = geonames.Xml;
                ////}

                ////{
                ////    MorphologyTagger morphology = new MorphologyTagger(settings.Config, fp.Xml);
                ////    morphology.TagMorphology(xpathProvider, dataProvider);
                ////    fp.Xml = morphology.Xml;
                ////}

                try
                {
                    Codes codes = new Codes(this.settings.Config, this.fileProcessor.Xml, this.logger);
                    codes.TagInstitutions(xpathProvider, dataProvider);
                    codes.TagInstitutionalCodes(xpathProvider, dataProvider);
                    ////codes.TagSpecimenCodes(xpathProvider);

                    this.fileProcessor.Xml = codes.Xml;
                }
                catch
                {
                    throw;
                }

                ////fp.XmlDocument.ClearTagsInWrongPositions();
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            this.PrintElapsedTime(timer);
        }

        private void TagCoordinates()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tTag coordinates.\n");

            try
            {
                var coordinatesTagger = new CoordinatesTagger(this.settings.Config, this.fileProcessor.Xml, this.logger);
                coordinatesTagger.Tag();
                this.fileProcessor.Xml = coordinatesTagger.Xml;
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            this.PrintElapsedTime(timer);
        }

        private void TagDates()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tTag dates.\n");

            try
            {
                var xpathProvider = new XPathProvider(this.settings.Config);
                var datesTagger = new DatesTagger(this.settings.Config, this.fileProcessor.Xml);
                datesTagger.Tag(xpathProvider);
                this.fileProcessor.Xml = datesTagger.Xml;
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            this.PrintElapsedTime(timer);
        }

        private void TagDoi()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tTag DOI.\n");

            try
            {
                var linksTagger = new DoiLinksTagger(this.settings.Config, this.fileProcessor.Xml);

                linksTagger.Tag();

                this.fileProcessor.Xml = linksTagger.Xml;
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            this.PrintElapsedTime(timer);
        }

        private void TagEnvo()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tUse greek tagger.\n");

            try
            {
                var xpathProvider = new XPathProvider(this.settings.Config);
                var envo = new Envo(this.settings.Config, this.fileProcessor.Xml, this.logger);
                envo.Tag(xpathProvider);
                this.fileProcessor.Xml = envo.Xml;
            }
            catch
            {
                throw;
            }

            this.PrintElapsedTime(timer);
        }

        private void TagEnvoTerms()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tTag environments.\n");

            try
            {
                var environments = new Environments(this.settings.Config, this.fileProcessor.Xml);
                environments.Tag();
                this.fileProcessor.Xml = environments.Xml;
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            this.PrintElapsedTime(timer);
        }

        private string TagFloats(string xmlContent)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tTag floats.\n");

            try
            {
                var fl = new FloatsTagger(this.settings.Config, xmlContent, this.logger);
                fl.Tag();
                xmlContent = fl.Xml;
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            this.PrintElapsedTime(timer);
            return xmlContent;
        }

        private string TagHigherTaxa(string xmlContent)
        {
            if (this.settings.TaxaB)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.logger?.Log("\n\tTag higher taxa.\n");

                try
                {
                    var tagger = new HigherTaxaTagger(this.settings.Config, xmlContent, this.whiteList, this.blackList, this.logger);
                    tagger.Tag();
                    xmlContent = tagger.Xml.NormalizeXmlToSystemXml(this.settings.Config);
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, string.Empty);
                }

                this.PrintElapsedTime(timer);
            }

            return xmlContent;
        }

        private string TagLowerTaxa(string xmlContent)
        {
            if (this.settings.TaxaA)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.logger?.Log("\n\tTag lower taxa.\n");

                try
                {
                    var tagger = new LowerTaxaTagger(this.settings.Config, xmlContent, this.whiteList, this.blackList);
                    tagger.Tag();
                    xmlContent = tagger.Xml.NormalizeXmlToSystemXml(this.settings.Config);
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, string.Empty);
                }

                this.PrintElapsedTime(timer);
            }

            return xmlContent;
        }

        private void TagQuantities()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tTag quantities.\n");

            try
            {
                var xpathProvider = new XPathProvider(this.settings.Config);
                var quantitiesTagger = new QuantitiesTagger(this.settings.Config, this.fileProcessor.Xml, this.logger);

                quantitiesTagger.TagQuantities(xpathProvider);
                quantitiesTagger.TagDeviation(xpathProvider);
                quantitiesTagger.TagAltitude(xpathProvider);

                this.fileProcessor.Xml = quantitiesTagger.Xml;
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            this.PrintElapsedTime(timer);
        }

        private string TagReferences(string xmlContent)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tTag references.\n");

            var references = new References(this.settings.Config, xmlContent, this.logger);

            references.GenerateTagTemplateXml();
            references.TagReferences();

            this.PrintElapsedTime(timer);

            return references.Xml;
        }

        private string TagTableFootnote(string xmlContent)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tTag table foot-notes.\n");

            try
            {
                var fl = new TableFootNotesTagger(this.settings.Config, xmlContent, this.logger);
                fl.Tag();
                xmlContent = fl.Xml;
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            this.PrintElapsedTime(timer);
            return xmlContent;
        }

        private void TagWebLinks()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tTag web links.\n");

            try
            {
                var linksTagger = new UrlLinksTagger(this.settings.Config, this.fileProcessor.Xml);
                linksTagger.Tag();
                this.fileProcessor.Xml = linksTagger.Xml;
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            this.PrintElapsedTime(timer);
        }

        private void WriteOutputFile()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tWriting data to output file.\n");

            try
            {
                this.fileProcessor.Xml = this.fileProcessor.Xml.NormalizeXmlToCurrentXml(this.settings.Config);
                this.fileProcessor.Write();
            }
            catch
            {
                throw;
            }

            this.PrintElapsedTime(timer);
        }

        private void ZooBankCloneJson()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tZoobank clone JSON.\n");

            if (this.settings.QueryFileName != null)
            {
                try
                {
                    string jsonStringContent = FileProcessor.ReadFileContentToString(this.settings.QueryFileName);
                    var zoobankCloner = new ZoobankJsonCloner(jsonStringContent, this.fileProcessor.Xml, this.logger);
                    zoobankCloner.Clone();
                    this.fileProcessor.Xml = zoobankCloner.Xml;
                }
                catch
                {
                    throw;
                }
            }

            this.PrintElapsedTime(timer);
        }

        private void ZooBankCloneXml()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.logger?.Log("\n\tZoobank clone XML.\n");

            if (this.settings.QueryFileName != null)
            {
                try
                {
                    var fileProcessorNlm = new FileProcessor(this.settings.Config, this.settings.QueryFileName, this.settings.OutputFileName);
                    fileProcessorNlm.Read();

                    var zoobankCloner = new ZoobankXmlCloner(fileProcessorNlm.Xml, this.fileProcessor.Xml, this.logger);
                    zoobankCloner.Clone();

                    this.fileProcessor.Xml = zoobankCloner.Xml;
                }
                catch
                {
                    throw;
                }
            }

            this.PrintElapsedTime(timer);
        }

        private void ZooBankGenerateRegistrationXml()
        {
            try
            {
                var zoobankRegistrationXmlGenerator = new ZoobankRegistrationXmlGenerator(this.settings.Config, this.fileProcessor.Xml);
                zoobankRegistrationXmlGenerator.Generate();
                this.fileProcessor.Xml = zoobankRegistrationXmlGenerator.Xml;
            }
            catch
            {
                throw;
            }
        }
    }
}