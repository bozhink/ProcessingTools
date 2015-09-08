using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using ProcessingTools.Base;
using ProcessingTools.Base.Taxonomy;
using System;

namespace ProcessingTools.Tag
{
    public partial class Tagger
    {
        private static TaxonomicBlackList blackList;
        private static TaxonomicWhiteList whiteList;

        private static string MainProcessing(string xml)
        {
            string xmlContent = xml;

            if (tagFigTab)
            {
                xmlContent = TagFloats(xmlContent);
            }

            if (tagTableFn)
            {
                xmlContent = TagTableFootnote(xmlContent);
            }

            /*
             * Taxonomic part
             */

            blackList = new TaxonomicBlackList(config);
            whiteList = new TaxonomicWhiteList(config);

            xmlContent = TagLowerTaxa(xmlContent);
            xmlContent = TagHigherTaxa(xmlContent);

            blackList.Clear();
            whiteList.Clear();

            xmlContent = ParseLowerTaxa(xmlContent);
            xmlContent = ParseHigherTaxa(xmlContent);

            if (taxaE || flag1 || flag2 || flag3 || flag4 || flag5 || flag6 || flag7 || flag8)
            {
                xmlContent = ExpandTaxa(xmlContent);
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
            if (extractTaxa || extractLowerTaxa || extractHigherTaxa)
            {
                ExtractTaxa(xmlContent);
            }

            if (validateTaxa)
            {
                try
                {
                    ValidateTaxa(xmlContent);
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, 0);
                }
            }

            if (untagSplit)
            {
                xmlContent = RemoveAllTaxaTags(xmlContent);
            }

            if (formatTreat)
            {
                xmlContent = FormatTreatments(xmlContent);
            }

            xmlContent = ParseTreatmentMeta(xmlContent);

            return xmlContent;
        }

        private static string ParseHigherTaxa(string xmlContent)
        {
            if (taxaD)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tParse higher taxa.\n");

                {
                    IBaseParser parser = new LocalDataBaseHigherTaxaParser(config, xmlContent);
                    parser.Parse();
                    parser.XmlDocument.PrintNonParsedTaxa();
                    xmlContent = parser.Xml;
                }

                if (splitHigherWithAphia)
                {
                    Alert.Log("\n\tSplit higher taxa using Aphia API\n");
                    IBaseParser parser = new AphiaHigherTaxaParser(config, xmlContent);
                    parser.Parse();
                    parser.XmlDocument.PrintNonParsedTaxa();
                    xmlContent = parser.Xml;
                }

                if (splitHigherWithCoL)
                {
                    Alert.Log("\n\tSplit higher taxa using CoL API\n");
                    IBaseParser parser = new CoLHigherTaxaParser(config, xmlContent);
                    parser.Parse();
                    parser.XmlDocument.PrintNonParsedTaxa();
                    xmlContent = parser.Xml;
                }

                if (splitHigherWithGbif)
                {
                    Alert.Log("\n\tSplit higher taxa using GBIF API\n");
                    IBaseParser parser = new GbifHigherTaxaParser(config, xmlContent);
                    parser.Parse();
                    parser.XmlDocument.PrintNonParsedTaxa();
                    xmlContent = parser.Xml;
                }

                if (splitHigherBySuffix)
                {
                    Alert.Log("\n\tSplit higher taxa by suffix\n");
                    IBaseParser parser = new SuffixHigherTaxaParser(config, xmlContent);
                    parser.Parse();
                    parser.XmlDocument.PrintNonParsedTaxa();
                    xmlContent = parser.Xml;
                }

                if (splitHigherAboveGenus)
                {
                    Alert.Log("\n\tMake higher taxa of type 'above-genus'\n");
                    IBaseParser parser = new AboveGenusHigherTaxaParser(config, xmlContent);
                    parser.Parse();
                    parser.XmlDocument.PrintNonParsedTaxa();
                    xmlContent = parser.Xml;
                }

                PrintElapsedTime(timer);
            }

            return xmlContent;
        }

        private static string TagHigherTaxa(string xmlContent)
        {
            if (taxaB)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tTag higher taxa.\n");

                try
                {
                    IBaseTagger tagger = new HigherTaxaTagger(config, xmlContent, whiteList, blackList);

                    tagger.Tag();

                    xmlContent = tagger.Xml;
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, 0);
                }

