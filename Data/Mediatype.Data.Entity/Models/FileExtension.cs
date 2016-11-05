namespace ProcessingTools.MediaType.Data.Entity.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.MediaType.Data.Common.Constants;

    public class FileExtension
    {
        private ICollection<MimeTypePair> mimeTypePairs;

        public FileExtension()
        {
            this.mimeTypePairs = new HashSet<MimeTypePair>();
        }

        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.MaximalLengthOfFileExtensionName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfFileExtensionDescription)]
        public string Description { get; set; }

        public virtual ICollection<MimeTypePair> MimeTypePairs
        {
            get
            {
                return this.mimeTypePairs;
            }

            set
            {
                this.mimeTypePairs = value;
            }
        }
    }
}
