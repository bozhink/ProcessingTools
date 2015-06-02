using System;
using System.Text;
using System.IO;
using System.Xml;

namespace Base
{
    public class FileProcessor : Base
    {
        private string inputFileName;
        public string InputFileName
        {
            get { return inputFileName; }
            set { inputFileName = value; }
        }

        private string outputFileName;
        public string OutputFileName
        {
            get { return outputFileName; }
            set { outputFileName = value; }
        }

        public FileProcessor()
        {
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

        public XmlReader GetXmlReader()
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
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 1);
            }
            return reader;
        }

        public void GetContent(bool removeDtd = false)
        {
            StreamReader reader = null;
            string line = string.Empty;
            StringBuilder result = new StringBuilder();
            try
            {
                reader = new StreamReader(inputFileName, encoding);
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
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 1, "Input file name: " + inputFileName);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            // Remove all Carret Return (CR) symbols
            xml = string.Empty;
            try
            {
                xml = System.Text.RegularExpressions.Regex.Replace(result.ToString(), "\r", "");
                if (removeDtd)
                {
                    xml = System.Text.RegularExpressions.Regex.Replace(xml, "<!DOCTYPE [^>]*>", "");
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 4, "Errors executing regex-es.");
            }
            finally
            {
                if (xml == string.Empty)
                {
                    xml = result.ToString();
                }
            }
        }

        public void WriteXMLFile()
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(this.outputFileName, false, encoding);
                writer.Write(xml);
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 2, "Output file: " + outputFileName);
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
