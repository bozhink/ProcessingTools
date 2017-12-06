namespace ProcessingTools.Mediatypes.Data.Entity.Models
{
    using ProcessingTools.Contracts.Models.Mediatypes;

    internal class Mediatype : IMediatypeEntity
    {
        public string Description { get; set; }

        public string FileExtension { get; set; }

        public string Mimesubtype { get; set; }

        public string Mimetype { get; set; }
    }
}
