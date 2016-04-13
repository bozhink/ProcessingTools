namespace ProcessingTools.NlmArchiveConsoleManager
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;

    using Contracts;
    using Loggers;
    using Models;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;

    public class Startup
    {
        private static ILogger logger = new TextWriterLogger();

        public static void Main(string[] args)
        {
            var appSettingsReader = new AppSettingsReader();
            var journalJsonFileName = appSettingsReader.GetValue("JournalConfigJsonFile", typeof(string)).ToString();

            IJournal journal = null;

            try
            {
                using (var journalJsonFileStream = new FileStream(journalJsonFileName, FileMode.Open))
                {
                    var serializer = new DataContractJsonSerializer(typeof(JournalDataContract));
                    var journalJsonObject = (JournalDataContract)serializer.ReadObject(journalJsonFileStream);

                    journal = new Journal
                    {
                        AbbreviatedJournalTitle = journalJsonObject.AbbreviatedJournalTitle,
                        FileNamePattern = journalJsonObject.FileNamePattern,
                        IssnEPub = journalJsonObject.IssnEPub,
                        IssnPPub = journalJsonObject.IssnPPub,
                        JournalId = journalJsonObject.JournalId,
                        JournalTitle = journalJsonObject.JournalTitle,
                        PublisherName = journalJsonObject.PublisherName
                    };
                }
            }
            catch (Exception e)
            {
                logger?.Log(e, "Cannot read journal data.");
                Environment.Exit(1);
            }

            var directories = args.Select(a =>
            {
                if (Directory.Exists(a))
                {
                    return a;
                }
                else
                {
                    string path = Path.GetDirectoryName(a);

                    if (Directory.Exists(path))
                    {
                        return path;
                    }
                    else
                    {
                        logger?.Log(LogType.Error, "'{0}' is not a valid path.", a);
                    }

                    return null;
                }

            })
            .Where(d => !string.IsNullOrWhiteSpace(d));

            foreach (var directoryName in directories)
            {
                Console.WriteLine(directoryName);

                var direcoryProcessor = new DirectoryProcessor(directoryName, journal, logger);
                direcoryProcessor.Process().Wait();
            }
        }
    }
}
