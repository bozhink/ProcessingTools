namespace ProcessingTools.MediaType.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MimeTypePair
    {
        private ICollection<FileExtension> fileExtensions;

        public MimeTypePair()
        {
            this.fileExtensions = new HashSet<FileExtension>();
        }

        [Key]
        public int Id { get; set; }

        public virtual int MimeTypeId { get; set; }

        public virtual MimeType MimeType { get; set; }

        public virtual int MimeSubtypeId { get; set; }

        public virtual MimeSubtype MimeSubtype { get; set; }

        public virtual ICollection<FileExtension> FileExtensions
        {
            get
            {
                return this.fileExtensions;
            }

            set
            {
                this.fileExtensions = value;
            }
        }
    }
}