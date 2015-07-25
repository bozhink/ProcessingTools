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
        private static void ParseCoordinates(FileProcessor fp)
        {
            if (parseCoords)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tParse coordinates.\n");
                Coordinates cd = new Coordinates(fp.Xml);

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
                Coordinates cd = new Coordinates(fp.Xml);

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
                Base.Nlm.LinksTagger ln = new Base.Nlm.LinksTagger(fp.Xml);

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
                Base.Nlm.LinksTagger ln = new Base.Nlm.LinksTagger(fp.Xml);

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
                References refs = new References();
                refs.Xml = fp.Xml;

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
                    Base.Format.NlmSystem.Formatter fmt = new Base.Format.NlmSystem.Formatter();
                    fmt.Xml = XsltOnString.ApplyTransform(config.systemInitialFormatXslPath, fp.GetXmlReader());
                    fmt.InitialFormat();
                    fp.Xml = fmt.Xml;
                }
                else
                {
                    Base.Format.Nlm.Formatter fmt = new Base.Format.Nlm.Formatter();
                    fmt.Xml = XsltOnString.ApplyTransform(config.nlmInitialFormatXslPath, fp.GetXmlReader());
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

                QuantitiesTagger quantitiesTagger = new QuantitiesTagger(fp.Xml);
                quantitiesTagger.TagQuantities();
                quantitiesTagger.TagDirections();
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

                DatesTagger datesTagger = new DatesTagger(fp.Xml);
                datesTagger.TagDates();
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

                AbbreviationsTagger abbreviationsTagger = new AbbreviationsTagger(fp.Xml);
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

                //codes.TagSpecimenCount();

                codes.TagKnownSpecimenCodes();

                codes.TagProducts();
                codes.TagGeonames();
                codes.TagMorphology();

                codes.TagInstitutions();
                codes.TagInstitutionalCodes();
                codes.TagSpecimenCodes();

                fp.Xml = Base.Base.NormalizeSystemToNlmXml(config, codes.Xml);
                PrintElapsedTime(timer);
            }
        }
    }
}
