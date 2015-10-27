namespace ProcessingTools.MimeResolver.Models.Database
{
    using System.Collections.Generic;

    public class MimeTypePair
    {
        private ICollection<FileExtension> fileExtensions;

        public MimeTypePair()
        {
            this.fileExtensions = new HashSet<FileExtension>();
        }

        public int Id { get; set; }

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