                PrintElapsedTime(timer);
            }

            return xmlContent;
        }

        private static string ParseLowerTaxa(string xmlContent)
        {
            if (taxaC)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tParse lower taxa.\n");
                IBaseParser parser = new LowerTaxaParser(config, xmlContent);

                parser.Parse();

                xmlContent = parser.Xml;
                PrintElapsedTime(timer);
            }

            return xmlContent;
        }

        private static string TagLowerTaxa(string xmlContent)
        {
            if (taxaA)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tTag lower taxa.\n");
                try
                {
                    IBaseTagger tagger = new LowerTaxaTagger(config, xmlContent, whiteList, blackList);

                    tagger.Tag();

                    xmlContent = tagger.Xml;
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, 0);
                }

                PrintElapsedTime(timer);
            }

            return xmlContent;
        }

        private static string ParseTreatmentMeta(string xmlContent)
        {
            if (parseTreatmentMetaWithAphia)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tParse treatment meta with Aphia.\n");

                IBaseParser parser = new AphiaTreatmentMetaParser(config, xmlContent);
                parser.Parse();
                xmlContent = parser.Xml;

                PrintElapsedTime(timer);
            }

            if (parseTreatmentMetaWithGbif)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tParse treatment meta with GBIF.\n");

                IBaseParser parser = new GbifTreatmentMetaParser(config, xmlContent);
                parser.Parse();
                xmlContent = parser.Xml;

                PrintElapsedTime(timer);
            }

            if (parseTreatmentMetaWithCol)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tParse treatment meta with CoL.\n");

                IBaseParser parser = new CoLTreatmentMetaParser(config, xmlContent);
                parser.Parse();
                xmlContent = parser.Xml;

                PrintElapsedTime(timer);
            }

            return xmlContent;
        }

        private static string FormatTreatments(string xmlContent)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Alert.Log("\n\tFormat treatments.\n");

            IBaseFormatter formatter = new TreatmentFormatter(config, xmlContent);
            formatter.Format();
            xmlContent = formatter.Xml;

            PrintElapsedTime(timer);
            return xmlContent;
        }

        private static string RemoveAllTaxaTags(string xmlContent)
        {
            IBaseParser parser = new LowerTaxaParser(config, xmlContent);
            parser.XmlDocument.RemoveTaxonNamePartTags();
            xmlContent = parser.Xml;
            return xmlContent;
        }

        private static void ExtractTaxa(string xmlContent)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlContent);
            IEnumerable<string> taxaList;

            if (extractTaxa)
            {
                Alert.Log("\n\t\tExtract all taxa\n");
                taxaList = xmlDocument.ExtractTaxa(true);
                foreach (string taxon in taxaList)
                {
                    Alert.Log(taxon);
                }
            }

            if (extractLowerTaxa)
            {
                Alert.Log("\n\t\tExtract lower taxa\n");
                taxaList = xmlDocument.ExtractTaxa(true, TaxaType.Lower);
                foreach (string taxon in taxaList)
                {
                    Alert.Log(taxon);
                }
            }

            if (extractHigherTaxa)
            {
                Alert.Log("\n\t\tExtract higher taxa\n");
                taxaList = xmlDocument.ExtractTaxa(true, TaxaType.Higher);
                foreach (string taxon in taxaList)
                {
                    Alert.Log(taxon);
                }
            }
        }

        public static void ValidateTaxa(string xmlContent)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Alert.Log("\n\tTaxa validation using Global names resolver.\n");

            IValidator validator = new TaxonomicNamesValidator(config, xmlContent);

            validator.Validate();

            PrintElapsedTime(timer);
        }

        private static string ExpandTaxa(string xmlContent)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Alert.Log("\n\tExpand taxa.\n");

            Base.Taxonomy.Nlm.Expander expand = new Base.Taxonomy.Nlm.Expander(config, xmlContent);
            Expander exp = new Expander(config, xmlContent);

            for (int i = 0; i < NumberOfExpandingIterations; i++)
            {
                if (taxaE)
                {
                    exp.Xml = expand.Xml;
                    exp.StableExpand();
                    expand.Xml = exp.Xml;
                }

                if (flag1)
                {
                    expand.UnstableExpand1();
                }

                if (flag2)
                {
                    expand.UnstableExpand2();
                }

                if (flag3)
                {
                    exp.Xml = expand.Xml;
                    exp.UnstableExpand3();
                    expand.Xml = exp.Xml;
                }

                if (flag4)
                {
                    expand.UnstableExpand4();
                }

                if (flag5)
                {
                    expand.UnstableExpand5();
                }

                if (flag6)
                {
                    expand.UnstableExpand6();
                }

                if (flag7)
                {
                    expand.UnstableExpand7();
                }

                if (flag8)
                {
                    exp.Xml = expand.Xml;
                    exp.UnstableExpand8();
                    expand.Xml = exp.Xml;
                }

                xmlContent = expand.Xml;
                PrintElapsedTime(timer);
            }

            return xmlContent;
        }

        private static string TagTableFootnote(string xmlContent)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Alert.Log("\n\tTag table foot-notes.\n");
            Floats fl = new Floats(config, xmlContent);
            fl.TagTableFootNotes();
            xmlContent = fl.Xml;
            PrintElapsedTime(timer);
            return xmlContent;
        }

        private static string TagFloats(string xmlContent)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Alert.Log("\n\tTag floats.\n");
            Floats fl = new Floats(config, xmlContent);
            fl.TagAllFloats();
            xmlContent = fl.Xml;
            PrintElapsedTime(timer);
            return xmlContent;
        }
    }
}