using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using ProcessingTools.Base;
using ProcessingTools.Base.Taxonomy;

namespace ProcessingTools.Tag
{
    public partial class Tagger
    {
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
            xmlContent = TagLowerTaxa(xmlContent);
            xmlContent = TagHigherTaxa(xmlContent);
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
                HigherTaxaParser parser = new HigherTaxaParser(config, xmlContent);

                parser.Parse(true, false, false, false, false);

                if (splitHigherWithAphia)
                {
                    Alert.Log("\n\tSplit higher taxa using Aphia API\n");
                    parser.Parse(false, true, false, false, false);
                }

                if (splitHigherWithCoL)
                {
                    Alert.Log("\n\tSplit higher taxa using CoL API\n");
                    parser.Parse(false, false, true, false, false);
                }

                if (splitHigherWithGbif)
                {
                    Alert.Log("\n\tSplit higher taxa using GBIF API\n");
                    parser.Parse(false, false, false, true, false);
                }

                if (splitHigherBySuffix)
                {
                    Alert.Log("\n\tSplit higher taxa by suffix\n");
                    parser.Parse(false, false, false, false, true);
                }

                if (splitHigherAboveGenus)
                {
                    Alert.Log("\n\tMake higher taxa of type 'above-genus'\n");
                    parser.Parse(false, false, false, false, false, true);
                }

                xmlContent = parser.Xml;
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
                TaxaTagger tagger = new TaxaTagger(config, xmlContent);

                tagger.TagHigherTaxa();

                tagger.UntagTaxa();

                xmlContent = tagger.Xml;
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
                LowerTaxaParser parser = new LowerTaxaParser(config, xmlContent);

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
                TaxaTagger tagger = new TaxaTagger(config, xmlContent);

                tagger.TagLowerTaxa(true);

                tagger.UntagTaxa();

                xmlContent = tagger.Xml;
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
                TaxaTagger tagger = new TaxaTagger(config, xmlContent);

                tagger.ParseTreatmentMetaWithAphia();

                xmlContent = tagger.Xml;
                PrintElapsedTime(timer);
            }

            if (parseTreatmentMetaWithGbif)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tParse treatment meta with GBIF.\n");
                TaxaTagger tagger = new TaxaTagger(config, xmlContent);

                tagger.ParseTreatmentMetaWithGbif();

                xmlContent = tagger.Xml;
                PrintElapsedTime(timer);
            }

            if (parseTreatmentMetaWithCol)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tParse treatment meta with CoL.\n");
                TaxaTagger tagger = new TaxaTagger(config, xmlContent);

                tagger.ParseTreatmentMetaWithCoL();

                xmlContent = tagger.Xml;
                PrintElapsedTime(timer);
            }

            return xmlContent;
        }

        private static string FormatTreatments(string xmlContent)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Alert.Log("\n\tFormat treatments.\n");
            TaxaTagger tagger = new TaxaTagger(config, xmlContent);

            tagger.FormatTreatments();

            xmlContent = tagger.Xml;
            PrintElapsedTime(timer);
            return xmlContent;
        }

        private static string RemoveAllTaxaTags(string xmlContent)
        {
            LowerTaxaParser parser = new LowerTaxaParser(config, xmlContent);
            parser.XmlDocument.RemoveTaxonNamePartTags();
            xmlContent = parser.Xml;
            return xmlContent;
        }

        private static void ExtractTaxa(string xmlContent)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xmlContent);
            List<string> taxaList;

            if (extractTaxa)
            {
                Alert.Log("\n\t\tExtract all taxa\n");
                taxaList = Taxonomy.ExtractTaxa(xdoc, true);
                foreach (string taxon in taxaList)
                {
                    Alert.Log(taxon);
                }
            }

            if (extractLowerTaxa)
            {
                Alert.Log("\n\t\tExtract lower taxa\n");
                taxaList = Taxonomy.ExtractTaxa(xdoc, true, TaxaType.Lower);
                foreach (string taxon in taxaList)
                {
                    Alert.Log(taxon);
                }
            }

            if (extractHigherTaxa)
            {
                Alert.Log("\n\t\tExtract higher taxa\n");
                taxaList = Taxonomy.ExtractTaxa(xdoc, true, TaxaType.Higher);
                foreach (string taxon in taxaList)
                {
                    Alert.Log(taxon);
                }
            }
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
