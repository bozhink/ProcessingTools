namespace ProcessingTools.Mediatypes.Services.Data.Contracts.Models
{
    public interface IMediatypeServiceModel
    {
        string FileExtension { get; set; }

        string Mimesubtype { get; set; }

        string Mimetype { get; set; }
    }
}
