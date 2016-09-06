namespace ProcessingTools.Documents.Services.Data.Models
{
    using System;

    public class DocumentServiceModel
    {
        public DocumentServiceModel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DateCreated = DateTime.UtcNow;
            this.DateModified = this.DateCreated;
        }

        public string Id { get; set; }

        public long ContentLength { get; set; }

        public string ContentType { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string FileName { get; set; }

        public string FileExtension { get; set; }
    }
}
