namespace ProcessingTools.MainProgram
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Xml;
    using BaseLibrary;
    using BaseLibrary.Abbreviations;
    using BaseLibrary.Coordinates;
    using BaseLibrary.Dates;
    using BaseLibrary.Measurements;
    using BaseLibrary.References;
    using BaseLibrary.ZooBank;
    using BaseLibrary.HyperLinks;

    public partial class MainProcessingTool
    {
        private static void FloraSpecific(FileProcessor fp)
        {
            try
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
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 1);
            }
        }

        private static void InitialFormat(FileProcessor fp)
        {
            if (formatInit)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tInitial format.\n");

                try
                {
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
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, 1);
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

                try
                {
                    IBaseParser cooredinatesParser = new CoordinatesParser(config, fp.Xml);
                    cooredinatesParser.Parse();
                    fp.Xml = cooredinatesParser.Xml;
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, 0);
                }

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

                try
                {
                    References referencesParser = new References(config, fp.Xml);
                    referencesParser.SplitReferences();
                    fp.Xml = referencesParser.Xml;
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, 0);
                }

                PrintElapsedTime(timer);
            }
        }

        private static void QuentinSpecific(FileProcessor fp)
        {
            try
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
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 1);
            }
        }

        private static void TagAbbreviations(FileProcessor fp)
        {
            if (tagAbbrev)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tTag abbreviations.\n");

                try
                {
                    IBaseTagger abbreviationsTagger = new AbbreviationsTagger(config, fp.Xml);
                    abbreviationsTagger.Tag();
                    fp.Xml = abbreviationsTagger.Xml;
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, 0);
                }

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

                try
                {
                    IXPathProvider xpathProvider = new XPathProvider(config);

                    ////{
                    ////    Codes codes = new Codes(config, fp.Xml);
                    ////    codes.TagKnownSpecimenCodes(xpathProvider);
                    ////    fp.Xml = codes.Xml;
                    ////}

                    ////{
                    ////    IBaseTagger abbreviationsTagger = new AbbreviationsTagger(config, fp.Xml);
                    ////    abbreviationsTagger.Tag();
                    ////    fp.Xml = abbreviationsTagger.Xml;
                    ////}

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

                    ////{
                    ////    config.EnvoResponseOutputXmlFileName = @"C:\temp\envo-out.xml";
                    ////    Envo envo = new Envo(config, fp.Xml);
                    ////    envo.Tag(xpathProvider);
                    ////    fp.Xml = envo.Xml;
                    ////}

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

                        try
                        {
                            Codes codes = new Codes(config, fp.Xml);
                            codes.TagInstitutions(xpathProvider, dataProvider);
                            codes.TagInstitutionalCodes(xpathProvider, dataProvider);
                            ////codes.TagSpecimenCodes(xpathProvider);

                            fp.Xml = codes.Xml;
                        }
                        catch
                        {
                            throw;
                        }
                    }

                    ////fp.XmlDocument.ClearTagsInWrongPositions();
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, 0);
                }

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

                try
                {
                    IBaseTagger coordinatesTagger = new CoordinatesTagger(config, fp.Xml);
                    coordinatesTagger.Tag();
                    fp.Xml = coordinatesTagger.Xml;
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, 0);
                }

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

                try
                {
                    IXPathProvider xpathProvider = new XPathProvider(config);
                    IBaseTagger datesTagger = new DatesTagger(config, fp.Xml);
                    datesTagger.Tag(xpathProvider);
                    fp.Xml = datesTagger.Xml;
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, 0);
                }

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

                try
                {
                    DoiLinksTagger linksTagger = new DoiLinksTagger(config, fp.Xml);

                    linksTagger.Tag();

                    fp.Xml = linksTagger.Xml;
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, 0);
                }

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

                try
                {
                    IXPathProvider xpathProvider = new XPathProvider(config);
                    Envo envo = new Envo(config, fp.Xml);
                    envo.Tag(xpathProvider);
                    fp.Xml = envo.Xml;
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, 1);
                }

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

                try
                {
                    Environments environments = new Environments(config, fp.Xml);
                    environments.Tag();
                    fp.Xml = environments.Xml;
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, 0);
                }

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

                try
                {
                    IXPathProvider xpathProvider = new XPathProvider(config);
                    QuantitiesTagger quantitiesTagger = new QuantitiesTagger(config, fp.Xml);
                    quantitiesTagger.TagQuantities(xpathProvider);
                    quantitiesTagger.TagDeviation(xpathProvider);
                    quantitiesTagger.TagAltitude(xpathProvider);
                    fp.Xml = quantitiesTagger.Xml;
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, 0);
                }

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

            try
            {
                if (parseBySection)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.PreserveWhitespace = true;
                    XmlNamespaceManager namespaceManager = Config.TaxPubNamespceManager(xmlDocument);

                    xmlDocument.LoadXml(fp.Xml);

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

                    fp.Xml = xmlDocument.OuterXml;
                }
                else
                {
                    fp.Xml = TagReferences(fp.Xml, fp.OutputFileName);
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0);
            }
        }

        private static void TagWebLinks(FileProcessor fp)
        {
            if (tagWWW)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tTag web links.\n");

                try
                {
                    IBaseTagger linksTagger = new UrlLinksTagger(config, fp.Xml);
                    linksTagger.Tag();
                    fp.Xml = linksTagger.Xml;
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, 0);
                }

                PrintElapsedTime(timer);
            }
        }

        private static void ZooBankCloneJson(FileProcessor fp)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Alert.Log("\n\tZoobank clone JSON.\n");
            if (arguments.Count > 2)
            {
                try
                {
                    string jsonStringContent = FileProcessor.ReadFileContentToString(queryFileName);
                    IBaseCloner zoobankCloner = new ZoobankJsonCloner(jsonStringContent, fp.Xml);
                    zoobankCloner.Clone();
                    fp.Xml = zoobankCloner.Xml;
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, 1);
                }
            }

            PrintElapsedTime(timer);
        }

        private static void ZooBankCloneXml(FileProcessor fp)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Alert.Log("\n\tZoobank clone XML.\n");
            if (arguments.Count > 2)
            {
                try
                {
                    FileProcessor fileProcessorNlm = new FileProcessor(config, queryFileName, outputFileName);
                    fileProcessorNlm.Read();
                    IBaseCloner zoobankCloner = new ZoobankXmlCloner(fileProcessorNlm.Xml, fp.Xml);
                    zoobankCloner.Clone();
                    fp.Xml = zoobankCloner.Xml;
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, 1);
                }
            }

            PrintElapsedTime(timer);
        }

        private static void ZooBankGenerateRegistrationXml(FileProcessor fp)
        {
            try
            {
                IBaseGenerator zoobankRegistrationXmlGenerator = new ZoobankRegistrationXmlGenerator(config, fp.Xml);
                zoobankRegistrationXmlGenerator.Generate();
                fp.Xml = zoobankRegistrationXmlGenerator.Xml;
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 1);
            }
        }
    }
}