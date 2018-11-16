namespace ProcessingTools.Data.Models.Entity.Files
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

        public virtual int MimetypeId { get; set; }

        public virtual Mimetype Mimetype { get; set; }

        public virtual int MimesubtypeId { get; set; }

        public virtual Mimesubtype Mimesubtype { get; set; }

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
