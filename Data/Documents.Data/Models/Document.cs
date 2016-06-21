namespace ProcessingTools.Documents.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ProcessingTools.Documents.Data.Common.Constants;

    public class Document : DocumentsAbstractEntity
    {
        public Document()
            : base()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfContentTypeString)]
        public string ContentType { get; set; }

        public long ContentLength { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(ValidationConstants.LengthOfDocumentFileName)]
        [MaxLength(ValidationConstants.LengthOfDocumentFileName)]
        public string FileName { get; set; }

        [MaxLength(ValidationConstants.LengthOfDocumentFileExtension)]
        public string FileExtension { get; set; }

        public virtual Article Article { get; set; }
    }
}