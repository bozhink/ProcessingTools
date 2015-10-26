namespace ProcessingTools.MimeResolver.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MimeSubtype
    {
        private ICollection<MimeTypePair> mimeTypePairs;

        public MimeSubtype()
        {
            this.mimeTypePairs = new HashSet<MimeTypePair>();
        }

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
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
