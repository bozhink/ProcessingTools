using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace ProcessingTools.Base
{
    public class FileProcessor : IBase
    {
        private string inputFileName;
        private string outputFileName;
        private string xml;
        private XmlDocument xmlDocument;
        private XmlNamespaceManager namespaceManager;

        public FileProcessor()
        {
            this.Initialize(null, null);
        }

        public FileProcessor(string inputFileName)
        {
            this.Initialize(inputFileName, null);
        }

        public FileProcessor(string inputFileName, string outputFileName)
        {
            this.Initialize(inputFileName, outputFileName);
        }

        public string InputFileName
        {
            get
            {
                return this.inputFileName;
            }

            set
            {
                if (value == null)
                {
                    this.inputFileName = null;
                }
                else
                {
                    if (!File.Exists(value))
                    {
                        Alert.Die(1, "\nERROR: The input file name '{0}' does not exist.\n", value);
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
                if (value == null)
                {
                    fileName = this.GenerateOutputFileNameBasedOnInputFileName();
                }

                if (File.Exists(fileName))
                {
                    Alert.Log("\nWARNING: Output file '{0}' already exists.\n", fileName);
                }

                this.outputFileName = fileName;
            }
        }


        public string Xml
        {
            get
            {
                return this.xml;
            }

            set
            {
                if (value != null && value.Length > 0)
                {
                    try
                    {
                        this.xml = value;
                        this.xmlDocument.LoadXml(this.xml);
                    }
                    catch (XmlException e)
                    {
                        throw e;
                    }
                    catch (Exception e)
                    {
                        Alert.RaiseExceptionForType(e, this.GetType().Name, 51);
                    }
                }
                else
                {
                    throw new ArgumentNullException("Invalid value for Xml: null or empty.");
                }
            }
        }

        public XmlDocument XmlDocument
        {
            get
            {
                return this.xmlDocument;
            }

            set
            {
                if (value != null)
                {
                    try
                    {
                        this.xmlDocument = value;
                        this.xml = this.xmlDocument.OuterXml;
                    }
                    catch (XmlException e)
                    {
                        throw e;
                    }
                    catch (Exception e)
                    {
                        Alert.RaiseExceptionForType(e, this.GetType().Name, 51);
                    }
                }
                else
                {
                    throw new ArgumentNullException("Invalid value for XmlDocument: null.");
                }
            }
        }

        public Config Config
        {
            get
            {
                return null;
            }
        }

        public XmlNamespaceManager NamespaceManager
        {
            get
            {
                if (this.xmlDocument != null)
                {
                    return ProcessingTools.Config.TaxPubNamespceManager(this.xmlDocument);
                }
                else
                {
                    return ProcessingTools.Config.TaxPubNamespceManager();
                }
            }
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
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 1, 1);
            }

            return reader;
        }

        /// <summary>
        /// Try to read the file ‘InputFileName’ as valid XML document.
        /// </summary>
        /// <returns>XmlReader of the file ‘InputFileName’</returns>
        public XmlReader GetXmlReader()
        {
            return FileProcessor.GetXmlReader(this.InputFileName);
        }

        public void Read()
        {
            XmlDocument readXml = new XmlDocument(this.NamespaceManager.NameTable);
            readXml.PreserveWhitespace = true;
            try
            {
                using (FileStream stream = new FileStream(inputFileName, FileMode.Open))
                {
                    XmlReaderSettings readerSettings = new XmlReaderSettings();
                    readerSettings.IgnoreWhitespace = false;
                    readerSettings.DtdProcessing = DtdProcessing.Ignore;
                    using (XmlReader reader = XmlTextReader.Create(stream, readerSettings))
                    {
                        try
                        {
                            readXml.Load(reader);
                            this.XmlDocument = readXml;
                        }
                        finally
                        {
                            reader.Dispose();
                            reader.Close();
                            stream.Dispose();
                            stream.Close();
                        }
                    }
                }
            }
            catch (XmlException xmlException)
            {
                Alert.Log(
                    "Input file name '{0}' is not a valid XML document.\n" +
                    "It will be read as text file and will wrapped in basic XML tags.\n\n" +
                    "{1}\n",
                    this.InputFileName,
                    xmlException.Message);

                XmlElement rootNode = readXml.CreateElement("article");
                XmlElement bodyNode = readXml.CreateElement("body");
                try
                {
                    string[] lines = System.IO.File.ReadAllLines(this.InputFileName, ProcessingTools.Config.DefaultEncoding);
                    for (int i = 0, len = lines.Length; i < len; ++i)
                    {
                        XmlElement paragraph = readXml.CreateElement("p");
                        paragraph.InnerText = lines[i];
                        bodyNode.AppendChild(paragraph);
                    }
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Input file name: " + this.inputFileName);
                }

                rootNode.AppendChild(bodyNode);
                readXml.AppendChild(rootNode);

                this.XmlDocument = readXml;
            }
            catch (Exception otherException)
            {
                Alert.RaiseExceptionForMethod(otherException, this.GetType().Name, 1);
            }
        }

        public void Write()
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(this.OutputFileName, false, ProcessingTools.Config.DefaultEncoding);
                writer.Write(this.Xml);
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 2, "Output file: " + this.OutputFileName);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Dispose();
                    writer.Close();
                }
            }
        }

        private void Initialize(string inputFileName, string outputFileName)
        {
            this.InputFileName = inputFileName;
            this.OutputFileName = outputFileName;
            this.xml = null;
            this.namespaceManager = ProcessingTools.Config.TaxPubNamespceManager();
            this.xmlDocument = new XmlDocument(namespaceManager.NameTable);
            this.xmlDocument.PreserveWhitespace = true;
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
                    int i = 1;
                    do
                    {
                        fileName = string.Format("{0}\\{1}-{2}{3}", directoryPath, name, i++, extension);
                    }
                    while (File.Exists(fileName));
                }
                else
                {
                    int i = 1;
                    do
                    {
                        fileName = string.Format("{0}\\{1}-out-{2}{3}", directoryPath, name, i++, extension);
                    }
                    while (File.Exists(fileName));
                }
            }

            return fileName;
        }
    }
}
