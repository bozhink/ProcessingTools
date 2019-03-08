﻿namespace ProcessingTools.Data.Models.Entity.Documents
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Common.Constants.Data.Documents;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Documents;

    public class Document : ModelWithUserInformation, IDocument
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
    }
}