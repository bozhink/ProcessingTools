namespace ProcessingTools.ListsManager
{
    using System;
    using System.IO;
    using System.Xml;

    using ProcessingTools.Common;

    public class XmlListHolder
    {
        private string fileName;

        public XmlListHolder(string fileName)
        {
            this.XmlDocument = new XmlDocument();
            this.FileName = fileName;
        }

        public XmlDocument XmlDocument { get; private set; }

        public string FileName
        {
            get
            {
                return this.fileName;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value) || !File.Exists(value))
                {
                    throw new ApplicationException("List file name is invalid.");
                }

                this.fileName = value;
            }
        }

        public void Load()
        {
            this.XmlDocument.Load(this.FileName);
        }

        public void Write()
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Async = true,
                CheckCharacters = true,
                CloseOutput = true,
                ConformanceLevel = ConformanceLevel.Document,
                Encoding = Defaults.DefaultEncoding,
                Indent = false,
                OmitXmlDeclaration = false,
                WriteEndDocumentOnClose = true
            };

            using (XmlWriter writer = XmlWriter.Create(this.FileName, settings))
            {
                this.XmlDocument.WriteTo(writer);
                writer.Flush();
            }
        }
    }
}
