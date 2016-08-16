namespace ProcessingTools.Documents.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ProcessingTools.Common.Models;
    using ProcessingTools.Data.Common.Entity.Models.Contracts;
    using ProcessingTools.Documents.Data.Common.Constants;
    using ProcessingTools.Documents.Data.Common.Models.Contracts;

    public class Document : ModelWithUserInformation, IEntityWithPreJoinedFields, IDocumentEntity
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

        public long OriginalContentLength { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(ValidationConstants.LengthOfDocumentFileName)]
        [MaxLength(ValidationConstants.LengthOfDocumentFileName)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfDocumentOriginalFileName)]
        public string OriginalFileName { get; set; }

        [MaxLength(ValidationConstants.LengthOfDocumentFileExtension)]
        public string FileExtension { get; set; }

        // TODO: ArticleId in Document
        [NotMapped]
        public Guid ArticleId { get; set; }

        public virtual Article Article { get; set; }

        [NotMapped]
        public IEnumerable<string> PreJoinFieldNames => null;
    }
}
