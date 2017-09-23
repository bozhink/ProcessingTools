namespace ProcessingTools.Web.Documents.Areas.Files.ViewModels.Metadata
{
    using System;
    using ProcessingTools.Models.Contracts.Files;

    public class FileMetadataViewModel : IFileMetadata
    {
        public long ContentLength { get; set; }

        public string ContentType { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Description { get; set; }

        public string FileExtension { get; set; }

        public string FileName { get; set; }

        public string FullName { get; set; }

        public object Id { get; set; }

        public string ModifiedByUser { get; set; }
    }
}
