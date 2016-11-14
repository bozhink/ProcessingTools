namespace ProcessingTools.NlmArchiveConsoleManager
{
    using Contracts.Models;
    using Core;
    using Loggers;
    using Models;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;

    public class Startup
    {
        private static ILogger logger = new ConsoleLogger();

        public static void Main(string[] args)
        {
            IJournal journal = GetJoutnalMetadata();

            var directories = args.Select(SelectDirectoryName)
                .Where(d => !string.IsNullOrWhiteSpace(d))
                .ToArray();

            foreach (var directoryName in directories)
            {
                Console.WriteLine(directoryName);

                var direcoryProcessor = new DirectoryProcessor(directoryName, journal, logger);
                direcoryProcessor.Process().Wait();
            }
        }

        private static Func<string, string> SelectDirectoryName => a =>
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
        };

        private static IJournal GetJoutnalMetadata()
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

            return journal;
        }
    }
}
