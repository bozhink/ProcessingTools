namespace ProcessingTools.Documents.Services.Data.Models
{
    using System;

    public class XmlFileMetadataServiceModel
    {
        public int Id => this.FileName.GetHashCode();

        public int ContentLength { get; set; }

        public string ContentType { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string FileName { get; set; }
    }
}
