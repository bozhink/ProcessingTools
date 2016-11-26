namespace ProcessingTools.Mediatypes.Services.Data.Models
{
    using Contracts.Models;

    public class MediatypeServiceModel : IMediatypeServiceModel
    {
        public string FileExtension { get; set; }

        public string Mimetype { get; set; }

        public string Mimesubtype { get; set; }
    }
}
