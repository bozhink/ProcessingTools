namespace ProcessingTools.Contracts.Models.Mediatypes
{
    public interface IMediatype
    {
        string Mimesubtype { get; }

        string Mimetype { get; }
    }
}
