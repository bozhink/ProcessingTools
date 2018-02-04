namespace ProcessingTools.Documents.Data.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Constants.Data.Documents;
    using ProcessingTools.Models.Contracts.Documents;
    using ProcessingTools.Data.Common.Entity.Models.Contracts;
    using ProcessingTools.Models.Abstractions;

    public class Document : ModelWithUserInformation, IEntityWithPreJoinedFields, IDocument
    {
        public Document()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfFileDescription)]
        public string Comment { get; set; }

        public long ContentLength { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfContentTypeString)]
        public string ContentType { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfFileExtension)]
        public string FileExtension { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(ValidationConstants.MaximalLengthOfFullFileName)]
        [MaxLength(ValidationConstants.MaximalLengthOfFullFileName)]
        public string FilePath { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfFileName)]
        public string FileName { get; set; }

        public long OriginalContentLength { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfContentTypeString)]
        public string OriginalContentType { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfFileExtension)]
        public string OriginalFileExtension { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfFileName)]
        public string OriginalFileName { get; set; }

        // TODO: ArticleId in Document
        [NotMapped]
        public Guid ArticleId { get; set; }

        public virtual Article Article { get; set; }

        [NotMapped]
        public IEnumerable<string> PreJoinFieldNames => new string[] { };
    }
}
