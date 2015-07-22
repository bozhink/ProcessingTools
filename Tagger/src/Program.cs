using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Base;
using Base.Taxonomy;
using Base.ZooBank;
using Tag;

namespace Tag
{
    public partial class Tagger
    {
        public static void Main(string[] args)
        {
            /*
             * Parse config file
             */
            config = ConfigBuilder.CreateConfig("C:\\bin\\config.json");
            config.NlmStyle = true;
            config.TagWholeDocument = false;

            /*
             * Initial check of input parameters
             */
            for (int i = 0; i < args.Length; i++)
            {
                char[] arg = args[i].ToCharArray();
                if (arg[0] == '-' && arg[1] == '-')
                {
                    // Double dashed options
                    doubleDashedOptions.Add(i);
                }
                else if (arg[0] == '-' || arg[0] == '/')
                {
                    // Single dashed options
                    singleDashedOptions.Add(i);
                }
                else
                {
                    arguments.Add(i);
                }
            }

            Timer allTime = new Timer();

            ParseFileNames(args);
            ParseSingleDashedOptions(args);
            ParseDoubleDashedOptions(args);

            /*
             * Main processing part
             */
            FileProcessor fp = new FileProcessor(inputFileName, outputFileName);
            fp.ReadStringContent(true);

            InitialFormat(fp);

            ParseReferences(fp);

            TagDoi(fp);
            TagWebLinks(fp);

            TagCoordinates(fp);
            ParseCoordinates(fp);

            TagEnvoTerms(fp);
            TagQuantities(fp);
            TagDates(fp);
            TagAbbreviations(fp);

            TagCodes(fp);

            if (nlm)
            {
                Timer timer = new Timer();
                timer.Start();
                Alert.Message("\n\tFormat NLM xml. [obsolete]\n");
                Base.Format.Nlm.Nlm fpnlm = new Base.Format.Nlm.Nlm();
                fpnlm.Xml = fp.Xml;
                fpnlm.Format();
                fp.Xml = fpnlm.Xml;
                timer.WriteOutput();
            }
            else if (html)
            {
                Timer timer = new Timer();
                timer.Start();
                Alert.Message("\n\tFormat Html. [obsolete]\n");
                Base.Format.Nlm.Html fphtml = new Base.Format.Nlm.Html();
                fphtml.Xml = fp.Xml;
                fphtml.Format();
                fp.Xml = fphtml.Xml;
                timer.WriteOutput();
            }
            else if (normalizeFlag)
            {
                Timer timer = new Timer();
                timer.Start();
                if (config.NlmStyle)
                {
                    Alert.Message("Input Xml will be normalized to NLM syle.");
                    string xml = fp.Xml;
                    xml = Base.Base.NormalizeSystemToNlmXml(config, xml);
                    fp.Xml = xml;
                }
                else
                {
                    Alert.Message("Input Xml will be normalized to System style.");
                    string xml = fp.Xml;
                    xml = Base.Base.NormalizeNlmToSystemXml(config, xml);
                    fp.Xml = xml;
                }

                timer.WriteOutput();
            }
            else if (zoobank)
            {
                Timer timer = new Timer();
                timer.Start();
                Alert.ZoobankCloneMessage();
                if (arguments.Count > 2)
                {
                    FileProcessor fileProcessorNlm = new FileProcessor(queryFileName, outputFileName);
                    fileProcessorNlm.ReadStringContent(true);
                    ZoobankCloner zb = new ZoobankCloner(fileProcessorNlm.Xml, fp.Xml);
                    zb.Clone();
                    fp.Xml = zb.Xml;
                }

                timer.WriteOutput();
            }
            else if (zoobankJson)
            {
                Timer timer = new Timer();
                timer.Start();
                Alert.ZoobankCloneMessage();
                if (arguments.Count > 2)
                {
                    FileProcessor fileProcessorJson = new FileProcessor(queryFileName, outputFileName);
                    fileProcessorJson.ReadStringContent();
                    ZoobankCloner zb = new ZoobankCloner();
                    zb.Xml = fp.Xml;
                    zb.CloneJsonToXml(fileProcessorJson.Xml);
                    fp.Xml = zb.Xml;
                }

                timer.WriteOutput();
            }
            else if (quentinSpecificActions)
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
            else if (queryReplace && queryFileName.Length > 0)
            {
                fp.Xml = Base.QueryReplace.Replace(fp.Xml, queryFileName);
            }
            else if (flora)
            {
                FileProcessor flp = new FileProcessor(inputFileName, config.floraExtractedTaxaListPath);
                FileProcessor flpp = new FileProcessor(inputFileName, config.floraExtractTaxaPartsOutputPath);
                Flora fl = new Flora(config);
                fl.Xml = fp.Xml;

                fl.ExtractTaxa();
                fl.DistinctTaxa();
                fl.GenerateTagTemplate();

                flp.Xml = fl.Xml;
                flp.WriteStringContentToFile();

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
                flpp.WriteStringContentToFile();
            }
            else if (testFlag)
            {
                ////Test test = new Test(fp.Xml);
                ////test.Config = config;
                ////test.ExtractSystemChecklistAuthority();
                ////fp.Xml = test.Xml;

                ////string scientificName = "Dascillidae";
                ////string[] scientificNames = { "Plantago major", "Monohamus galloprovincialis", "Felis concolor" };
                ////int[] srcId = { 1, 12 };
                ////string rank = Base.Net.SearchNameInPaleobiologyDatabase(scientificName);
                ////Alert.Message(scientificName + " --> " + rank);

                ////Base.Json.Pbdb.PbdbAllParents obj = Net.SearchParentsInPaleobiologyDatabase(scientificName);

                ////if (obj.records.Count > 0)
                ////{
                ////    Alert.Message(obj.records[0].nam + "  " + obj.records[0].rnk);
                ////}

                ////XmlDocument xx = Base.Net.SearchWithGlobalNamesResolver(scientificNames/*, srcId*/);
                ////fp.Xml = xx.OuterXml;

                ////test.SqlSelect();

                List<string> words = fp.ExtractWordsFromXml();
                foreach (string word in words)
                {
                    Alert.Message(word);
                }

                Alert.Message("\n\n" + words.Count + " words in this article\n");
            }
            else if (generateZooBankNlm)
            {
                ZooBank zb = new ZooBank(config);
                zb.Xml = fp.Xml;
                zb.GenerateZooBankNlm();
                fp.Xml = zb.Xml;
            }
            else if (tagReferences)
            {
                if (parseBySection)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.PreserveWhitespace = true;
                    XmlNamespaceManager namespaceManager = Base.Base.TaxPubNamespceManager(xmlDocument);

                    try
                    {
                        xmlDocument.LoadXml(fp.Xml);
                    }
                    catch (XmlException)
                    {
                        Alert.Message("Tagger: XmlException");
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

                            Alert.Message(templateFileName);

                            XmlNode newNode = node;
                            newNode.InnerXml = TagReferences(node.OuterXml, templateFileName);
                            node.InnerXml = newNode.FirstChild.InnerXml;
                        }
                    }
                    catch (System.Xml.XPath.XPathException)
                    {
                        Alert.Message("Tagger: XPathException");
                        Alert.Exit(1);
                    }
                    catch (System.InvalidOperationException)
                    {
                        Alert.Message("Tagger: InvalidOperationException");
                        Alert.Exit(1);
                    }
                    catch (System.Xml.XmlException)
                    {
                        Alert.Message("Tagger: XmlException");
                        Alert.Exit(1);
                    }

                    fp.Xml = xmlDocument.OuterXml;
                }
                else
                {
                    fp.Xml = TagReferences(fp.Xml, outputFileName);
                }
            }
            else
            {
                /*
                 * Main Tagging part of the program
                 */
                if (parseBySection)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.PreserveWhitespace = true;
                    XmlNamespaceManager namespaceManager = Base.Base.TaxPubNamespceManager(xmlDocument);

                    try
                    {
                        xmlDocument.LoadXml(fp.Xml);
                    }
                    catch (XmlException)
                    {
                        Alert.Message("Tagger: XmlException");
                        Alert.Exit(10);
                    }

                    try
                    {
                        foreach (XmlNode node in xmlDocument.SelectNodes(higherStructrureXpath, namespaceManager))
                        {
                            XmlNode newNode = node;
                            newNode.InnerXml = MainProcessing(node.OuterXml);
                            node.InnerXml = newNode.FirstChild.InnerXml;
                        }
                    }
                    catch (System.Xml.XPath.XPathException)
                    {
                        Alert.Message("Tagger: XPathException trying to tag taxa.");
                        Alert.Exit(1);
                    }
                    catch (System.InvalidOperationException)
                    {
                        Alert.Message("Tagger: InvalidOperationException trying to tag taxa.");
                        Alert.Exit(1);
                    }
                    catch (System.Xml.XmlException)
                    {
                        Alert.Message("Tagger: XmlException trying to tag taxa.");
                        Alert.Exit(1);
                    }

                    fp.Xml = xmlDocument.OuterXml;
                }
                else
                {
                    fp.Xml = MainProcessing(fp.Xml);
                }
            }

            {
                Timer timer = new Timer();
                timer.Start();
                Alert.WriteOutputFileMessage();
                fp.WriteStringContentToFile();
                timer.WriteOutput();
            }

            allTime.WriteOutput();
        }

        private static void ParseFileNames(string[] args)
        {
            if (arguments.Count < 1)
            {
                Alert.PrintHelp();
                Alert.Exit(1);
            }
            else if (arguments.Count == 1)
            {
                inputFileName = args[arguments[0]];
                outputFileName = System.IO.Path.GetDirectoryName(inputFileName) + "\\"
                    + System.IO.Path.GetFileNameWithoutExtension(inputFileName) + "-out"
                    + System.IO.Path.GetExtension(inputFileName);
            }
            else if (arguments.Count == 2)
            {
                inputFileName = args[arguments[0]];
                outputFileName = args[arguments[1]];
            }
            else
            {
                inputFileName = args[arguments[0]];
                outputFileName = args[arguments[1]];
                queryFileName = args[arguments[2]];
            }

            Alert.Message("Input file name: " + inputFileName);
            Alert.Message("Output file name: " + outputFileName);
            Alert.Message(queryFileName);
        }

        private static string MainProcessing(string xml)
        {
            string xmlContent = xml;
            Base.Taxonomy.Splitter split = new Base.Taxonomy.Splitter(config);
            Base.Taxonomy.Tagger tagger = new Base.Taxonomy.Tagger(config);

            if (tagFigTab)
            {
                Timer timer = new Timer();
                timer.Start();
                Alert.Message("\n\tTag floats.\n");
                Floats fl = new Floats(xmlContent);
                fl.TagAllFloats();
                xmlContent = fl.Xml;
                timer.WriteOutput();
            }

            if (tagTableFn)
            {
                Timer timer = new Timer();
                timer.Start();
                Alert.Message("\n\tTag table foot-notes.\n");
                Floats fl = new Floats(xmlContent);
                fl.TagTableFootNotes();
                xmlContent = fl.Xml;
                timer.WriteOutput();
            }

            /*
             * Taxonomic part
             */
            if (taxaA || taxaB)
            {
                Timer timer = new Timer();
                timer.Start();
                Alert.Message("\n\tTag taxa.\n");
                tagger.Xml = xmlContent;

                if (taxaA)
                {
                    tagger.TagLowerTaxa(true);
                }

                if (taxaB)
                {
                    tagger.TagHigherTaxa();
                }

                tagger.UntagTaxa();

                xmlContent = tagger.Xml;
                timer.WriteOutput();
            }

            if (taxaC || taxaD)
            {
                Timer timer = new Timer();
                timer.Start();
                Alert.Message("\n\tSplit taxa.\n");
                split.Xml = xmlContent;

                if (taxaC)
                {
                    split.SplitLowerTaxa();
                }

                if (taxaD)
                {
                    split.SplitHigherTaxa(true, false, false, false, false);

                    if (splitHigherWithAphia)
                    {
                        Alert.Message("\n\tSplit higher taxa using Aphia API\n");
                        split.SplitHigherTaxa(false, true, false, false, false);
                    }

                    if (splitHigherWithCoL)
                    {
                        Alert.Message("\n\tSplit higher taxa using CoL API\n");
                        split.SplitHigherTaxa(false, false, true, false, false);
                    }

                    if (splitHigherWithGbif)
                    {
                        Alert.Message("\n\tSplit higher taxa using GBIF API\n");
                        split.SplitHigherTaxa(false, false, false, true, false);
                    }

                    if (splitHigherBySuffix)
                    {
                        Alert.Message("\n\tSplit higher taxa by suffix\n");
                        split.SplitHigherTaxa(false, false, false, false, true);
                    }

                    if (splitHigherAboveGenus)
                    {
                        Alert.Message("\n\tMake higher taxa of type 'above-genus'\n");
                        split.SplitHigherTaxa(false, false, false, false, false, true);
                    }
                }

                xmlContent = split.Xml;
                timer.WriteOutput();
            }

            if (taxaE || flag1 || flag2 || flag3 || flag4 || flag5 || flag6 || flag7 || flag8)
            {
                Timer timer = new Timer();
                timer.Start();
                Alert.Message("\n\tExpand taxa.\n");

                Base.Taxonomy.Nlm.Expander expand = new Base.Taxonomy.Nlm.Expander(config, xmlContent);
                Base.Taxonomy.Expander exp = new Base.Taxonomy.Expander(config, xmlContent);

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
                    timer.WriteOutput();
                }
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
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(xmlContent);
                List<string> taxaList;

                if (extractTaxa)
                {
                    Alert.Message("\n\t\tExtract all taxa\n");
                    taxaList = Taxonomy.ExtractTaxa(xdoc, true);
                    foreach (string taxon in taxaList)
                    {
                        Alert.Message(taxon);
                    }
                }

                if (extractLowerTaxa)
                {
                    Alert.Message("\n\t\tExtract lower taxa\n");
                    taxaList = Taxonomy.ExtractTaxa(xdoc, true, TaxaType.Lower);
                    foreach (string taxon in taxaList)
                    {
                        Alert.Message(taxon);
                    }
                }

                if (extractHigherTaxa)
                {
                    Alert.Message("\n\t\tExtract higher taxa\n");
                    taxaList = Taxonomy.ExtractTaxa(xdoc, true, TaxaType.Higher);
                    foreach (string taxon in taxaList)
                    {
                        Alert.Message(taxon);
                    }
                }
            }

            if (untagSplit)
            {
                split.Xml = xmlContent;
                split.UnSplitAllTaxa();
                xmlContent = split.Xml;
            }

            if (formatTreat)
            {
                Timer timer = new Timer();
                timer.Start();
                Alert.Message("\n\tFormat treatments.\n");
                tagger.Xml = xmlContent;

                tagger.FormatTreatments();

                xmlContent = tagger.Xml;
                timer.WriteOutput();
            }

            if (parseTreatmentMetaWithAphia)
            {
                Timer timer = new Timer();
                timer.Start();
                Alert.Message("\n\tParse treatment meta with Aphia.\n");
                tagger.Xml = xmlContent;

                tagger.ParseTreatmentMetaWithAphia();

                xmlContent = tagger.Xml;
                timer.WriteOutput();
            }

            if (parseTreatmentMetaWithGbif)
            {
                Timer timer = new Timer();
                timer.Start();
                Alert.Message("\n\tParse treatment meta with GBIF.\n");
                tagger.Xml = xmlContent;

                tagger.ParseTreatmentMetaWithGbif();

                xmlContent = tagger.Xml;
                timer.WriteOutput();
            }

            if (parseTreatmentMetaWithCol)
            {
                Timer timer = new Timer();
                timer.Start();
                Alert.Message("\n\tParse treatment meta with CoL.\n");
                tagger.Xml = xmlContent;

                tagger.ParseTreatmentMetaWithCoL();

                xmlContent = tagger.Xml;
                timer.WriteOutput();
            }

            return xmlContent;
        }
    }
}
