namespace ProcessingTools.Mediatypes.Data.Entity.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MimetypePair
    {
        private ICollection<FileExtension> fileExtensions;

        public MimetypePair()
        {
            this.fileExtensions = new HashSet<FileExtension>();
        }

        [Key]
        public int Id { get; set; }

        public virtual int MimeTypeId { get; set; }

        public virtual Mimetype MimeType { get; set; }

        public virtual int MimeSubtypeId { get; set; }

        public virtual Mimesubtype MimeSubtype { get; set; }

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
