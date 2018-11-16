namespace ProcessingTools.Data.Models.Entity.Files
{
    using ProcessingTools.Models.Contracts.Files.Mediatypes;

    public class Mediatype : IMediatypeBaseModel
    {
        public string Description { get; set; }

        public string Extension { get; set; }

        public string MimeSubtype { get; set; }

        public string MimeType { get; set; }
    }
}
