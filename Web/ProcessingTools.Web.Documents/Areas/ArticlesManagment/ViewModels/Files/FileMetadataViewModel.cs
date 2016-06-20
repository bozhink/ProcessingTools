namespace ProcessingTools.Web.Documents.Areas.ArticlesManagment.ViewModels.Files
{
    using System;

    public class FileMetadataViewModel
    {
        public int Id => this.FileName.GetHashCode();

        public string FileName { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}