namespace ProcessingTools.Mediatypes.Services.Data.Contracts.Models
{
    public interface IMediatypeServiceModel
    {
        string FileExtension { get; }

        string Mimesubtype { get; }

        string Mimetype { get; }
    }
}
