namespace ProcessingTools.Mediatypes.Data.Common.Models
{
    using Contracts.Models;

    public class Mediatype : IMediatype
    {
        public string Description { get; set; }

        public string FileExtension { get; set; }

        public string Mimesubtype { get; set; }

        public string Mimetype { get; set; }
    }
}
