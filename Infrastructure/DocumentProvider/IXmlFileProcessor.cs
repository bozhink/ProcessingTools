namespace ProcessingTools.DocumentProvider
{
    using System.Xml;
    using ProcessingTools.Contracts;

    public interface IXmlFileProcessor
    {
        string InputFileName { get; set; }

        string OutputFileName { get; set; }

        void Read(IDocument document);

        void Read(IDocument document, XmlReaderSettings readerSettings);

        void Write(IDocument document);

        void Write(IDocument document, XmlDocumentType documentType, XmlWriterSettings writerSettings = null);
    }
}
