namespace ProcessingTools.BaseLibrary
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Configurator;
    using Contracts;

    public class FileProcessor : Base
    {
        private string inputFileName;
        private string outputFileName;
        private ILogger logger;

        public FileProcessor(Config config)
            : this(config, null, null, null)
        {
        }

        public FileProcessor(Config config, ILogger logger)
            : this(config, null, null, logger)
        {
        }

        public FileProcessor(Config config, string inputFileName)
            : this(config, inputFileName, null, null)
        {
        }

        public FileProcessor(Config config, string inputFileName, ILogger logger)
            : this(config, inputFileName, null, logger)
        {
        }

        public FileProcessor(Config config, string inputFileName, string outputFileName)
            : this(config, inputFileName, outputFileName, null)
        {
        }

        public FileProcessor(Config config, string inputFileName, string outputFileName, ILogger logger)
            : base(config)
        {
            this.logger = logger;
            this.InputFileName = inputFileName;
            this.OutputFileName = outputFileName;
        }

        public string InputFileName
        {
            get
            {
                return this.inputFileName;
            }

            set
            {
                if (value == null || value.Length < 1)
                {
                    this.inputFileName = null;
                }
                else
                {
                    if (!File.Exists(value))
                    {
                        throw new FileNotFoundException($"The input file '{value}' does not exist.");
                    }
                    else
                    {
                        this.inputFileName = value;
                    }
                }
            }
        }

        public string OutputFileName
        {
            get
            {
                return this.outputFileName;
            }

            set
            {
                string fileName = value;
                if (value == null || value.Length < 1)
                {
                    fileName = this.GenerateOutputFileNameBasedOnInputFileName();
                }

                if (File.Exists(fileName))
                {
                    this.logger?.Log(LogType.Warning, "Output file '{0}' already exists.\n", fileName);
                }

                this.outputFileName = fileName;
            }
        }

        /// <summary>
        /// Try to read the file ‘InputFileName’ as valid XML document.
        /// </summary>
        /// <returns>XmlReader of the file ‘InputFileName’</returns>
        public XmlReader XmlReader
        {
            get
            {
                return FileProcessor.GetXmlReader(this.InputFileName);
            }
        }

        public static string ReadFileContentToString(string inputFileName)
        {
            string result = string.Empty;
            try
            {
                result = File.ReadAllText(inputFileName);
            }
            catch (Exception e)
            {
                throw new IOException($"Cannot read file {inputFileName}.", e);
            }

            return result;
        }

        public static XmlReader GetXmlReader(string inputFileName)
        {
            XmlReader reader = null;
            try
            {
                FileStream stream = new FileStream(inputFileName, FileMode.Open);
                XmlReaderSettings readerSettings = new XmlReaderSettings();
                readerSettings.IgnoreWhitespace = false;
                readerSettings.DtdProcessing = DtdProcessing.Ignore;
                reader = XmlTextReader.Create(stream, readerSettings);
            }
            catch
            {
                throw;
            }

            return reader;
        }

        public void Read()
        {
            XmlDocument readXml = new XmlDocument(this.NamespaceManager.NameTable);
            readXml.PreserveWhitespace = true;
            try
            {
                XmlReaderSettings readerSettings = new XmlReaderSettings();
                readerSettings.IgnoreWhitespace = false;
                readerSettings.DtdProcessing = DtdProcessing.Ignore;

                FileStream stream = null;
                XmlReader reader = null;
                try
                {
                    stream = new FileStream(this.inputFileName, FileMode.Open);
                    reader = XmlTextReader.Create(stream, readerSettings);

                    readXml.Load(reader);
                    this.XmlDocument = readXml;
                }
                finally
                {
                    // Just close all open streams and readers
                    try
                    {
                        reader.Close();
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        stream.Close();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (XmlException xmlException)
            {
                this.logger?.Log(
                    LogType.Info,
                    xmlException,
                    "Input file name '{0}' is not a valid XML document.\nIt will be read as text file and will be wrapped in basic XML tags.\n",
                    this.InputFileName);

                XmlElement rootNode = readXml.CreateElement("article");
                XmlElement bodyNode = readXml.CreateElement("body");
                try
                {
                    string[] lines = File.ReadAllLines(this.InputFileName, Config.DefaultEncoding);
                    for (int i = 0, len = lines.Length; i < len; ++i)
                    {
                        XmlElement paragraph = readXml.CreateElement("p");
                        paragraph.InnerText = lines[i];
                        bodyNode.AppendChild(paragraph);
                    }
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, "Input file name: {0}", this.inputFileName);
                }

                rootNode.AppendChild(bodyNode);
                readXml.AppendChild(rootNode);

                this.XmlDocument = readXml;
            }
            catch
            {
                throw;
            }
        }

        public void Write()
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(this.OutputFileName, false, Config.DefaultEncoding);
                writer.Write(this.Xml);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        private string GenerateOutputFileNameBasedOnInputFileName()
        {
            string fileName = null;
            if (this.inputFileName == null)
            {
                fileName = null;
            }
            else
            {
                string directoryPath = Path.GetDirectoryName(this.inputFileName);
                string extension = Path.GetExtension(this.inputFileName);
                string name = Path.GetFileNameWithoutExtension(this.inputFileName);

                if (Regex.Match(name, @"\-out(\-\d+)?\Z").Success)
                {
                    name = Regex.Replace(name, @"\-\d+(?=\Z)", string.Empty);
                    int i = 0;
                    do
                    {
                        fileName = string.Format("{0}\\{1}-{2}{3}", directoryPath, name, ++i, extension);
                    }
                    while (File.Exists(fileName));
                }
                else
                {
                    int i = 0;
                    do
                    {
                        fileName = string.Format("{0}\\{1}-out-{2}{3}", directoryPath, name, ++i, extension);
                    }
                    while (File.Exists(fileName));
                }
            }

            return fileName;
        }
    }
}
