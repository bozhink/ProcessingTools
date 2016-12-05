namespace ProcessingTools.Services.Data.Models.Mediatypes
{
    using ProcessingTools.Contracts.Models.Mediatypes;

    public class MediatypeServiceModel : IMediatype
    {
        public string FileExtension { get; set; }

        public string Mimetype { get; set; }

        public string Mimesubtype { get; set; }
    }
}
