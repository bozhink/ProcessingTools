namespace ProcessingTools.Documents.Services.Data.Models
{
    using System;

    public class XmlFileDetailsServiceModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int ContentLength { get; set; }

        public string ContentType { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string FileName { get; set; }
    }
}
