namespace ProcessingTools.Mediatypes.Data.Common.Contracts.Models
{
    public interface IMediatype
    {
        string Description { get; }

        string FileExtension { get; }

        string Mimesubtype { get; }

        string Mimetype { get; }
    }
}
