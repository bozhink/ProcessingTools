namespace ProcessingTools.Contracts.Data.Mediatypes.Models
{
    public interface IMediatype
    {
        string Description { get; }

        string FileExtension { get; }

        string Mimesubtype { get; }

        string Mimetype { get; }
    }
}
