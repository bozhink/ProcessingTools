using System.Diagnostics;
using System.IO;
using ProcessingTools.Base;

namespace ProcessingTools.Tag
{
    public partial class Tagger
    {
        private static void TagEnvo(FileProcessor fp)
        {
            if (tagEnvo)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tUse greek tagger.\n");
                Envo envo = new Envo(config, fp.Xml);

                XPathProvider xpathProvider = new XPathProvider(config);

                envo.Tag(xpathProvider);

                fp.Xml = envo.Xml;
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
                Coordinates cd = new Coordinates(config, fp.Xml);

                cd.ParseCoordinates();

                fp.Xml = cd.Xml;
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
                Coordinates cd = new Coordinates(config, fp.Xml);

                cd.TagCoordinates();

                fp.Xml = cd.Xml;
                PrintElapsedTime(timer);
            }
        }

        private static void TagWebLinks(FileProcessor fp)
        {
            if (tagWWW)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tTag web links.\n");
                Base.Nlm.LinksTagger ln = new Base.Nlm.LinksTagger(config, fp.Xml);

                ln.TagWWW();

                fp.Xml = ln.Xml;
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
                Base.Nlm.LinksTagger ln = new Base.Nlm.LinksTagger(config, fp.Xml);

                ln.TagDOI();
                ln.TagPMCLinks();

                fp.Xml = ln.Xml;
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
                References refs = new References(config, fp.Xml);

                refs.SplitReferences();

                fp.Xml = refs.Xml;
                PrintElapsedTime(timer);
            }
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
                    Base.Format.NlmSystem.Formatter fmt = new Base.Format.NlmSystem.Formatter(config, xml);
                    fmt.InitialFormat();
                    fp.Xml = fmt.Xml;
                }
                else
                {
                    string xml = fp.XmlReader.ApplyXslTransform(config.nlmInitialFormatXslPath);
                    Base.Format.Nlm.Formatter fmt = new Base.Format.Nlm.Formatter(config, xml);
                    fmt.InitialFormat();
                    fp.Xml = fmt.Xml;
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

        private static void TagEnvoTerms(FileProcessor fp)
        {
            if (tagEnvironments)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tTag environments.\n");
                Base.Environments environments = new Environments(config, fp.Xml);

                environments.TagEnvironmentsRecords();

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

                XPathProvider xpathProvider = new XPathProvider(config);
                QuantitiesTagger quantitiesTagger = new QuantitiesTagger(config, fp.Xml);
                quantitiesTagger.TagQuantities(xpathProvider);
                quantitiesTagger.TagDeviation(xpathProvider);
                quantitiesTagger.TagAltitude(xpathProvider);
                fp.Xml = quantitiesTagger.Xml;

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

                XPathProvider xpathProvider = new XPathProvider(config);
                DatesTagger datesTagger = new DatesTagger(config, fp.Xml);
                datesTagger.TagDates(xpathProvider);
                fp.Xml = datesTagger.Xml;

                PrintElapsedTime(timer);
            }
        }

        private static void TagAbbreviations(FileProcessor fp)
        {
            if (tagAbbrev)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tTag abbreviations.\n");

                AbbreviationsTagger abbreviationsTagger = new AbbreviationsTagger(config, fp.Xml);
                abbreviationsTagger.TagAbbreviationsInText();
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

                XPathProvider xpathProvider = new XPathProvider(config);

                ////{
                ////    Codes codes = new Codes(config, fp.Xml);
                ////    codes.TagKnownSpecimenCodes(xpathProvider);
                ////    fp.Xml = codes.Xml;
                ////}

                {
                    AbbreviationsTagger abbr = new AbbreviationsTagger(config, fp.Xml);
                    abbr.TagAbbreviationsInText();
                    fp.Xml = abbr.Xml;
                }

                //{
                //    SpecimenCountTagger specimenCountTagger = new SpecimenCountTagger(config, fp.Xml);
                //    specimenCountTagger.TagSpecimenCount(xpathProvider);
                //    fp.Xml = specimenCountTagger.Xml;
                //}

                //{
                //    QuantitiesTagger quantitiesTagger = new QuantitiesTagger(config, fp.Xml);
                //    quantitiesTagger.TagQuantities(xpathProvider);
                //    quantitiesTagger.TagDeviation(xpathProvider);
                //    quantitiesTagger.TagAltitude(xpathProvider);
                //    fp.Xml = quantitiesTagger.Xml;
                //}

                //{
                //    DatesTagger datesTagger = new DatesTagger(config, fp.Xml);
                //    datesTagger.TagDates(xpathProvider);
                //    fp.Xml = datesTagger.Xml;
                //}

                {
                    config.EnvoResponseOutputXmlFileName = @"C:\temp\envo-out.xml";
                    Envo envo = new Envo(config, fp.Xml);
                    envo.Tag(xpathProvider);
                    fp.Xml = envo.Xml;
                }

                using (DataProvider dataProvider = new DataProvider(config, fp.Xml))
                {
                    //{
                    //    ProductsTagger products = new ProductsTagger(config, fp.Xml);
                    //    products.TagProducts(xpathProvider, dataProvider);
                    //    fp.Xml = products.Xml;
                    //}

                    //{
                    //    GeoNamesTagger geonames = new GeoNamesTagger(config, fp.Xml);
                    //    geonames.TagGeonames(xpathProvider, dataProvider);
                    //    fp.Xml = geonames.Xml;
                    //}

                    //{
                    //    MorphologyTagger morphology = new MorphologyTagger(config, fp.Xml);
                    //    morphology.TagMorphology(xpathProvider, dataProvider);
                    //    fp.Xml = morphology.Xml;
                    //}

                    {
                        Codes codes = new Codes(config, fp.Xml);
                        codes.TagInstitutions(xpathProvider, dataProvider);
                        codes.TagInstitutionalCodes(xpathProvider, dataProvider);
                        //codes.TagSpecimenCodes(xpathProvider);

                        fp.Xml = codes.Xml;
                    }
                }

                //fp.XmlDocument.ClearTagsInWrongPositions();
                
                PrintElapsedTime(timer);
            }
        }
    }
}
