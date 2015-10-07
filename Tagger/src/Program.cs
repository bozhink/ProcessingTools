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
                FileProcessor fp = new FileProcessor(settings.Config, settings.InputFileName, settings.OutputFileName, consoleLogger);

                consoleLogger.Log(
                    "Input file name: {0}\nOutput file name: {1}\n{2}",
                    fp.InputFileName,
                    fp.OutputFileName,
                    settings.QueryFileName);

                settings.Config.EnvoResponseOutputXmlFileName = string.Format(
                    "{0}\\envo-{1}.xml",
                    settings.Config.tempDirectoryPath,
                    Path.GetFileNameWithoutExtension(fp.OutputFileName));

                settings.Config.GnrOutputFileName = string.Format(
                    "{0}\\gnr-{1}.xml",
                    settings.Config.tempDirectoryPath,
                    Path.GetFileNameWithoutExtension(fp.OutputFileName));

                fp.Read();

                switch (fp.XmlDocument.DocumentElement.Name)
                {
                    case "article":
                        settings.Config.NlmStyle = true;
                        break;

                    default:
                        settings.Config.NlmStyle = false;
                        break;
                }

                fp.Xml = fp.Xml.NormalizeXmlToSystemXml(settings.Config);

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

                if (settings.ZoobankCloneXml)
                {
                    ZooBankCloneXml(fp);
                }
                else if (settings.ZoobankCloneJson)
                {
                    ZooBankCloneJson(fp);
                }
                else if (settings.ZoobankGenerateRegistrationXml)
                {
                    ZooBankGenerateRegistrationXml(fp);
                }
                else if (settings.QuentinSpecificActions)
                {
                    QuentinSpecific(fp);
                }
                else if (settings.Flora)
                {
                    FloraSpecific(fp);
                }
                else if (settings.TagReferences)
                {
                    TagReferences(fp);
                }
                else if (settings.QueryReplace && settings.QueryFileName.Length > 0)
                {
                    fp.Xml = QueryReplace.Replace(settings.Config, fp.Xml, settings.QueryFileName);
                }
                else if (settings.TestFlag)
                {
                }
                else
                {
                    /*
                     * Main Tagging part of the program
                     */
                    if (settings.ParseBySection)
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
                            foreach (XmlNode node in xmlDocument.SelectNodes(settings.HigherStructrureXpath, namespaceManager))
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
                consoleLogger.LogException(e, string.Empty);
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
                    if (arg[0] == '-' && arg.Length > 1 && arg[1] == '-')
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
            catch
            {
                throw;
            }
        }

        private static void ParseConfigFiles()
        {
            try
            {
                AppSettingsReader appConfigReader = new AppSettingsReader();
                string configJsonFilePath = appConfigReader.GetValue("ConfigJsonFilePath", typeof(string)).ToString();

                settings.Config = ConfigBuilder.CreateConfig(configJsonFilePath);
                settings.Config.NlmStyle = true;
                settings.Config.TagWholeDocument = false;
            }
            catch
            {
                throw;
            }
        }

        private static void ParseFileNames(string[] args)
        {
            try
            {
                if (arguments.Count < 1)
                {
                    Alert.PrintHelp();
                    Alert.Exit(0);
                }
                else if (arguments.Count == 1)
                {
                    settings.InputFileName = args[arguments[0]];
                    settings.OutputFileName = null;
                }
                else if (arguments.Count == 2)
                {
                    settings.InputFileName = args[arguments[0]];
                    settings.OutputFileName = args[arguments[1]];
                }
                else
                {
                    settings.InputFileName = args[arguments[0]];
                    settings.OutputFileName = args[arguments[1]];
                    settings.QueryFileName = args[arguments[2]];
                }
            }
            catch
            {
                throw;
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
                fp.Xml = fp.Xml.NormalizeXmlToCurrentXml(settings.Config);
                fp.Write();
            }
            catch
            {
                throw;
            }

            PrintElapsedTime(timer);
        }
    }
}