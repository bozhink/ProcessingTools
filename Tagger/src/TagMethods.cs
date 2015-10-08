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
                FileProcessor flp = new FileProcessor(settings.Config, settings.InputFileName, settings.Config.floraExtractedTaxaListPath);
                FileProcessor flpp = new FileProcessor(settings.Config, settings.InputFileName, settings.Config.floraExtractTaxaPartsOutputPath);
                Flora fl = new Flora(settings.Config, fp.Xml);

                fl.ExtractTaxa();
                fl.DistinctTaxa();
                fl.GenerateTagTemplate();

                flp.Xml = fl.Xml;
                flp.Write();

                fl.Xml = fp.Xml;
                if (settings.TaxaA)
                {
                    fl.PerformReplace();
                }

                if (settings.TaxaB)
                {
                    ////fl.TagHigherTaxa();
                }

                if (settings.TaxaC)
                {
                    if (settings.Flag1)
                    {
                        fl.ParseInfra();
                    }

                    if (settings.Flag2)
                    {
                        fl.ParseTn();
                    }

                    if (settings.Flag3)
                    {
                        ////fl.SplitLowerTaxa();
                    }
                }

                fp.Xml = fl.Xml;

                flpp.Xml = fl.ExtractTaxaParts();
                flpp.Write();
            }
            catch
            {
                throw;
            }
        }

        private static void InitialFormat(FileProcessor fp)
        {
            if (settings.FormatInit)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tInitial format.\n");

                try
                {
                    if (!settings.Config.NlmStyle)
                    {
                        string xml = fp.XmlReader.ApplyXslTransform(settings.Config.systemInitialFormatXslPath);
                        BaseLibrary.Format.NlmSystem.Formatter fmt = new BaseLibrary.Format.NlmSystem.Formatter(settings.Config, xml);
                        fmt.Format();
                        fp.Xml = fmt.Xml;
                    }
                    else
                    {
                        string xml = fp.XmlReader.ApplyXslTransform(settings.Config.nlmInitialFormatXslPath);
                        BaseLibrary.Format.Nlm.Formatter fmt = new BaseLibrary.Format.Nlm.Formatter(settings.Config, xml);
                        fmt.Format();
                        fp.Xml = fmt.Xml;
                    }
                }
                catch
                {
                    throw;
                }

                PrintElapsedTime(timer);
            }
        }

        private static void ParseCoordinates(FileProcessor fp)
        {
            if (settings.ParseCoords)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tParse coordinates.\n");

                try
                {
                    IBaseParser cooredinatesParser = new CoordinatesParser(settings.Config, fp.Xml, consoleLogger);
                    cooredinatesParser.Parse();
                    fp.Xml = cooredinatesParser.Xml;
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

                PrintElapsedTime(timer);
            }
        }

        private static void ParseReferences(FileProcessor fp)
        {
            if (settings.ParseReferences)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tParse references.\n");

                try
                {
                    References referencesParser = new References(settings.Config, fp.Xml, consoleLogger);
                    referencesParser.SplitReferences();
                    fp.Xml = referencesParser.Xml;
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

                PrintElapsedTime(timer);
            }
        }

        private static void QuentinSpecific(FileProcessor fp)
        {
            try
            {
                QuentinFlora qf = new QuentinFlora(fp.Xml);
                if (settings.FormatInit)
                {
                    qf.InitialFormat();
                }
                else if (settings.Flag1)
                {
                    qf.Split1();
                }
                else if (settings.Flag2)
                {
                    qf.Split2();
                }
                else
                {
                    qf.FinalFormat();
                }

                fp.Xml = qf.Xml;
            }
            catch
            {
                throw;
            }
        }

        private static void TagAbbreviations(FileProcessor fp)
        {
            if (settings.TagAbbrev)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tTag abbreviations.\n");

                try
                {
                    IBaseTagger abbreviationsTagger = new AbbreviationsTagger(settings.Config, fp.Xml);
                    abbreviationsTagger.Tag();
                    fp.Xml = abbreviationsTagger.Xml;
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

                PrintElapsedTime(timer);
            }
        }

        private static void TagCodes(FileProcessor fp)
        {
            if (settings.TagCodes)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tTag codes.\n");

                try
                {
                    IXPathProvider xpathProvider = new XPathProvider(settings.Config);

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

                    using (DataProvider dataProvider = new DataProvider(settings.Config, fp.Xml))
                    {
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
                            Codes codes = new Codes(settings.Config, fp.Xml, consoleLogger);
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
                    consoleLogger.LogException(e, string.Empty);
                }

                PrintElapsedTime(timer);
            }
        }

        private static void TagCoordinates(FileProcessor fp)
        {
            if (settings.TagCoords)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tTag coordinates.\n");

                try
                {
                    IBaseTagger coordinatesTagger = new CoordinatesTagger(settings.Config, fp.Xml);
                    coordinatesTagger.Tag();
                    fp.Xml = coordinatesTagger.Xml;
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

                PrintElapsedTime(timer);
            }
        }

        private static void TagDates(FileProcessor fp)
        {
            if (settings.TagDates)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tTag dates.\n");

                try
                {
                    IXPathProvider xpathProvider = new XPathProvider(settings.Config);
                    IBaseTagger datesTagger = new DatesTagger(settings.Config, fp.Xml);
                    datesTagger.Tag(xpathProvider);
                    fp.Xml = datesTagger.Xml;
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

                PrintElapsedTime(timer);
            }
        }

        private static void TagDoi(FileProcessor fp)
        {
            if (settings.TagDoi)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tTag DOI.\n");

                try
                {
                    DoiLinksTagger linksTagger = new DoiLinksTagger(settings.Config, fp.Xml);

                    linksTagger.Tag();

                    fp.Xml = linksTagger.Xml;
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

                PrintElapsedTime(timer);
            }
        }

        private static void TagEnvo(FileProcessor fp)
        {
            if (settings.TagEnvo)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tUse greek tagger.\n");

                try
                {
                    IXPathProvider xpathProvider = new XPathProvider(settings.Config);
                    Envo envo = new Envo(settings.Config, fp.Xml);
                    envo.Tag(xpathProvider);
                    fp.Xml = envo.Xml;
                }
                catch
                {
                    throw;
                }

                PrintElapsedTime(timer);
            }
        }

        private static void TagEnvoTerms(FileProcessor fp)
        {
            if (settings.TagEnvironments)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tTag environments.\n");

                try
                {
                    Environments environments = new Environments(settings.Config, fp.Xml);
                    environments.Tag();
                    fp.Xml = environments.Xml;
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

                PrintElapsedTime(timer);
            }
        }

        private static void TagQuantities(FileProcessor fp)
        {
            if (settings.TagQuantities)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tTag quantities.\n");

                try
                {
                    IXPathProvider xpathProvider = new XPathProvider(settings.Config);
                    QuantitiesTagger quantitiesTagger = new QuantitiesTagger(settings.Config, fp.Xml, consoleLogger);
                    quantitiesTagger.TagQuantities(xpathProvider);
                    quantitiesTagger.TagDeviation(xpathProvider);
                    quantitiesTagger.TagAltitude(xpathProvider);
                    fp.Xml = quantitiesTagger.Xml;
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

                PrintElapsedTime(timer);
            }
        }

        private static string TagReferences(string xml, string fileName)
        {
            string xmlContent = xml;
            References refs = new References(settings.Config, xmlContent, consoleLogger);

            settings.Config.referencesGetReferencesXmlPath = Path.GetDirectoryName(fileName) + "\\zzz-" +
                Path.GetFileNameWithoutExtension(fileName) + "-references.xml";
            settings.Config.referencesTagTemplateXmlPath = settings.Config.tempDirectoryPath + "\\zzz-" +
                Path.GetFileNameWithoutExtension(fileName) + "-references-tag-template.xml";

            refs.GenerateTagTemplateXml();
            refs.TagReferences();
            xmlContent = refs.Xml;
            return xmlContent;
        }

        private static void TagReferences(FileProcessor fp)
        {
            consoleLogger.Log("\n\tTag references.\n");

            try
            {
                if (settings.ParseBySection)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.PreserveWhitespace = true;
                    XmlNamespaceManager namespaceManager = Config.TaxPubNamespceManager(xmlDocument);

                    xmlDocument.LoadXml(fp.Xml);

                    foreach (XmlNode node in xmlDocument.SelectNodes(settings.HigherStructrureXpath, namespaceManager))
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

                        consoleLogger.Log(templateFileName);

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
                consoleLogger.LogException(e, string.Empty);
            }
        }

        private static void TagWebLinks(FileProcessor fp)
        {
            if (settings.TagWWW)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tTag web links.\n");

                try
                {
                    IBaseTagger linksTagger = new UrlLinksTagger(settings.Config, fp.Xml);
                    linksTagger.Tag();
                    fp.Xml = linksTagger.Xml;
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

                PrintElapsedTime(timer);
            }
        }

        private static void ZooBankCloneJson(FileProcessor fp)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            consoleLogger.Log("\n\tZoobank clone JSON.\n");
            if (settings.QueryFileName != null)
            {
                try
                {
                    string jsonStringContent = FileProcessor.ReadFileContentToString(settings.QueryFileName);
                    IBaseCloner zoobankCloner = new ZoobankJsonCloner(jsonStringContent, fp.Xml, consoleLogger);
                    zoobankCloner.Clone();
                    fp.Xml = zoobankCloner.Xml;
                }
                catch
                {
                    throw;
                }
            }

            PrintElapsedTime(timer);
        }

        private static void ZooBankCloneXml(FileProcessor fp)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            consoleLogger.Log("\n\tZoobank clone XML.\n");
            if (settings.QueryFileName != null)
            {
                try
                {
                    FileProcessor fileProcessorNlm = new FileProcessor(settings.Config, settings.QueryFileName, settings.OutputFileName);
                    fileProcessorNlm.Read();
                    IBaseCloner zoobankCloner = new ZoobankXmlCloner(fileProcessorNlm.Xml, fp.Xml, consoleLogger);
                    zoobankCloner.Clone();
                    fp.Xml = zoobankCloner.Xml;
                }
                catch
                {
                    throw;
                }
            }

            PrintElapsedTime(timer);
        }

        private static void ZooBankGenerateRegistrationXml(FileProcessor fp)
        {
            try
            {
                IBaseGenerator zoobankRegistrationXmlGenerator = new ZoobankRegistrationXmlGenerator(settings.Config, fp.Xml);
                zoobankRegistrationXmlGenerator.Generate();
                fp.Xml = zoobankRegistrationXmlGenerator.Xml;
            }
            catch
            {
                throw;
            }
        }
    }
}