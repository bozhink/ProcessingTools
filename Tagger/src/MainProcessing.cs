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
            if (settings.ValidateTaxa)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tTaxa validation using Global names resolver.\n");

                try
                {
                    IValidator validator = new TaxonomicNamesValidator(settings.Config, xmlContent, consoleLogger);
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
                BaseLibrary.Taxonomy.Nlm.Expander expand = new BaseLibrary.Taxonomy.Nlm.Expander(settings.Config, xmlContent, consoleLogger);
                Expander exp = new Expander(settings.Config, xmlContent, consoleLogger);

                for (int i = 0; i < NumberOfExpandingIterations; ++i)
                {
                    if (settings.TaxaE)
                    {
                        exp.Xml = expand.Xml;
                        exp.StableExpand();
                        expand.Xml = exp.Xml;
                    }

                    if (settings.Flag1)
                    {
                        expand.UnstableExpand1();
                    }

                    if (settings.Flag2)
                    {
                        expand.UnstableExpand2();
                    }

                    if (settings.Flag3)
                    {
                        exp.Xml = expand.Xml;
                        exp.UnstableExpand3();
                        expand.Xml = exp.Xml;
                    }

                    if (settings.Flag4)
                    {
                        expand.UnstableExpand4();
                    }

                    if (settings.Flag5)
                    {
                        expand.UnstableExpand5();
                    }

                    if (settings.Flag6)
                    {
                        expand.UnstableExpand6();
                    }

                    if (settings.Flag7)
                    {
                        expand.UnstableExpand7();
                    }

                    if (settings.Flag8)
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

                if (settings.ExtractTaxa)
                {
                    consoleLogger.Log("\n\t\tExtract all taxa\n");
                    taxaList = xmlDocument.ExtractTaxa(true);
                    foreach (string taxon in taxaList)
                    {
                        consoleLogger.Log(taxon);
                    }
                }

                if (settings.ExtractLowerTaxa)
                {
                    consoleLogger.Log("\n\t\tExtract lower taxa\n");
                    taxaList = xmlDocument.ExtractTaxa(true, TaxaType.Lower);
                    foreach (string taxon in taxaList)
                    {
                        consoleLogger.Log(taxon);
                    }
                }

                if (settings.ExtractHigherTaxa)
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
                IBaseFormatter formatter = new TreatmentFormatter(settings.Config, xmlContent, consoleLogger);
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

            if (settings.TagFigTab)
            {
                xmlContent = TagFloats(xmlContent);
            }

            if (settings.TagTableFn)
            {
                xmlContent = TagTableFootnote(xmlContent);
            }

            /*
             * Taxonomic part
             */

            blackList = new TaxonomicBlackList(settings.Config);
            whiteList = new TaxonomicWhiteList(settings.Config);

            xmlContent = TagLowerTaxa(xmlContent);
            xmlContent = TagHigherTaxa(xmlContent);

            blackList.Clear();
            whiteList.Clear();

            xmlContent = ParseLowerTaxa(xmlContent);
            xmlContent = ParseHigherTaxa(xmlContent);

            if (settings.TaxaE || settings.Flag1 || settings.Flag2 || settings.Flag3 || settings.Flag4 || settings.Flag5 || settings.Flag6 || settings.Flag7 || settings.Flag8)
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
            if (settings.ExtractTaxa || settings.ExtractLowerTaxa || settings.ExtractHigherTaxa)
            {
                ExtractTaxa(xmlContent);
            }

            ValidateTaxa(xmlContent);

            if (settings.UntagSplit)
            {
                xmlContent = RemoveAllTaxaTags(xmlContent);
            }

            if (settings.FormatTreat)
            {
                xmlContent = FormatTreatments(xmlContent);
            }

            xmlContent = ParseTreatmentMeta(xmlContent);

            return xmlContent;
        }

        private static string ParseHigherTaxa(string xmlContent)
        {
            if (settings.TaxaD)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tParse higher taxa.\n");

                try
                {
                    IBaseParser parser = new LocalDataBaseHigherTaxaParser(settings.Config, xmlContent, consoleLogger);
                    parser.Parse();
                    parser.XmlDocument.PrintNonParsedTaxa(consoleLogger);
                    xmlContent = parser.Xml;
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

                if (settings.SplitHigherWithAphia)
                {
                    consoleLogger.Log("\n\tSplit higher taxa using Aphia API\n");

                    try
                    {
                        IBaseParser parser = new AphiaHigherTaxaParser(settings.Config, xmlContent, consoleLogger);
                        parser.Parse();
                        parser.XmlDocument.PrintNonParsedTaxa(consoleLogger);
                        xmlContent = parser.Xml;
                    }
                    catch (Exception e)
                    {
                        consoleLogger.LogException(e, string.Empty);
                    }
                }

                if (settings.SplitHigherWithCoL)
                {
                    consoleLogger.Log("\n\tSplit higher taxa using CoL API\n");

                    try
                    {
                        IBaseParser parser = new CoLHigherTaxaParser(settings.Config, xmlContent, consoleLogger);
                        parser.Parse();
                        parser.XmlDocument.PrintNonParsedTaxa(consoleLogger);
                        xmlContent = parser.Xml;
                    }
                    catch (Exception e)
                    {
                        consoleLogger.LogException(e, string.Empty);
                    }
                }

                if (settings.SplitHigherWithGbif)
                {
                    consoleLogger.Log("\n\tSplit higher taxa using GBIF API\n");

                    try
                    {
                        IBaseParser parser = new GbifHigherTaxaParser(settings.Config, xmlContent, consoleLogger);
                        parser.Parse();
                        parser.XmlDocument.PrintNonParsedTaxa(consoleLogger);
                        xmlContent = parser.Xml;
                    }
                    catch (Exception e)
                    {
                        consoleLogger.LogException(e, string.Empty);
                    }
                }

                if (settings.SplitHigherBySuffix)
                {
                    consoleLogger.Log("\n\tSplit higher taxa by suffix\n");

                    try
                    {
                        IBaseParser parser = new SuffixHigherTaxaParser(settings.Config, xmlContent, consoleLogger);
                        parser.Parse();
                        parser.XmlDocument.PrintNonParsedTaxa(consoleLogger);
                        xmlContent = parser.Xml;
                    }
                    catch (Exception e)
                    {
                        consoleLogger.LogException(e, string.Empty);
                    }
                }

                if (settings.SplitHigherAboveGenus)
                {
                    consoleLogger.Log("\n\tMake higher taxa of type 'above-genus'\n");

                    try
                    {
                        IBaseParser parser = new AboveGenusHigherTaxaParser(settings.Config, xmlContent, consoleLogger);
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
            if (settings.TaxaC)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tParse lower taxa.\n");

                try
                {
                    IBaseParser parser = new LowerTaxaParser(settings.Config, xmlContent, consoleLogger);
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
            if (settings.ParseTreatmentMetaWithAphia)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tParse treatment meta with Aphia.\n");

                try
                {
                    IBaseParser parser = new AphiaTreatmentMetaParser(settings.Config, xmlContent, consoleLogger);
                    parser.Parse();
                    xmlContent = parser.Xml;
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

                PrintElapsedTime(timer);
            }

            if (settings.ParseTreatmentMetaWithGbif)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tParse treatment meta with GBIF.\n");

                try
                {
                    IBaseParser parser = new GbifTreatmentMetaParser(settings.Config, xmlContent, consoleLogger);
                    parser.Parse();
                    xmlContent = parser.Xml;
                }
                catch (Exception e)
                {
                    consoleLogger.LogException(e, string.Empty);
                }

                PrintElapsedTime(timer);
            }

            if (settings.ParseTreatmentMetaWithCol)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tParse treatment meta with CoL.\n");

                try
                {
                    IBaseParser parser = new CoLTreatmentMetaParser(settings.Config, xmlContent, consoleLogger);
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
                IBaseParser parser = new LowerTaxaParser(settings.Config, xmlContent, consoleLogger);
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
                FloatsTagger fl = new FloatsTagger(settings.Config, xmlContent, consoleLogger);
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
            if (settings.TaxaB)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tTag higher taxa.\n");

                try
                {
                    IBaseTagger tagger = new HigherTaxaTagger(settings.Config, xmlContent, whiteList, blackList);
                    tagger.Tag();
                    xmlContent = tagger.Xml.NormalizeXmlToSystemXml(settings.Config);
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
            if (settings.TaxaA)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                consoleLogger.Log("\n\tTag lower taxa.\n");

                try
                {
                    IBaseTagger tagger = new LowerTaxaTagger(settings.Config, xmlContent, whiteList, blackList);
                    tagger.Tag();
                    xmlContent = tagger.Xml.NormalizeXmlToSystemXml(settings.Config);
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
                IBaseTagger fl = new TableFootNotesTagger(settings.Config, xmlContent, consoleLogger);
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