namespace ProcessingTools.Data.Models.Entity.Files
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Mediatypes;

    public class FileExtension
    {
        private ICollection<MimetypePair> mimetypePairs;

        public FileExtension()
        {
            this.mimetypePairs = new HashSet<MimetypePair>();
        }

        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfFileExtensionName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfFileExtensionDescription)]
        public string Description { get; set; }

        public virtual ICollection<MimetypePair> MimetypePairs
        {
            get
            {
                return this.mimetypePairs;
            }

            set
            {
                this.mimetypePairs = value;
            }
        }
    }
}
