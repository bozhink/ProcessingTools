namespace ProcessingTools.Data.Models.Entity.Files
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Mediatypes;

    public class Mimesubtype
    {
        private string name;
        private ICollection<MimetypePair> mimeTypePairs;

        public Mimesubtype()
        {
            this.mimeTypePairs = new HashSet<MimetypePair>();
        }

        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfMimeSubtypeName)]
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value?.ToLowerInvariant();
            }
        }

        public virtual ICollection<MimetypePair> MimeTypePairs
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
