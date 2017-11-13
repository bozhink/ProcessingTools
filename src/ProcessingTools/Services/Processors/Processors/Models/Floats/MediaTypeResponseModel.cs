namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants;
    using ProcessingTools.Processors.Models.Contracts.Floats;

    internal class MediatypeResponseModel : IMediaType
    {
        public string FileExtension { get; set; }

        public string MimeType { get; set; } = ContentTypes.DefaultMimetype;

        public string MimeSubtype { get; set; } = ContentTypes.DefaultMimesubtype;
    }
}
