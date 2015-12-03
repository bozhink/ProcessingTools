namespace ProcessingTools.DocumentProvider
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Contracts;
    using Contracts.Log;

    public class XmlFileProcessor
    {
        private string inputFileName;
        private string outputFileName;

        private ILogger logger;

        private XmlReaderSettings readerSettings;
        private XmlWriterSettings writerSettings;

        public XmlFileProcessor()
            : this(null, null, null)
        {
        }

        public XmlFileProcessor(ILogger logger)
            : this(null, null, logger)
        {
        }

        public XmlFileProcessor(string inputFileName)
            : this(inputFileName, null, null)
        {
        }

        public XmlFileProcessor(string inputFileName, ILogger logger)
            : this(inputFileName, null, logger)
        {
        }

        public XmlFileProcessor(string inputFileName, string outputFileName)
            : this(inputFileName, outputFileName, null)
        {
        }

        public XmlFileProcessor(string inputFileName, string outputFileName, ILogger logger)
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
                ConformanceLevel = ConformanceLevel.Document,
                ValidationType = ValidationType.None
            };

            this.writerSettings = new XmlWriterSettings()
            {
                Async = false,
                Encoding = Encoding.UTF8,
                Indent = false,
                IndentChars = " ",
                NewLineChars = "\n",
                NewLineHandling = NewLineHandling.Replace,
                NewLineOnAttributes = false,
                NamespaceHandling = NamespaceHandling.OmitDuplicates,
                OmitXmlDeclaration = false,
                WriteEndDocumentOnClose = true,
                CloseOutput = true
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

        public void Read(IDocument document)
        {
            this.Read(document, this.readerSettings);
        }

        public void Read(IDocument document, XmlReaderSettings readerSettings)
        {
            try
            {
                XmlReader reader = null;
                try
                {
                    reader = XmlReader.Create(this.InputFileName, readerSettings);
                    document.XmlDocument.Load(reader);
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
                }
            }
            catch (XmlException xmlException)
            {
                this.logger?.Log(
                    LogType.Info,
                    xmlException,
                    "Input file name '{0}' is not a valid XML document.\nIt will be read as text file and will be wrapped in basic XML tags.\n",
                    this.InputFileName);

                XmlElement rootNode = document.XmlDocument.CreateElement("article");
                XmlElement bodyNode = document.XmlDocument.CreateElement("body");
                try
                {
                    string[] lines = File.ReadAllLines(this.InputFileName, document.Encoding);
                    for (int i = 0, len = lines.Length; i < len; ++i)
                    {
                        XmlElement paragraph = document.XmlDocument.CreateElement("p");
                        paragraph.InnerText = lines[i];
                        bodyNode.AppendChild(paragraph);
                    }
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, "Input file name: {0}", this.InputFileName);
                }

                rootNode.AppendChild(bodyNode);
                document.Xml = rootNode.OuterXml;
            }
            catch
            {
                throw;
            }
        }

        public void Write(IDocument document)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(this.OutputFileName, false, document.Encoding);
                writer.Write(document.Xml);
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

        public void Write(IDocument document, XmlDocumentType documentType, XmlWriterSettings writerSettings = null)
        {
            XmlWriterSettings settings = this.writerSettings;
            if (writerSettings != null)
            {
                settings = writerSettings;
            }

            XmlWriter writer = XmlWriter.Create(this.OutputFileName, settings);
            if (documentType != null)
            {
                writer.WriteDocType(documentType.Name, documentType.PublicId, documentType.SystemId, documentType.InternalSubset);
            }

            document.XmlDocument.DocumentElement.WriteTo(writer);
            writer.Flush();
            writer.Close();
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
