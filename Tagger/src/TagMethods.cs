using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using ProcessingTools.Base;
using ProcessingTools.Base.Taxonomy;
using ProcessingTools.Base.ZooBank;

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

                envo.Tag();

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
                    string xml = XsltOnString.ApplyTransform(config.systemInitialFormatXslPath, fp.GetXmlReader());
                    Base.Format.NlmSystem.Formatter fmt = new Base.Format.NlmSystem.Formatter(config, xml);
                    fmt.InitialFormat();
                    fp.Xml = fmt.Xml;
                }
                else
                {
                    string xml = XsltOnString.ApplyTransform(config.nlmInitialFormatXslPath, fp.GetXmlReader());
                    Base.Format.Nlm.Formatter fmt = new Base.Format.Nlm.Formatter(config, xml);
                    fmt.InitialFormat();
                    fp.Xml = Base.Base.NormalizeSystemToNlmXml(config, fmt.Xml);
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
                quantitiesTagger.TagDirections(xpathProvider);
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
                Codes codes = new Codes(config, Base.Base.NormalizeNlmToSystemXml(config, fp.Xml));

                //Alert.Log("1");

                using (DataProvider dataProvider = new DataProvider(config, codes.Xml))
                {
                    //Alert.Log("2");
                    XPathProvider xpathProvider = new XPathProvider(config);

                    {
                        SpecimenCountTagger specimenCounter = new SpecimenCountTagger(config, fp.Xml);
                        specimenCounter.TagSpecimenCount(xpathProvider);
                        codes.Xml = specimenCounter.Xml;
                    }

                    //Alert.Log("3");
                    codes.TagKnownSpecimenCodes(xpathProvider);

                    //Alert.Log("4");
                    {
                        ProductsTagger products = new ProductsTagger(config, codes.Xml);
                        //Alert.Log("5");
                        products.TagProducts(xpathProvider, dataProvider);
                        //Alert.Log("6");
                        codes.Xml = products.Xml;
                    }

                    //Alert.Log("7");
                    {
                        GeoNamesTagger geonames = new GeoNamesTagger(config, codes.Xml);
                        geonames.TagGeonames(xpathProvider, dataProvider);
                        codes.Xml = geonames.Xml;
                    }

                    //Alert.Log("8");
                    {
                        MorphologyTagger morphology = new MorphologyTagger(config, codes.Xml);
                        morphology.TagMorphology(xpathProvider, dataProvider);
                        codes.Xml = morphology.Xml;
                    }

                    //Alert.Log("9");
                    codes.TagInstitutions(xpathProvider, dataProvider);

                    //Alert.Log("9");
                    codes.TagInstitutionalCodes(xpathProvider, dataProvider);

                    //Alert.Log("10");
                    codes.TagSpecimenCodes(xpathProvider);

                    //Alert.Log("11");
                    dataProvider.Dispose();
                    //Alert.Log("12");
                }

                fp.Xml = Base.Base.NormalizeSystemToNlmXml(config, codes.Xml);
                PrintElapsedTime(timer);
            }
        }
    }
}
