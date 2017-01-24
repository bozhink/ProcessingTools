namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants;

    internal class MediatypeResponseModel : IMediaType
    {
        public string FileExtension { get; set; }

        public string MimeType { get; set; } = MediaTypes.DefaultMimetype;

        public string MimeSubtype { get; set; } = MediaTypes.DefaultMimesubtype;
    }
}
