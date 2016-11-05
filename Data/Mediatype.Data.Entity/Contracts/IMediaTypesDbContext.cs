namespace ProcessingTools.MediaType.Data.Entity.Contracts
{
    using System.Data.Entity;
    using Models;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IMediaTypesDbContext : IDbContext
    {
        IDbSet<FileExtension> FileExtensions { get; set; }

        IDbSet<MimeType> MimeTypes { get; set; }

        IDbSet<MimeSubtype> MimeSubtypes { get; set; }

        IDbSet<MimeTypePair> MimeTypePairs { get; set; }
    }
}
