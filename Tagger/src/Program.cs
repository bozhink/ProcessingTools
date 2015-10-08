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

        private const int NumberOfExpandingIterations = 1;
        private static ProgramSettings settings = new ProgramSettings();
        private static ILogger consoleLogger = new ConsoleLogger();

        public static void Main(string[] args)
        {
            Stopwatch mainTimer = new Stopwatch();
            mainTimer.Start();

            try
            {
                ProgramSettingsBuilder settingsBuilder = new ProgramSettingsBuilder(args);
                settings = settingsBuilder.Settings;

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

                settingsBuilder.ParseProgramOptions();
                settings = settingsBuilder.Settings;

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