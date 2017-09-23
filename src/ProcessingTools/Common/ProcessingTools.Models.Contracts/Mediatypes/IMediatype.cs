namespace ProcessingTools.Models.Contracts.Mediatypes
{
    public interface IMediatype
    {
        string Mimesubtype { get; }

        string Mimetype { get; }
    }
}
