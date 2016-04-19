namespace ProcessingTools.Documents.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Constants;

    public class Document : DocumentsAbstractEntity
    {
        public Document()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(ValidationConstants.LengthOfDocumentFileName)]
        [MaxLength(ValidationConstants.LengthOfDocumentFileName)]
        public string FileName { get; set; }

        public virtual Guid ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}