namespace ProcessingTools.Mediatypes.Data.Entity.Contracts
{
    using System.Data.Entity;
    using Models;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IMediatypesDbContext : IDbContext
    {
        IDbSet<FileExtension> FileExtensions { get; set; }

        IDbSet<Mimetype> Mimetypes { get; set; }

        IDbSet<Mimesubtype> Mimesubtypes { get; set; }

        IDbSet<MimetypePair> MimetypePairs { get; set; }
    }
}
