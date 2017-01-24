namespace ProcessingTools.Services.Data.Models.Mediatypes
{
    using ProcessingTools.Contracts.Models.Mediatypes;

    internal class MediatypeServiceModel : IMediatype
    {
        public string Mimetype { get; set; }

        public string Mimesubtype { get; set; }
    }
}
