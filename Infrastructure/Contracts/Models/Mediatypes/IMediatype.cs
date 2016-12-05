namespace ProcessingTools.Contracts.Models.Mediatypes
{
    public interface IMediatype
    {
        string FileExtension { get; }

        string Mimesubtype { get; }

        string Mimetype { get; }
    }
}
