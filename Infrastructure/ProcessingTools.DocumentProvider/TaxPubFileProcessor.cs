namespace ProcessingTools.DocumentProvider
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Contracts;

    public class TaxPubFileProcessor : TaxPubDocument
    {
        private string inputFileName;
        private string outputFileName;
        private ILogger logger;
        private XmlReaderSettings readerSettings;

        public TaxPubFileProcessor()
            : this(null, null, null)
        {
        }

        public TaxPubFileProcessor(ILogger logger)
            : this(null, null, logger)
        {
        }

        public TaxPubFileProcessor(string inputFileName)
            : this(inputFileName, null, null)
        {
        }

        public TaxPubFileProcessor(string inputFileName, ILogger logger)
            : this(inputFileName, null, logger)
        {
        }

        public TaxPubFileProcessor(string inputFileName, string outputFileName)
            : this(inputFileName, outputFileName, null)
        {
        }

        public TaxPubFileProcessor(string inputFileName, string outputFileName, ILogger logger)
            : base()
        {
            this.logger = logger;
            this.InputFileName = inputFileName;
            this.OutputFileName = outputFileName;

            this.readerSettings = new XmlReaderSettings()
            {
                IgnoreComments = false,
                IgnoreProcessingInstructions = false,
                IgnoreWhitespace = false,
                CloseInput = false,
                DtdProcessing = DtdProcessing.Ignore,
                NameTable = this.NameTable
            };
        }

        public string InputFileName
        {
            get
            {
                return this.inputFileName;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
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
                if (string.IsNullOrWhiteSpace(value))
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
                var stream = new FileStream(this.InputFileName, FileMode.Open);
                return XmlTextReader.Create(stream, this.readerSettings);
            }
        }

        public void Read()
        {
            XmlDocument readXml = new XmlDocument(this.NameTable)
            {
                PreserveWhitespace = true
            };

            try
            {
                FileStream stream = null;
                XmlReader reader = null;
                try
                {
                    stream = new FileStream(this.inputFileName, FileMode.Open);
                    reader = XmlTextReader.Create(stream, this.readerSettings);

                    readXml.Load(reader);
                    this.Xml = readXml.OuterXml;
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
                    string[] lines = File.ReadAllLines(this.InputFileName, this.Encoding);
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

                this.Xml = readXml.OuterXml;
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
                writer = new StreamWriter(this.OutputFileName, false, this.Encoding);
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

                string outputFileNameFormat = null;
                if (Regex.IsMatch(name, @"\-out(?:\-\d+)?\Z"))
                {
                    name = Regex.Replace(name, @"\-\d+(?=\Z)", string.Empty);

                    if (string.IsNullOrEmpty(directoryPath))
                    {
                        outputFileNameFormat = "{1}-{2}{3}";
                    }
                    else
                    {
                        outputFileNameFormat = "{0}\\{1}-{2}{3}";
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(directoryPath))
                    {
                        outputFileNameFormat = "{1}-out-{2}{3}";
                    }
                    else
                    {
                        outputFileNameFormat = "{0}\\{1}-out-{2}{3}";
                    }
                }

                int i = 0;
                do
                {
                    fileName = string.Format(outputFileNameFormat, directoryPath, name, ++i, extension);
                }
                while (File.Exists(fileName));
            }

            return fileName;
        }
    }
}
