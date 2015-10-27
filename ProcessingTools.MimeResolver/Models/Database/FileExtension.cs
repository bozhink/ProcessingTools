namespace ProcessingTools.MimeResolver.Models.Database
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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
        [MaxLength(10)]
        public string Name { get; set; }

        [MaxLength(100)]
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
