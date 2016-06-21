namespace ProcessingTools.Web.Documents.Areas.ArticlesManagment.ViewModels.Files
{
    using System;

    public class FileMetadataViewModel
    {
        public string Id { get; set; }

        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public long ContentLength { get; set; }

        public string ContentType { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
