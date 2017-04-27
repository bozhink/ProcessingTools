namespace ProcessingTools.Mediatypes.Data.Entity.Models
{
    using ProcessingTools.Contracts.Data.Mediatypes.Models;

    internal class Mediatype : IMediatype
    {
        public string Description { get; set; }

        public string FileExtension { get; set; }

        public string Mimesubtype { get; set; }

        public string Mimetype { get; set; }
    }
}
