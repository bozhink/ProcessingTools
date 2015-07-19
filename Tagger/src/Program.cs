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
    public class Tag
    {
        private const int NumberOfExpandingIterations = 1;
        private static bool tagWWW = false;
        private static bool tagDoi = false;
        private static bool tagFigTab = false;
        private static bool tagTableFn = false;
        private static bool tagCoords = false;
        private static bool parseCoords = false;
        private static bool formatInit = false;
        private static bool formatTreat = false;
        private static bool taxaA = false;
        private static bool taxaB = false;
        private static bool taxaC = false;
        private static bool taxaD = false;
        private static bool taxaE = false;
        private static bool extractTaxa = false;
        private static bool extractLowerTaxa = false;
        private static bool extractHigherTaxa = false;
        private static bool untagSplit = false;
        private static bool flag1 = false;
        private static bool flag2 = false;
        private static bool flag3 = false;
        private static bool flag4 = false;
        private static bool flag5 = false;
        private static bool flag6 = false;
        private static bool flag7 = false;
        private static bool flag8 = false;
        private static bool nlm = false;
        private static bool html = false;
        private static bool zoobank = false;
        private static bool zoobankJson = false;
        private static bool generateZooBankNlm = false;
        private static bool parseReferences = false;
        private static bool tagReferences = false;
        private static bool quentinSpecificActions = false;
        private static bool queryReplace = false;
        private static bool flora = false;
        private static bool parseBySection = false;
        private static bool splitHigherBySuffix = false;
        private static bool splitHigherWithGbif = false;
        private static bool splitHigherWithAphia = false;
        private static bool splitHigherWithCoL = false;
        private static bool splitHigherAboveGenus = false;
        private static bool parseTreatmentMetaWithAphia = false;
        private static bool parseTreatmentMetaWithGbif = false;
        private static bool parseTreatmentMetaWithCol = false;
        private static bool testFlag = false;
        private static bool normalizeFlag = false;
        private static bool tagEnvironments = false;
        private static bool tagCodes = false;
        private static Config config;

        private static string higherStructrureXpath = "//article"; // "//sec[name(..)!='sec']";

        private static List<int> dashOptions = new List<int>();
        private static List<int> ddashOptions = new List<int>();
        private static List<int> arguments = new List<int>();

        public static void Main(string[] args)
        {
            /*
             * Parse config file
             */
            config = ConfigBuilder.CreateConfig("C:\\bin\\config.json");

            /*
             * Initial check of input parameters
             */
            for (int i = 0; i < args.Length; i++)
            {
                char[] arg = args[i].ToCharArray();
                if (arg[0] == '-' && arg[1] == '-')
                {
                    /*
                     * Double dashed options
                    */
                    ddashOptions.Add(i);
                }
                else if (arg[0] == '-' || arg[0] == '/')
                {
                    /*
                     * Single dashed options
                     */
                    dashOptions.Add(i);
                }
                else
                {
                    arguments.Add(i);
                }
            }

            config.NlmStyle = true;
            config.TagWholeDocument = false;

            Timer allTime = new Timer();
            Timer timer = new Timer();
            string inputFileName = string.Empty, outputFileName = string.Empty, queryFileName = string.Empty;

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

            foreach (int item in dashOptions)
            {
                char[] arg = args[item].ToCharArray();
                for (int i = 1; i < arg.Length; i++)
                {
                    switch (arg[i])
                    {
                        case 'h':
                        case '?':
                            Alert.PrintHelp();
                            Alert.Exit(0);
                            break;
                        case 'i':
                            formatInit = true;
                            break;
                        case 't':
                            formatTreat = true;
                            break;
                        case 'A':
                            taxaA = true;
                            break;
                        case 'B':
                            taxaB = true;
                            break;
                        case 'C':
                            taxaC = true;
                            break;
                        case 'D':
                            taxaD = true;
                            break;
                        case 'E':
                            taxaE = true;
                            break;
                        case 'u':
                            untagSplit = true;
                            break;
                        case 'w':
                            tagWWW = true;
                            break;
                        case 'd':
                            tagDoi = true;
                            break;
                        case 'f':
                            tagFigTab = true;
                            break;
                        case 'c':
                            tagCoords = true;
                            break;
                        case 'k':
                            parseCoords = true;
                            break;
                        case '1':
                            flag1 = true;
                            break;
                        case '2':
                            flag2 = true;
                            break;
                        case '3':
                            flag3 = true;
                            break;
                        case '4':
                            flag4 = true;
                            break;
                        case '5':
                            flag5 = true;
                            break;
                        case '6':
                            flag6 = true;
                            break;
                        case '7':
                            flag7 = true;
                            break;
                        case '8':
                            flag8 = true;
                            break;
                        case 'X':
                            queryReplace = true;
                            break;
                        case 'N':
                            nlm = true;
                            break;
                        case 'H':
                            html = true;
                            break;
                        case 'z':
                            zoobank = true;
                            break;
                        case 'r':
                            parseReferences = true;
                            break;
                        case 'R':
                            tagReferences = true;
                            break;
                        case 'Q':
                            quentinSpecificActions = true;
                            break;
                        case 'F':
                            flora = true;
                            break;
                        case 's':
                            parseBySection = true;
                            break;
                    }
                }
            }

            foreach (int item in ddashOptions)
            {
                if (args[item].CompareTo("--split-aphia") == 0)
                {
                    splitHigherWithAphia = true;
                }
                else if (args[item].CompareTo("--split-col") == 0)
                {
                    splitHigherWithCoL = true;
                }
                else if (args[item].CompareTo("--split-gbif") == 0)
                {
                    splitHigherWithGbif = true;
                }
                else if (args[item].CompareTo("--split-suffix") == 0)
                {
                    splitHigherBySuffix = true;
                }
                else if (args[item].CompareTo("--above-genus") == 0)
                {
                    splitHigherAboveGenus = true;
                }
                else if (args[item].CompareTo("--system") == 0)
                {
                    config.NlmStyle = false;
                }
                else if (args[item].CompareTo("--normalize") == 0)
                {
                    normalizeFlag = true;
                }
                else if (args[item].CompareTo("--test") == 0)
                {
                    testFlag = true;
                }
                else if (args[item].CompareTo("--extract-taxa") == 0 || args[item].CompareTo("--et") == 0)
                {
                    extractTaxa = true;
                }
                else if (args[item].CompareTo("--extract-lower-taxa") == 0 || args[item].CompareTo("--elt") == 0)
                {
                    extractLowerTaxa = true;
                }
                else if (args[item].CompareTo("--extract-higher-taxa") == 0 || args[item].CompareTo("--eht") == 0)
                {
                    extractHigherTaxa = true;
                }
                else if (args[item].CompareTo("--zoobank-nlm") == 0)
                {
                    generateZooBankNlm = true;
                }
                else if (args[item].CompareTo("--zoobank-json") == 0)
                {
                    zoobankJson = true;
                }
                else if (args[item].CompareTo("--zoobank-clone") == 0)
                {
                    zoobank = true;
                }
                else if (args[item].CompareTo("--parse-treatment-meta-with-aphia") == 0 || args[item].CompareTo("--ptm-aphia") == 0)
                {
                    parseTreatmentMetaWithAphia = true;
                }
                else if (args[item].CompareTo("--parse-treatment-meta-with-gbif") == 0 || args[item].CompareTo("--ptm-gbif") == 0)
                {
                    parseTreatmentMetaWithGbif = true;
                }
                else if (args[item].CompareTo("--parse-treatment-meta-with-col") == 0 || args[item].CompareTo("--ptm-col") == 0)
                {
                    parseTreatmentMetaWithCol = true;
                }
                else if (args[item].CompareTo("--table-fn") == 0)
                {
                    tagTableFn = true;
                }
                else if (args[item].CompareTo("--environments") == 0)
                {
                    tagEnvironments = true;
                }
                else if (args[item].CompareTo("--codes") == 0)
                {
                    tagCodes = true;
                }
            }

            /*
             * Now input parameters are set.
             */

            /*
             * Main processing part
             */
            FileProcessor fp = new FileProcessor(inputFileName, outputFileName);
            fp.ReadStringContent(true);

            if (formatInit)
            {
                timer.Start();
                Alert.Message("\n\tInitial format.\n");

                if (!config.NlmStyle)
                {
                    Base.Format.NlmSystem.Format fmt = new Base.Format.NlmSystem.Format();
                    fmt.Xml = XsltOnString.ApplyTransform(config.systemInitialFormatXslPath, fp.GetXmlReader());
                    fmt.InitialFormat();
                    fp.Xml = fmt.Xml;
                }
                else
                {
                    Base.Format.Nlm.Format fmt = new Base.Format.Nlm.Format();
                    fmt.Xml = XsltOnString.ApplyTransform(config.nlmInitialFormatXslPath, fp.GetXmlReader());
                    fmt.InitialFormat();
                    fp.Xml = Base.Base.NormalizeSystemToNlmXml(config, fmt.Xml);
                }

                timer.WriteOutput();
            }

            if (parseReferences)
            {
                timer.Start();
                Alert.Message("\n\tParse references.\n");
                References refs = new References();
                refs.Xml = fp.Xml;

                refs.SplitReferences();

                fp.Xml = refs.Xml;
                timer.WriteOutput();
            }

            if (tagDoi)
            {
                timer.Start();
                Alert.Message("\n\tTag DOI.\n");
                Base.Nlm.Links ln = new Base.Nlm.Links(fp.Xml);

                ln.TagDOI();
                ln.TagPMCLinks();

                fp.Xml = ln.Xml;
                timer.WriteOutput();
            }

            if (tagWWW)
            {
                timer.Start();
                Alert.Message("\n\tTag web links.\n");
                Base.Nlm.Links ln = new Base.Nlm.Links(fp.Xml);

                ln.TagWWW();

                fp.Xml = ln.Xml;
                timer.WriteOutput();
            }

            if (tagCoords)
            {
                timer.Start();
                Alert.Message("\n\tTag coordinates.\n");
                Coordinates cd = new Coordinates(fp.Xml);

                cd.TagCoordinates();

                fp.Xml = cd.Xml;
                timer.WriteOutput();
            }

            if (parseCoords)
            {
                timer.Start();
                Alert.Message("\n\tParse coordinates.\n");
                Coordinates cd = new Coordinates(fp.Xml);

                cd.ParseCoordinates();

                fp.Xml = cd.Xml;
                timer.WriteOutput();
            }

            if (nlm)
            {
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
                Flora fl = new Flora();
                fl.Config = config;
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
                ZooBank zb = new ZooBank();
                zb.Config = config;
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
                            newNode.InnerXml = MainProcessing(node.OuterXml, timer);
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
                    fp.Xml = MainProcessing(fp.Xml, timer);
                }
            }

            if (tagEnvironments)
            {
                timer.Start();
                Alert.Message("\n\tTag environments.\n");
                Base.Environments environments = new Environments(fp.Xml);
                environments.Config = config;

                environments.TagEnvironmentsRecords();

                fp.Xml = environments.Xml;
                timer.WriteOutput();
            }

            if (tagCodes)
            {
                timer.Start();
                Alert.Message("\n\tTag codes.\n");
                Base.Codes codes = new Codes(Base.Base.NormalizeNlmToSystemXml(config, fp.Xml));
                codes.Config = config;

                codes.TagInstitutions();

                codes.TagProducts();
                codes.TagGeonames();
                codes.TagMorphology();
                codes.TagSpecimenCount();
                codes.TagAbbreviationsInText();

                codes.TagInstitutionalCodes();

                codes.TagQuantities();
                codes.TagDirections();
                codes.TagDates();
                codes.TagSpecimenCodes();

                fp.Xml = Base.Base.NormalizeSystemToNlmXml(config, codes.Xml);
                timer.WriteOutput();
            }

            timer.Start();
            Alert.WriteOutputFileMessage();
            fp.WriteStringContentToFile();
            timer.WriteOutput();

            allTime.WriteOutput();
        }

        private static string MainProcessing(string xml, Timer timer)
        {
            string xmlContent = xml;
            Base.Taxonomy.Splitter split = new Base.Taxonomy.Splitter(config);
            Base.Taxonomy.Tagger tagger = new Base.Taxonomy.Tagger(config);

            if (tagFigTab)
            {
                timer.Start();
                Alert.Message("\n\tTag floats.\n");
                Floats fl = new Floats(xmlContent);
                fl.TagAllFloats();
                xmlContent = fl.Xml;
                timer.WriteOutput();
            }

            if (tagTableFn)
            {
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
                timer.Start();
                Alert.Message("\n\tExpand taxa.\n");

                Base.Taxonomy.Nlm.Expander expand = new Base.Taxonomy.Nlm.Expander(xmlContent);
                Base.Taxonomy.Expander exp = new Base.Taxonomy.Expander(xmlContent);
                expand.Config = config;
                exp.Config = config;

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
                timer.Start();
                Alert.Message("\n\tFormat treatments.\n");
                tagger.Xml = xmlContent;

                tagger.FormatTreatments();

                xmlContent = tagger.Xml;
                timer.WriteOutput();
            }

            if (parseTreatmentMetaWithAphia)
            {
                timer.Start();
                Alert.Message("\n\tParse treatment meta with Aphia.\n");
                tagger.Xml = xmlContent;

                tagger.ParseTreatmentMetaWithAphia();

                xmlContent = tagger.Xml;
                timer.WriteOutput();
            }

            if (parseTreatmentMetaWithGbif)
            {
                timer.Start();
                Alert.Message("\n\tParse treatment meta with GBIF.\n");
                tagger.Xml = xmlContent;

                tagger.ParseTreatmentMetaWithGbif();

                xmlContent = tagger.Xml;
                timer.WriteOutput();
            }

            if (parseTreatmentMetaWithCol)
            {
                timer.Start();
                Alert.Message("\n\tParse treatment meta with CoL.\n");
                tagger.Xml = xmlContent;

                tagger.ParseTreatmentMetaWithCoL();

                xmlContent = tagger.Xml;
                timer.WriteOutput();
            }

            return xmlContent;
        }

        private static string TagReferences(string xml, string fileName)
        {
            string xmlContent = xml;
            References refs = new References(xmlContent);

            config.referencesGetReferencesXmlPath = Path.GetDirectoryName(fileName) + "\\zzz-" + Path.GetFileNameWithoutExtension(fileName) + "-references.xml";
            config.referencesTagTemplateXmlPath = config.tempDirectoryPath + "\\zzz-" + Path.GetFileNameWithoutExtension(fileName) + "-references-tag-template.xml";

            refs.Config = config;
            refs.GenerateTagTemplateXml();
            refs.TagReferences();
            xmlContent = refs.Xml;
            return xmlContent;
        }
    }
}
