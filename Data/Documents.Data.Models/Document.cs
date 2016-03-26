namespace ProcessingTools.Documents.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.IO;
    using System.Text;
    using System.Xml;

    using Common.Models;

    public class Document : DocumentsAbstractEntity
    {
        private readonly Encoding defaultEncoding = Encoding.UTF8;
        private DateTime date;

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "xml")]
        public string Content { get; set; }

        [NotMapped]
        public XmlDocument XmlDocument
        {
            get
            {
                var settings = new XmlReaderSettings
                {
                    Async = true,
                    CheckCharacters = true,
                    CloseInput = true,
                    ConformanceLevel = ConformanceLevel.Document,
                    DtdProcessing = DtdProcessing.Ignore,
                    IgnoreComments = false,
                    IgnoreProcessingInstructions = false,
                    IgnoreWhitespace = false
                };

                using (var stream = new MemoryStream(this.defaultEncoding.GetBytes(this.Content)))
                {
                    var reader = XmlReader.Create(stream, settings);

                    var document = new XmlDocument
                    {
                        PreserveWhitespace = true
                    };

                    document.Load(reader);
                    return document;
                }
            }

            set
            {
                this.Content = value.OuterXml;
            }
        }

        public virtual int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}