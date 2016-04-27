namespace FileUploadMvc.Models
{
    using System;

    public class ListModel
    {
        public int Id => FileName.GetHashCode();

        public string FileName { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
