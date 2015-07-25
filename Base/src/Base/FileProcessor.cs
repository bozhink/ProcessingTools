using System;
using System.IO;
using System.Text;
using System.Xml;

namespace ProcessingTools.Base
{
    public class FileProcessor : Base
    {
        private string inputFileName;
        private string outputFileName;

        public FileProcessor()
        {
            this.inputFileName = null;
            this.outputFileName = null;
        }

        public FileProcessor(string inputFileName)
        {
            this.inputFileName = inputFileName;
        }

        public FileProcessor(string inputFileName, string outputFileName)
        {
            this.inputFileName = inputFileName;
            this.outputFileName = outputFileName;
        }

        public string InputFileName
        {
            get { return this.inputFileName; }
            set { this.inputFileName = value; }
        }

        public string OutputFileName
        {
            get { return this.outputFileName; }
            set { this.outputFileName = value; }
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

        public static string GetStringContent(string fileName)
        {
            string content = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                string[] lines = System.IO.File.ReadAllLines(fileName);
                for (int i = 0; i < lines.Length; i++)
                {
                    sb.Append(lines[i]);
                    sb.Append('\n');
                }
            }
            catch (Exception e)
            {
                Alert.Log("ReadAllLinesToString Exception:");
                Alert.Log(e.Message);
                Alert.Exit(1);
            }
            finally
            {
                content = sb.ToString();
            }

            return content;
        }

        public static XmlDocument GetContentAsXmlDocument(string inputFileName)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(FileProcessor.GetXmlReader(inputFileName));
            return xml;
        }

        /// <summary>
        /// Try to read the file ‘InputFileName’ as valid XML document.
        /// </summary>
        /// <returns>XmlReader of the file ‘InputFileName’</returns>
        public XmlReader GetXmlReader()
        {
            return FileProcessor.GetXmlReader(this.inputFileName);
        }

        public void ReadStringContent(bool removeDtd = false)
        {
            StreamReader reader = null;
            string line = string.Empty;
            StringBuilder result = new StringBuilder();
            try
            {
                reader = new StreamReader(this.inputFileName, this.DefaultEncoding);
                while ((line = reader.ReadLine()) != null)
                {
                    result.Append(line);
                    result.Append('\n');
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 3, "Argument out of range in the StringBuilder.");
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 1, "Input file name: " + this.inputFileName);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            // Remove all Carret Return (CR) symbols
            this.xml = string.Empty;
            try
            {
                this.xml = System.Text.RegularExpressions.Regex.Replace(result.ToString(), "\r", string.Empty);
                if (removeDtd)
                {
                    this.xml = System.Text.RegularExpressions.Regex.Replace(this.xml, "<!DOCTYPE [^>]*>", string.Empty);
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 4, "Errors executing regex-es.");
            }
            finally
            {
                if (this.xml == string.Empty)
                {
                    this.xml = result.ToString();
                }
            }
        }

        public void WriteStringContentToFile()
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(this.outputFileName, false, this.DefaultEncoding);
                writer.Write(this.xml);
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 2, "Output file: " + this.outputFileName);
            }
            finally
            {
                if (writer != null)
                {
                    try
                    {
                        writer.Close();
                    }
                    catch (Exception e)
                    {
                        Alert.RaiseExceptionForMethod(e, this.GetType().Name, 100);
                    }
                }
            }
        }
    }
}
