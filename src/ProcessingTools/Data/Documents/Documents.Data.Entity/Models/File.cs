namespace ProcessingTools.Documents.Data.Entity.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Common.Constants.Data.Documents;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Documents;

    public class File : ModelWithUserInformation, IFile
    {
        public File()
        {
            this.Id = Guid.NewGuid();
        }

        public long ContentLength { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfContentTypeString)]
        public string ContentType { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfFileDescription)]
        public string Description { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfFileExtension)]
        public string FileExtension { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfFileName)]
        public string FileName { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(ValidationConstants.MaximalLengthOfFullFileName)]
        [MaxLength(ValidationConstants.MaximalLengthOfFullFileName)]
        public string FullName { get; set; }

        [Key]
        public Guid Id { get; set; }

        public long OriginalContentLength { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfContentTypeString)]
        public string OriginalContentType { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfFileExtension)]
        public string OriginalFileExtension { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfFileName)]
        public string OriginalFileName { get; set; }
    }
}
