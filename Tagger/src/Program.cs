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
        public static void Main(string[] args)
        {
            Stopwatch mainTimer = new Stopwatch();
            mainTimer.Start();

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

            ParseFileNames(args);
            ParseSingleDashedOptions(args);
            ParseDoubleDashedOptions(args);

            /*
             * Main processing part
             */
            FileProcessor fp = new FileProcessor(inputFileName, outputFileName);
            Alert.Log(
                "Input file name: {0}\nOutput file name: {1}\n{2}",
                fp.InputFileName,
                fp.OutputFileName,
                queryFileName);

            config.EnvoResponseOutputXmlFileName = string.Format(
                "{0}\\envo-{1}.xml",
                config.tempDirectoryPath,
                Path.GetFileNameWithoutExtension(fp.OutputFileName));

            fp.Read();

            InitialFormat(fp);

            ParseReferences(fp);

            TagDoi(fp);
            TagWebLinks(fp);

            TagCoordinates(fp);
            ParseCoordinates(fp);

            TagEnvo(fp);
            TagEnvoTerms(fp);
            TagQuantities(fp);
            TagDates(fp);
            TagAbbreviations(fp);

            TagCodes(fp);

            if (nlm)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tFormat NLM xml. [obsolete]\n");
                Base.Format.Nlm.Nlm fpnlm = new Base.Format.Nlm.Nlm(fp.Xml);
                fpnlm.Format();
                fp.Xml = fpnlm.Xml;
                PrintElapsedTime(timer);
            }
            else if (html)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.Log("\n\tFormat Html. [obsolete]\n");
                Base.Format.Nlm.Html fphtml = new Base.Format.Nlm.Html(fp.Xml);
                fphtml.Format();
                fp.Xml = fphtml.Xml;
                PrintElapsedTime(timer);
            }
            else if (normalizeFlag)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                if (config.NlmStyle)
                {
                    Alert.Log("Input Xml will be normalized to NLM syle.");
                    string xml = fp.Xml;
                    xml = Base.Base.NormalizeSystemToNlmXml(config, xml);
                    fp.Xml = xml;
                }
                else
                {
                    Alert.Log("Input Xml will be normalized to System style.");
                    string xml = fp.Xml;
                    xml = Base.Base.NormalizeNlmToSystemXml(config, xml);
                    fp.Xml = xml;
                }

                PrintElapsedTime(timer);
            }
            else if (zoobank)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.ZoobankCloneMessage();
                if (arguments.Count > 2)
                {
                    FileProcessor fileProcessorNlm = new FileProcessor(queryFileName, outputFileName);
                    fileProcessorNlm.Read();
                    ZoobankCloner zb = new ZoobankCloner(fileProcessorNlm.Xml, fp.Xml);
                    zb.Clone();
                    fp.Xml = zb.Xml;
                }

                PrintElapsedTime(timer);
            }
            else if (zoobankJson)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.ZoobankCloneMessage();
                if (arguments.Count > 2)
                {
                    FileProcessor fileProcessorJson = new FileProcessor(queryFileName, outputFileName);
                    fileProcessorJson.Read();
                    ZoobankCloner zb = new ZoobankCloner(fp.Xml);
                    zb.CloneJsonToXml(fileProcessorJson.Xml);
                    fp.Xml = zb.Xml;
                }

                PrintElapsedTime(timer);
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
                Flora fl = new Flora(config, fp.Xml);

                fl.ExtractTaxa();
                fl.DistinctTaxa();
                fl.GenerateTagTemplate();

                flp.Xml = fl.Xml;
                flp.Write();

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
                flpp.Write();
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
            }
            else if (generateZooBankNlm)
            {
                ZooBank zb = new ZooBank(config, fp.Xml);
                zb.GenerateZooBankNlm();
                fp.Xml = zb.Xml;
            }
            else if (tagReferences)
            {
                if (parseBySection)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.PreserveWhitespace = true;
                    XmlNamespaceManager namespaceManager = ProcessingTools.Config.TaxPubNamespceManager(xmlDocument);

                    try
                    {
                        xmlDocument.LoadXml(fp.Xml);
                    }
                    catch (XmlException)
                    {
                        Alert.Log("Tagger: XmlException");
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

                            Alert.Log(templateFileName);

                            XmlNode newNode = node;
                            newNode.InnerXml = TagReferences(node.OuterXml, templateFileName);
                            node.InnerXml = newNode.FirstChild.InnerXml;
                        }
                    }
                    catch (System.Xml.XPath.XPathException)
                    {
                        Alert.Log("Tagger: XPathException");
                        Alert.Exit(1);
                    }
                    catch (System.InvalidOperationException)
                    {
                        Alert.Log("Tagger: InvalidOperationException");
                        Alert.Exit(1);
                    }
                    catch (System.Xml.XmlException)
                    {
                        Alert.Log("Tagger: XmlException");
                        Alert.Exit(1);
                    }

                    fp.Xml = xmlDocument.OuterXml;
                }
                else
                {
                    fp.Xml = TagReferences(fp.Xml, fp.OutputFileName);
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
                    XmlNamespaceManager namespaceManager = ProcessingTools.Config.TaxPubNamespceManager(xmlDocument);

                    try
                    {
                        xmlDocument.LoadXml(fp.Xml);
                    }
                    catch (XmlException)
                    {
                        Alert.Log("Tagger: XmlException");
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
                        Alert.Log("Tagger: XPathException trying to tag taxa.");
                        Alert.Exit(1);
                    }
                    catch (System.InvalidOperationException)
                    {
                        Alert.Log("Tagger: InvalidOperationException trying to tag taxa.");
                        Alert.Exit(1);
                    }
                    catch (System.Xml.XmlException)
                    {
                        Alert.Log("Tagger: XmlException trying to tag taxa.");
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
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.WriteOutputFileMessage();
                fp.Write();
                PrintElapsedTime(timer);
            }

            Alert.Log("Main timer: " + mainTimer.Elapsed);
        }

        private static void PrintElapsedTime(Stopwatch timer)
        {
            Alert.Log("Elapsed time " + timer.Elapsed);
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
                outputFileName = null;
                //outputFileName = System.IO.Path.GetDirectoryName(inputFileName) + "\\"
                //    + System.IO.Path.GetFileNameWithoutExtension(inputFileName) + "-out"
                //    + System.IO.Path.GetExtension(inputFileName);
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
        }
    }
}
