namespace ProcessingTools.MediaType.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class MimeSubtype
    {
        private ICollection<MimeTypePair> mimeTypePairs;

        public MimeSubtype()
        {
            this.mimeTypePairs = new HashSet<MimeTypePair>();
        }

        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Index(IsUnique = true)]
        [MaxLength(150)]
        public string Name { get; set; }

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
