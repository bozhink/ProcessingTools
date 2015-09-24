namespace ProcessingTools.MainProgram
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Xml;
    using BaseLibrary;

    public partial class MainProcessingTool
    {
        public static void Main(string[] args)
        {
            Stopwatch mainTimer = new Stopwatch();
            mainTimer.Start();

            ParseConfigFiles();
            InitialCheckOfInputParameters(args);
            ParseFileNames(args);

            try
            {
                FileProcessor fp = new FileProcessor(config, inputFileName, outputFileName, consoleLogger);

                consoleLogger.Log(
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

                switch (fp.XmlDocument.DocumentElement.Name)
                {
                    case "article":
                        config.NlmStyle = true;
                        break;

                    default:
                        config.NlmStyle = false;
                        break;
                }

                fp.Xml = fp.Xml.NormalizeXmlToSystemXml(config);

                ParseSingleDashedOptions(args);
                ParseDoubleDashedOptions(args);

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

                if (zoobankCloneXml)
                {
                    ZooBankCloneXml(fp);
                }
                else if (zoobankCloneJson)
                {
                    ZooBankCloneJson(fp);
                }
                else if (zoobankGenerateRegistrationXml)
                {
                    ZooBankGenerateRegistrationXml(fp);
                }
                else if (quentinSpecificActions)
                {
                    QuentinSpecific(fp);
                }
                else if (flora)
                {
                    FloraSpecific(fp);
                }
                else if (tagReferences)
                {
                    TagReferences(fp);
                }
                else if (queryReplace && queryFileName.Length > 0)
                {
                    fp.Xml = QueryReplace.Replace(config, fp.Xml, queryFileName);
                }
                else if (testFlag)
                {
                }
                else
                {
                    /*
                     * Main Tagging part of the program
                     */
                    if (parseBySection)
                    {
                        XmlNamespaceManager namespaceManager = Config.TaxPubNamespceManager();
                        XmlDocument xmlDocument = new XmlDocument(namespaceManager.NameTable);
                        xmlDocument.PreserveWhitespace = true;

                        try
                        {
                            xmlDocument.LoadXml(fp.Xml);
                        }
                        catch
                        {
                            throw;
                        }

                        try
                        {
                            foreach (XmlNode node in xmlDocument.SelectNodes(higherStructrureXpath, namespaceManager))
                            {
                                XmlNode newNode = node;
                                newNode.InnerXml = MainProcessing(node.OuterXml);
                                node.InnerXml = newNode.FirstChild.InnerXml;
                            }

                            fp.Xml = xmlDocument.OuterXml;
                        }
                        catch
                        {
                            throw;
                        }
                    }
                    else
                    {
                        fp.Xml = MainProcessing(fp.Xml);
                    }
                }

                WriteOutputFile(fp);
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0);
            }

            consoleLogger.Log("Main timer: " + mainTimer.Elapsed);
        }

        private static void InitialCheckOfInputParameters(string[] args)
        {
            try
            {
                for (int i = 0; i < args.Length; ++i)
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
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 1);
            }
        }

        private static void ParseConfigFiles()
        {
            try
            {
                AppSettingsReader appConfigReader = new AppSettingsReader();
                string configJsonFilePath = appConfigReader.GetValue("ConfigJsonFilePath", typeof(string)).ToString();

                config = ConfigBuilder.CreateConfig(configJsonFilePath);
                config.NlmStyle = true;
                config.TagWholeDocument = false;
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 1);
            }
        }

        private static void ParseFileNames(string[] args)
        {
            try
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
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 1);
            }
        }

        private static void PrintElapsedTime(Stopwatch timer)
        {
            consoleLogger.Log("Elapsed time " + timer.Elapsed);
        }

        private static void WriteOutputFile(FileProcessor fp)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            consoleLogger.Log("\n\tWriting data to output file.\n");

            try
            {
                fp.Xml = fp.Xml.NormalizeXmlToCurrentXml(config);
                fp.Write();
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 1);
            }

            PrintElapsedTime(timer);
        }
    }
}