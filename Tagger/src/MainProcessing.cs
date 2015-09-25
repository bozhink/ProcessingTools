namespace ProcessingTools.MainProgram
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml;
    using BaseLibrary;
    using BaseLibrary.Floats;
    using BaseLibrary.Taxonomy;

    public partial class MainProcessingTool
    {
        private static TaxonomicBlackList blackList;
        private static TaxonomicWhiteList whiteList;

        public static void ValidateTaxa(string xmlContent)
        {
            if (validateTaxa)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tTaxa validation using Global names resolver.\n");

                try
                {
                    IValidator validator = new TaxonomicNamesValidator(config, xmlContent, consoleLogger);
                    validator.Validate();
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

                PrintElapsedTime(timer);
            }
        }

        private static string ExpandTaxa(string xmlContent)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            consoleLogger.Log("\n\tExpand taxa.\n");

            try
            {
                BaseLibrary.Taxonomy.Nlm.Expander expand = new BaseLibrary.Taxonomy.Nlm.Expander(config, xmlContent, consoleLogger);
                Expander exp = new Expander(config, xmlContent, consoleLogger);

                for (int i = 0; i < NumberOfExpandingIterations; ++i)
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
            }
            catch (Exception e)
            {
                consoleLogger.LogException(e, string.Empty);
            }

            return xmlContent;
        }

        private static void ExtractTaxa(string xmlContent)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlContent);
                IEnumerable<string> taxaList;

                if (extractTaxa)
                {
                    consoleLogger.Log("\n\t\tExtract all taxa\n");
                    taxaList = xmlDocument.ExtractTaxa(true);
                    foreach (string taxon in taxaList)
                    {
                        consoleLogger.Log(taxon);
                    }
                }

                if (extractLowerTaxa)
                {
                    consoleLogger.Log("\n\t\tExtract lower taxa\n");
                    taxaList = xmlDocument.ExtractTaxa(true, TaxaType.Lower);
                    foreach (string taxon in taxaList)
                    {
                        consoleLogger.Log(taxon);
                    }
                }

                if (extractHigherTaxa)
                {
                    consoleLogger.Log("\n\t\tExtract higher taxa\n");
                    taxaList = xmlDocument.ExtractTaxa(true, TaxaType.Higher);
                    foreach (string taxon in taxaList)
                    {
                        consoleLogger.Log(taxon);
                    }
                }
            }
            catch (Exception e)
            {
                consoleLogger.LogException(e, string.Empty);
            }
        }

        private static string FormatTreatments(string xmlContent)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            consoleLogger.Log("\n\tFormat treatments.\n");

            try
            {
                IBaseFormatter formatter = new TreatmentFormatter(config, xmlContent, consoleLogger);
                formatter.Format();
                xmlContent = formatter.Xml;
            }
            catch (Exception e)
            {
                consoleLogger.LogException(e, string.Empty);
            }

            PrintElapsedTime(timer);
            return xmlContent;
        }

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

            ValidateTaxa(xmlContent);

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
                consoleLogger.Log("\n\tParse higher taxa.\n");

                try
                {
                    IBaseParser parser = new LocalDataBaseHigherTaxaParser(config, xmlContent, consoleLogger);
                    parser.Parse();
                    parser.XmlDocument.PrintNonParsedTaxa(consoleLogger);
                    xmlContent = parser.Xml;
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

                if (splitHigherWithAphia)
                {
                    consoleLogger.Log("\n\tSplit higher taxa using Aphia API\n");

                    try
                    {
                        IBaseParser parser = new AphiaHigherTaxaParser(config, xmlContent, consoleLogger);
                        parser.Parse();
                        parser.XmlDocument.PrintNonParsedTaxa(consoleLogger);
                        xmlContent = parser.Xml;
                    }
                    catch (Exception e)
                    {
                        consoleLogger.LogException(e, string.Empty);
                    }
                }

                if (splitHigherWithCoL)
                {
                    consoleLogger.Log("\n\tSplit higher taxa using CoL API\n");

                    try
                    {
                        IBaseParser parser = new CoLHigherTaxaParser(config, xmlContent, consoleLogger);
                        parser.Parse();
                        parser.XmlDocument.PrintNonParsedTaxa(consoleLogger);
                        xmlContent = parser.Xml;
                    }
                    catch (Exception e)
                    {
                        consoleLogger.LogException(e, string.Empty);
                    }
                }

                if (splitHigherWithGbif)
                {
                    consoleLogger.Log("\n\tSplit higher taxa using GBIF API\n");

                    try
                    {
                        IBaseParser parser = new GbifHigherTaxaParser(config, xmlContent, consoleLogger);
                        parser.Parse();
                        parser.XmlDocument.PrintNonParsedTaxa(consoleLogger);
                        xmlContent = parser.Xml;
                    }
                    catch (Exception e)
                    {
                        consoleLogger.LogException(e, string.Empty);
                    }
                }

                if (splitHigherBySuffix)
                {
                    consoleLogger.Log("\n\tSplit higher taxa by suffix\n");

                    try
                    {
                        IBaseParser parser = new SuffixHigherTaxaParser(config, xmlContent, consoleLogger);
                        parser.Parse();
                        parser.XmlDocument.PrintNonParsedTaxa(consoleLogger);
                        xmlContent = parser.Xml;
                    }
                    catch (Exception e)
                    {
                        consoleLogger.LogException(e, string.Empty);
                    }
                }

                if (splitHigherAboveGenus)
                {
                    consoleLogger.Log("\n\tMake higher taxa of type 'above-genus'\n");

                    try
                    {
                        IBaseParser parser = new AboveGenusHigherTaxaParser(config, xmlContent, consoleLogger);
                        parser.Parse();
                        parser.XmlDocument.PrintNonParsedTaxa(consoleLogger);
                        xmlContent = parser.Xml;
                    }
                    catch (Exception e)
                    {
                        consoleLogger.LogException(e, string.Empty);
                    }
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
                consoleLogger.Log("\n\tParse lower taxa.\n");

                try
                {
                    IBaseParser parser = new LowerTaxaParser(config, xmlContent, consoleLogger);
                    parser.Parse();
                    xmlContent = parser.Xml;
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
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
                consoleLogger.Log("\n\tParse treatment meta with Aphia.\n");

                try
                {
                    IBaseParser parser = new AphiaTreatmentMetaParser(config, xmlContent, consoleLogger);
                    parser.Parse();
                    xmlContent = parser.Xml;
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

                PrintElapsedTime(timer);
            }

            if (parseTreatmentMetaWithGbif)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tParse treatment meta with GBIF.\n");

                try
                {
                    IBaseParser parser = new GbifTreatmentMetaParser(config, xmlContent, consoleLogger);
                    parser.Parse();
                    xmlContent = parser.Xml;
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

                PrintElapsedTime(timer);
            }

            if (parseTreatmentMetaWithCol)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tParse treatment meta with CoL.\n");

                try
                {
                    IBaseParser parser = new CoLTreatmentMetaParser(config, xmlContent, consoleLogger);
                    parser.Parse();
                    xmlContent = parser.Xml;
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

                PrintElapsedTime(timer);
            }

            return xmlContent;
        }

        private static string RemoveAllTaxaTags(string xmlContent)
        {
            try
            {
                IBaseParser parser = new LowerTaxaParser(config, xmlContent, consoleLogger);
                parser.XmlDocument.RemoveTaxonNamePartTags();
                xmlContent = parser.Xml;
            }
            catch (Exception e)
            {
                consoleLogger.LogException(e, string.Empty);
            }

            return xmlContent;
        }

        private static string TagFloats(string xmlContent)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            consoleLogger.Log("\n\tTag floats.\n");

            try
            {
                FloatsTagger fl = new FloatsTagger(config, xmlContent, consoleLogger);
                fl.Tag();
                xmlContent = fl.Xml;
            }
            catch (Exception e)
            {
                consoleLogger.LogException(e, string.Empty);
            }

            PrintElapsedTime(timer);
            return xmlContent;
        }

        private static string TagHigherTaxa(string xmlContent)
        {
            if (taxaB)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tTag higher taxa.\n");

                try
                {
                    IBaseTagger tagger = new HigherTaxaTagger(config, xmlContent, whiteList, blackList);
                    tagger.Tag();
                    xmlContent = tagger.Xml.NormalizeXmlToSystemXml(config);
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

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
                consoleLogger.Log("\n\tTag lower taxa.\n");

                try
                {
                    IBaseTagger tagger = new LowerTaxaTagger(config, xmlContent, whiteList, blackList);
                    tagger.Tag();
                    xmlContent = tagger.Xml.NormalizeXmlToSystemXml(config);
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

                PrintElapsedTime(timer);
            }

            return xmlContent;
        }

        private static string TagTableFootnote(string xmlContent)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            consoleLogger.Log("\n\tTag table foot-notes.\n");

            try
            {
                IBaseTagger fl = new TableFootNotesTagger(config, xmlContent, consoleLogger);
                fl.Tag();
                xmlContent = fl.Xml;
            }
            catch (Exception e)
            {
                consoleLogger.LogException(e, string.Empty);
            }

            PrintElapsedTime(timer);
            return xmlContent;
        }
    }
}