namespace ProcessingTools.Tag
{
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Base;
    using Base.ZooBank;

    public partial class Tagger
    {
        public static void Main(string[] args)
        {
            Stopwatch mainTimer = new Stopwatch();
            mainTimer.Start();

            /*
             * Parse config file
             */

            AppSettingsReader appConfigReader = new AppSettingsReader();
            string configJsonFilePath = appConfigReader.GetValue("ConfigJsonFilePath", typeof(string)).ToString();

            config = ConfigBuilder.CreateConfig(configJsonFilePath);
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
                    doubleDashedOptions.Add(i);
                }
                else if (arg[0] == '-' || arg[0] == '/')
                {
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
            FileProcessor fp = new FileProcessor(config, inputFileName, outputFileName);
            Alert.Log(
                "Input file name: {0}\nOutput file name: {1}\n{2}",
                fp.InputFileName,
                fp.OutputFileName,
                queryFileName);

            config.EnvoResponseOutputXmlFileName = string.Format(
                "{0}\\envo-{1}.xml",
                config.tempDirectoryPath,
                Path.GetFileNameWithoutExtension(fp.OutputFileName));

            config.GnrOutputFileName = string.Format(
                "{0}\\gnr-{1}.xml",
                config.tempDirectoryPath,
                Path.GetFileNameWithoutExtension(fp.OutputFileName));

            fp.Read();
            fp.NormalizeXmlToSystemXml();

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

            if (zoobank)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Alert.ZoobankCloneMessage();
                if (arguments.Count > 2)
                {
                    FileProcessor fileProcessorNlm = new FileProcessor(config, queryFileName, outputFileName);
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
                    string jsonStringContent = FileProcessor.ReadFileContentToString(queryFileName);
                    ZoobankCloner zb = new ZoobankCloner(fp.Xml);
                    zb.CloneJsonToXml(jsonStringContent);
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
                fp.Xml = QueryReplace.Replace(config, fp.Xml, queryFileName);
            }
            else if (flora)
            {
                FileProcessor flp = new FileProcessor(config, inputFileName, config.floraExtractedTaxaListPath);
                FileProcessor flpp = new FileProcessor(config, inputFileName, config.floraExtractTaxaPartsOutputPath);
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
            }
            else if (generateZooBankNlm)
            {
                ZooBank zb = new ZooBank(config, fp.Xml);
                zb.GenerateZooBankNlm();
                fp.Xml = zb.Xml;
            }
            else if (tagReferences)
            {
                Alert.Log("\n\tTag references.\n");
                if (parseBySection)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.PreserveWhitespace = true;
                    XmlNamespaceManager namespaceManager = Config.TaxPubNamespceManager(xmlDocument);

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
                    catch (XmlException)
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
                    catch (XmlException)
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

                fp.NormalizeSystemXmlToCurrent();
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
