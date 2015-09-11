namespace ProcessingTools.MainProgram
{
    using System.Diagnostics;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Xml;
    using BaseLibrary;
    using BaseLibrary.Abbreviations;
    using BaseLibrary.Coordinates;
    using BaseLibrary.ZooBank;

    public partial class MainProcessingTool
    {
        private static void FloraSpecific(FileProcessor fp)
        {
            FileProcessor flp = new FileProcessor(config, inputFileName, config.floraExtractedTaxaListPath);
            FileProcessor flpp = new FileProcessor(config, inputFileName, config.floraExtractTaxaPartsOutputPath);
            Flora fl = new Flora(config, fp.Xml);

            fl.ExtractTaxa();
            fl.DistinctTaxa();
            fl.GenerateTagTemplate();

            flp.Xml = fl.Xml;
            flp.Write();

            fl.Xml = fp.Xml;
            if (taxaA)
            {
                fl.PerformReplace();
            }

            if (taxaB)
            {
                ////fl.TagHigherTaxa();
            }

            if (taxaC)
            {
                if (flag1)
                {
                    fl.ParseInfra();
                }

                if (flag2)
                {
                    fl.ParseTn();
                }

                if (flag3)
                {
                    ////fl.SplitLowerTaxa();
                }
            }

            fp.Xml = fl.Xml;

            flpp.Xml = fl.ExtractTaxaParts();
            flpp.Write();
        }

        private static void InitialFormat(FileProcessor fp)
        {
            if (formatInit)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tInitial format.\n");

                if (!config.NlmStyle)
                {
                    string xml = fp.XmlReader.ApplyXslTransform(config.systemInitialFormatXslPath);
                    BaseLibrary.Format.NlmSystem.Formatter fmt = new BaseLibrary.Format.NlmSystem.Formatter(config, xml);
                    fmt.Format();
                    fp.Xml = fmt.Xml;
                }
                else
                {
                    string xml = fp.XmlReader.ApplyXslTransform(config.nlmInitialFormatXslPath);
                    BaseLibrary.Format.Nlm.Formatter fmt = new BaseLibrary.Format.Nlm.Formatter(config, xml);
                    fmt.Format();
                    fp.Xml = fmt.Xml;
                }

                PrintElapsedTime(timer);
            }
        }

        private static void ParseCoordinates(FileProcessor fp)
        {
            if (parseCoords)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tParse coordinates.\n");
                IBaseParser cooredinatesParser = new CoordinatesParser(config, fp.Xml);

                cooredinatesParser.Parse();

                fp.Xml = cooredinatesParser.Xml;
                PrintElapsedTime(timer);
            }
        }

        private static void ParseReferences(FileProcessor fp)
        {
            if (parseReferences)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tParse references.\n");
                References referencesParser = new References(config, fp.Xml);

                referencesParser.SplitReferences();

                fp.Xml = referencesParser.Xml;
                PrintElapsedTime(timer);
            }
        }

        private static void QuentinSpecific(FileProcessor fp)
        {
            QuentinFlora qf = new QuentinFlora(fp.Xml);
            if (formatInit)
            {
                qf.InitialFormat();
            }
            else if (flag1)
            {
                qf.Split1();
            }
            else if (flag2)
            {
                qf.Split2();
            }
            else
            {
                qf.FinalFormat();
            }

            fp.Xml = qf.Xml;
        }

        private static void TagAbbreviations(FileProcessor fp)
        {
            if (tagAbbrev)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tTag abbreviations.\n");

                IBaseTagger abbreviationsTagger = new AbbreviationsTagger(config, fp.Xml);
                abbreviationsTagger.Tag();
                fp.Xml = abbreviationsTagger.Xml;

                PrintElapsedTime(timer);
            }
        }

        private static void TagCodes(FileProcessor fp)
        {
            if (tagCodes)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tTag codes.\n");

                IXPathProvider xpathProvider = new XPathProvider(config);

                ////{
                ////    Codes codes = new Codes(config, fp.Xml);
                ////    codes.TagKnownSpecimenCodes(xpathProvider);
                ////    fp.Xml = codes.Xml;
                ////}

                {
                    IBaseTagger abbreviationsTagger = new AbbreviationsTagger(config, fp.Xml);
                    abbreviationsTagger.Tag();
                    fp.Xml = abbreviationsTagger.Xml;
                }

                ////{
                ////    SpecimenCountTagger specimenCountTagger = new SpecimenCountTagger(config, fp.Xml);
                ////    specimenCountTagger.TagSpecimenCount(xpathProvider);
                ////    fp.Xml = specimenCountTagger.Xml;
                ////}

                ////{
                ////    QuantitiesTagger quantitiesTagger = new QuantitiesTagger(config, fp.Xml);
                ////    quantitiesTagger.TagQuantities(xpathProvider);
                ////    quantitiesTagger.TagDeviation(xpathProvider);
                ////    quantitiesTagger.TagAltitude(xpathProvider);
                ////    fp.Xml = quantitiesTagger.Xml;
                ////}

                ////{
                ////    DatesTagger datesTagger = new DatesTagger(config, fp.Xml);
                ////    datesTagger.TagDates(xpathProvider);
                ////    fp.Xml = datesTagger.Xml;
                ////}

                {
                    config.EnvoResponseOutputXmlFileName = @"C:\temp\envo-out.xml";
                    Envo envo = new Envo(config, fp.Xml);
                    envo.Tag(xpathProvider);
                    fp.Xml = envo.Xml;
                }

                using (DataProvider dataProvider = new DataProvider(config, fp.Xml))
                {
                    ////{
                    ////    ProductsTagger products = new ProductsTagger(config, fp.Xml);
                    ////    products.TagProducts(xpathProvider, dataProvider);
                    ////    fp.Xml = products.Xml;
                    ////}

                    ////{
                    ////    GeoNamesTagger geonames = new GeoNamesTagger(config, fp.Xml);
                    ////    geonames.TagGeonames(xpathProvider, dataProvider);
                    ////    fp.Xml = geonames.Xml;
                    ////}

                    ////{
                    ////    MorphologyTagger morphology = new MorphologyTagger(config, fp.Xml);
                    ////    morphology.TagMorphology(xpathProvider, dataProvider);
                    ////    fp.Xml = morphology.Xml;
                    ////}

                    {
                        Codes codes = new Codes(config, fp.Xml);
                        codes.TagInstitutions(xpathProvider, dataProvider);
                        codes.TagInstitutionalCodes(xpathProvider, dataProvider);
                        ////codes.TagSpecimenCodes(xpathProvider);

                        fp.Xml = codes.Xml;
                    }
                }

                ////fp.XmlDocument.ClearTagsInWrongPositions();

                PrintElapsedTime(timer);
            }
        }

        private static void TagCoordinates(FileProcessor fp)
        {
            if (tagCoords)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tTag coordinates.\n");
                IBaseTagger coordinatesTagger = new CoordinatesTagger(config, fp.Xml);

                coordinatesTagger.Tag();

                fp.Xml = coordinatesTagger.Xml;
                PrintElapsedTime(timer);
            }
        }

        private static void TagDates(FileProcessor fp)
        {
            if (tagDates)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tTag dates.\n");

                IXPathProvider xpathProvider = new XPathProvider(config);
                IBaseTagger datesTagger = new DatesTagger(config, fp.Xml);
                datesTagger.Tag(xpathProvider);
                fp.Xml = datesTagger.Xml;

                PrintElapsedTime(timer);
            }
        }

        private static void TagDoi(FileProcessor fp)
        {
            if (tagDoi)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tTag DOI.\n");
                BaseLibrary.Nlm.LinksTagger linksTagger = new BaseLibrary.Nlm.LinksTagger(config, fp.Xml);

                linksTagger.TagDOI();
                linksTagger.TagPMCLinks();

                fp.Xml = linksTagger.Xml;
                PrintElapsedTime(timer);
            }
        }

        private static void TagEnvo(FileProcessor fp)
        {
            if (tagEnvo)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tUse greek tagger.\n");
                Envo envo = new Envo(config, fp.Xml);

                IXPathProvider xpathProvider = new XPathProvider(config);

                envo.Tag(xpathProvider);

                fp.Xml = envo.Xml;
                PrintElapsedTime(timer);
            }
        }

        private static void TagEnvoTerms(FileProcessor fp)
        {
            if (tagEnvironments)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tTag environments.\n");
                Environments environments = new Environments(config, fp.Xml);

                environments.Tag();

                fp.Xml = environments.Xml;
                PrintElapsedTime(timer);
            }
        }

        private static void TagQuantities(FileProcessor fp)
        {
            if (tagQuantities)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tTag quantities.\n");

                IXPathProvider xpathProvider = new XPathProvider(config);
                QuantitiesTagger quantitiesTagger = new QuantitiesTagger(config, fp.Xml);
                quantitiesTagger.TagQuantities(xpathProvider);
                quantitiesTagger.TagDeviation(xpathProvider);
                quantitiesTagger.TagAltitude(xpathProvider);
                fp.Xml = quantitiesTagger.Xml;

                PrintElapsedTime(timer);
            }
        }

        private static string TagReferences(string xml, string fileName)
        {
            string xmlContent = xml;
            References refs = new References(config, xmlContent);

            config.referencesGetReferencesXmlPath = Path.GetDirectoryName(fileName) + "\\zzz-" +
                Path.GetFileNameWithoutExtension(fileName) + "-references.xml";
            config.referencesTagTemplateXmlPath = config.tempDirectoryPath + "\\zzz-" +
                Path.GetFileNameWithoutExtension(fileName) + "-references-tag-template.xml";

            refs.GenerateTagTemplateXml();
            refs.TagReferences();
            xmlContent = refs.Xml;
            return xmlContent;
        }

        private static void TagReferences(FileProcessor fp)
        {
            Alert.Log("\n\tTag references.\n");
            if (parseBySection)
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.PreserveWhitespace = true;
                XmlNamespaceManager namespaceManager = Config.TaxPubNamespceManager(xmlDocument);

                try
                {
                    xmlDocument.LoadXml(fp.Xml);
                }
                catch (XmlException)
                {
                    Alert.Log("Tagger: XmlException");
                    Alert.Exit(10);
                }

                try
                {
                    foreach (XmlNode node in xmlDocument.SelectNodes(higherStructrureXpath, namespaceManager))
                    {
                        string templateFileName = string.Empty;
                        if (node.Attributes["sec-type"] != null)
                        {
                            templateFileName = node.Attributes["sec-type"].InnerText;
                        }

                        if (node["front"]["article-meta"]["article-id"] != null)
                        {
                            templateFileName = Regex.Replace(node["front"]["article-meta"]["article-id"].InnerText, @"\d+\.\d+/", string.Empty);
                        }

                        templateFileName = Regex.Replace(templateFileName, @"\W+", "_");
                        templateFileName = Regex.Replace(templateFileName, @"^(.{0,30}).*$", "$1_" + node.GetHashCode());

                        Alert.Log(templateFileName);

                        XmlNode newNode = node;
                        newNode.InnerXml = TagReferences(node.OuterXml, templateFileName);
                        node.InnerXml = newNode.FirstChild.InnerXml;
                    }
                }
                catch (System.Xml.XPath.XPathException)
                {
                    Alert.Log("Tagger: XPathException");
                    Alert.Exit(1);
                }
                catch (System.InvalidOperationException)
                {
                    Alert.Log("Tagger: InvalidOperationException");
                    Alert.Exit(1);
                }
                catch (XmlException)
                {
                    Alert.Log("Tagger: XmlException");
                    Alert.Exit(1);
                }

                fp.Xml = xmlDocument.OuterXml;
            }
            else
            {
                fp.Xml = TagReferences(fp.Xml, fp.OutputFileName);
            }
        }

        private static void TagWebLinks(FileProcessor fp)
        {
            if (tagWWW)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tTag web links.\n");
                BaseLibrary.Nlm.LinksTagger linksTagger = new BaseLibrary.Nlm.LinksTagger(config, fp.Xml);

                linksTagger.TagWWW();

                fp.Xml = linksTagger.Xml;
                PrintElapsedTime(timer);
            }
        }

        private static void ZooBankCloneJson(FileProcessor fp)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Alert.ZoobankCloneMessage();
            if (arguments.Count > 2)
            {
                string jsonStringContent = FileProcessor.ReadFileContentToString(queryFileName);
                ZoobankCloner zoobankCloner = new ZoobankCloner(fp.Xml);
                zoobankCloner.CloneJsonToXml(jsonStringContent);
                fp.Xml = zoobankCloner.Xml;
            }

            PrintElapsedTime(timer);
        }

        private static void ZooBankCloneXml(FileProcessor fp)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Alert.ZoobankCloneMessage();
            if (arguments.Count > 2)
            {
                FileProcessor fileProcessorNlm = new FileProcessor(config, queryFileName, outputFileName);
                fileProcessorNlm.Read();
                ZoobankCloner zoobankCloner = new ZoobankCloner(fileProcessorNlm.Xml, fp.Xml);
                zoobankCloner.Clone();
                fp.Xml = zoobankCloner.Xml;
            }

            PrintElapsedTime(timer);
        }

        private static void ZooBankGenerateRegistrationXml(FileProcessor fp)
        {
            ZooBank zoobankRegistrationXmlGenerator = new ZooBank(config, fp.Xml);
            zoobankRegistrationXmlGenerator.GenerateZooBankNlm();
            fp.Xml = zoobankRegistrationXmlGenerator.Xml;
        }
    }
}