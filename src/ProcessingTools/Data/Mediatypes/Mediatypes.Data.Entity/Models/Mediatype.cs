namespace ProcessingTools.Mediatypes.Data.Entity.Models
{
    using ProcessingTools.Models.Contracts.Files.Mediatypes;

    internal class Mediatype : IMediatypeBaseModel
    {
        public string Description { get; set; }

        public string Extension { get; set; }

        public string MimeSubtype { get; set; }

        public string MimeType { get; set; }
    }
}
